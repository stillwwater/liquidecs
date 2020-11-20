using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Profiling;
using static UnityEngine.Debug;

namespace Liquid.Entities {

// A unique identifier representing each type of component.
using ComponentType = System.Byte;

// Holds information on which components are attached to an entity.
// 1 bit is used for each component type.
// Note: Do not serialize an entity mask since which bit represents a
// given component may change. Use the Contains<Component> function
// instead.
using EntityMask = Bitset128;

// An empty component that is used to indicate whether the entity it is
// attached to is currently active.
[Serializable]
public struct Active {}

// Base class for all systems. The lifetime of systems is managed by a World.
public abstract class EntitySystem {
    public virtual void Load(World world) {}
    public virtual void Update(World world) {}
    public virtual void Draw(World world) {}
    public virtual void Unload(World world) {}
}

// A world holds a collection of systems, components and entities.
public partial class World : MonoBehaviour {
    // Diff operations that are applied to entity caches.
    struct EntityDiff {
        internal enum Operation { Add, Remove };
        internal Entity entity;
        internal Operation op;
    }

    // Used to speed up entity lookups.
    class EntityCache {
        internal List<Entity> entities;
        internal List<EntityDiff> diffs;
        internal HashSet<Entity> lookup;
    }

    struct DestroyedEntity {
        internal Entity entity;
        // Caches that needs to be rebuilt before the entity can be reused.
        internal List<EntityCache> caches;
    }

    // Defines the maximum number of entities that may be alive at the
    // same time.
    public const int EntityMax = 8192;

    // Defines the maximum number of component types
    public const int ComponentMax = EntityMask.Length;

    // Systems cannot oulive World.
    readonly List<EntitySystem> activeSystems = new List<EntitySystem>();

    // Kept separate since most of the time we just want to iterate through
    // all the systems and do not need to know their types.
    readonly List<Type> activeSystemTypes = new List<Type>();

    // Used to get a string representation of a system type to use as
    // profiling information.
    readonly List<string> activeSystemNames = new List<string>();

    // All alive (but not necessarily active) entities.
    readonly List<Entity> entities = new List<Entity>();

    // Contains available entity ids. When creating entities check if this
    // is not empty, otherwise use alive_count + 1 as the new id.
    readonly List<Entity> unusedEntities = new List<Entity>();

    // Contains available entity ids that may still be present in
    // some cache. Calling `collect_unused_entities()` will remove the
    // entity from the caches so that the entity can be reused.
    readonly List<DestroyedEntity> destroyedEntities
        = new List<DestroyedEntity>();

    readonly Dictionary<EntityMask, EntityCache> viewCache
        = new Dictionary<EntityMask, EntityCache>();

    // Masks for all entities
    readonly EntityMask[] entityMasks = new EntityMask[EntityMax];

    readonly Dictionary<Type, ComponentType> componentTypes
        = new Dictionary<Type, ComponentType>();

    // Event channels.
    readonly Dictionary<Type, IEventChannel> channels
        = new Dictionary<Type, IEventChannel>();

    ComponentType componentTypeIndex = 0;
    int aliveCount = 0;

    // Index with component index from componentTypes[type]
    // This is never modified with a new instance but is not marked readonly
    // so it can be read using reflection by the editor.
    IComponentArray[] components = new IComponentArray[ComponentMax];

    // The default MonoBehaviour update loop for a World. You can define an
    // Update() function in a world MonoBehaviour to override this function.
    public void Update() {
        UpdateSystems();
        DrawSystems();
        CollectUnusedEntities();
    }

    // The default MonoBehaviour OnDestroy function for a World. This will
    // call `DestroyAllSystems()` which will call `Unload(world)` for each
    // system. You can define `OnDestroy()` in a world to override this
    // function.
    public void OnDestroy() {
        DestroyAllSystems();
    }

    // Create a new entity in the world with a new GameObject and an
    // Active component.
    public Entity InstantiateEntity() {
        var entity = MakeInactiveEntity();
        Pack(entity, new Active());
        return entity;
    }

    // Creates a new GameObject entity from a prefab. All components from the
    // prefab will be packed in the entity returned, and can be unpacked like
    // any other component.
    //
    // > Note: This function uses reflection to call the correct generic `Pack`
    // function for each component, use the generic version of this function to
    // avoid reflection.
    public Entity InstantiateEntity(GameObject prefab) {
        Assert(prefab != null);
        var entity = InstantiateEntity();
        var go = Pack(entity, GameObject.Instantiate(prefab));

        go.name = entity.ToString();
        go.transform.SetParent(transform);
        CopyComponentReferences(entity, go);
        return entity;
    }

    // Creates a new inactive entity in the world and instantiates
    // a new GameObject for the entity. The entity will need to have
    // active set before it can be used by systems.
    // Useful to create entities without initializing them.
    // > Note: Inactive entities still exist in the world and can have
    // components added to them.
    public Entity InstantiateInactiveEntity() {
        var entity = MakeInactiveEntity();
        var go = Pack(entity, new GameObject(entity.ToString()));
        var tf = Pack(entity, go.transform);
        tf.SetParent(transform);
        return entity;
    }

    // Creates a new entity in the world with an Active component.
    public Entity MakeEntity() {
        var entity = MakeInactiveEntity();
        Pack(entity, new Active());
        return entity;
    }

    // Creates a new inactive entity in the world. The entity will need
    // to have active set before it can be used by systems.
    // Useful to create entities without initializing them.
    // > Note: Inactive entities still exist in the world and can have
    // components added to them.
    public Entity MakeInactiveEntity() {
        Entity entity = default;
        if (unusedEntities.Count == 0) {
            Assert(aliveCount < EntityMax, "Too many entities");
            entity = new Entity(aliveCount++, 0);

            // Create a nullentity that will never have any components
            // This is useful when we need to store entities in an array and
            // need a way to define entities that are not valid.
            if (entity == Entity.Null) {
                entities.Add(Entity.Null);
                entity = new Entity(entity.index + 1, 0);
                ++aliveCount;
            }
        } else {
            var last = unusedEntities.Count - 1;
            var unused = unusedEntities[last];
            unusedEntities.RemoveAt(last);
            entity = new Entity(unused.index, unused.version + 1);
        }
        entities.Add(entity);
        return entity;
    }

    // Convert a GameObject to an entity by packing references to all
    // components in the GameObject.
    public Entity ConvertEntity(GameObject go) {
        var entity = MakeEntity();
        Pack(entity, go);
        CopyComponentReferences(entity, go);
        return entity;
    }

    // Destroys an entity and all of its components.
    public void DestroyEntity(Entity entity) {
        if (Contains<GameObject>(entity)) {
            GameObject.Destroy(Unpack<GameObject>(entity));
        }
        foreach (var a in components) {
            if (a != null) {
                a.Remove(entity);
            }
        }
        entityMasks[entity.index] = new EntityMask(0);

        var destroyed = new DestroyedEntity {
            entity = entity,
            caches = new List<EntityCache>(),
        };

        foreach (var cached in viewCache) {
            var lookup = cached.Value.lookup;
            if (!lookup.Contains(entity)) {
                continue;
            }
            cached.Value.diffs.Add(new EntityDiff {
                entity = entity,
                op = EntityDiff.Operation.Remove,
            });

            // This cache must be rebuilt before the entity can be reused.
            destroyed.caches.Add(cached.Value);
        }
        var rem = entities.IndexOf(entity);
        var last = entities.Count - 1;
        Assert(rem != -1);
        entities[rem] = entities[last];
        entities.RemoveAt(last);
        destroyedEntities.Add(destroyed);
    }

    // Returns the entity mask
    public EntityMask GetMask(Entity entity) {
        return entityMasks[entity.index];
    }

    // Adds a component and associates an entity with the component.
    //
    // Unlike the original `Pack(entity, component)` the component will also
    // be created and added to the `GameObject` related to the entity.
    public T Pack<T>(Entity entity) where T : Component {
        Assert(Contains<GameObject>(entity),
               $"Entity {entity} does not have a GameObject, "
               + "did you mean to call Pack(entity, component)?");

        Assert(!Contains<T>(entity),
               $"The MonoBehaviour {typeof(T)} has already been added "
               + $"to {entity}. Only pure components can be repacked.");

        var go = Unpack<GameObject>(entity);
        var component = go.AddComponent<T>();
        return PackComponentReference(entity, component);
    }

    // Adds or replaces a component and associates an entity with the
    // component. This function can be used with pure entities.
    //
    // > Note: When using this function with GameObject entities, make sure
    // you pass the component returned from `GameObject.AddComponent` to this
    // function. Otherwise use the `Pack(entity)` overload instead which also
    // adds the component to the GameObject.
    //
    // Adding components will invalidate the cache. The number of cached views
    // is *usually* approximately equal to the number of systems, so this
    // operation is not that expensive but you should avoid doing it every
    // frame. This does not apply to 're-packing' components (see note below).
    //
    // > Note: If the entity already has a component of the same type, the old
    // component will be replaced. Replacing a component with a new instance
    // is *much* faster than calling `Remove` and then `Pack` which
    // would result in the cache being rebuilt twice. Replacing a component
    // does not invalidate the cache and is cheap operation.
    public ref T Pack<T>(Entity entity, T component) {
        var index = entity.index;
        var currentMask = entityMasks[index];
        ref var mask = ref entityMasks[index];

        // Component may not have been registered yet
        var type = FindOrRegisterComponent<T>();
        mask.Set(type);

        var a = components[type] as ComponentArray<T>;
        ref var newComponent = ref a.Write(entity, component);

        if (currentMask[type] == mask[type]) {
            // entity already has a component of this type, the component was
            // replaced, but since the mask is unchanged there is no need to
            // rebuild the cache.
            return ref newComponent;
        }

        // Invalidate caches
        InvalidateWriteCaches(entity, in mask);
        return ref newComponent;
    }

    public void Pack<T0, T1>(Entity entity, T0 c0, T1 c1) {
        Pack(entity, c0);
        Pack(entity, c1);
    }

    public void Pack<T0, T1, T2>(Entity entity, T0 c0, T1 c1, T2 c2) {
        Pack(entity, c0);
        Pack(entity, c1, c2);
    }

    public void Pack<T0, T1, T2, T3>(
        Entity entity,
        T0 c0, T1 c1, T2 c2, T3 c3) {

        Pack(entity, c0);
        Pack(entity, c1, c2, c3);
    }

    public void Pack<T0, T1, T2, T3, T4>(
        Entity entity,
        T0 c0, T1 c1, T2 c2, T3 c3, T4 c4) {

        Pack(entity, c0);
        Pack(entity, c1, c2, c3, c4);
    }

    public void Pack<T0, T1, T2, T3, T4, T5>(
        Entity entity,
        T0 c0, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5) {

        Pack(entity, c0);
        Pack(entity, c1, c2, c3, c4, c5);
    }

    public void Pack<T0, T1, T2, T3, T4, T5, T6>(
        Entity entity,
        T0 c0, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6) {

        Pack(entity, c0);
        Pack(entity, c1, c2, c3, c4, c5, c6);
    }

    public void Pack<T0, T1, T2, T3, T4, T5, T6, T7>(
        Entity entity,
        T0 c0, T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7) {

        Pack(entity, c0);
        Pack(entity, c1, c2, c3, c4, c5, c6, c7);
    }

    // Returns a component of the given type associated with an entity.
    // This function will only check if the component does not exist for an
    // entity if assertions are enabled, otherwise it is unchecked.
    // Use `Contains` if you need to verify the existence of a component
    // before removing it. This is a cheap operation.
    //
    // This function returns a reference to a component in the packed array.
    // The reference may become invalid after `Remove` is called since `Remove`
    // may move components in memory. This only applies to 'struct' components.
    //
    // Using `ref` is only necessary when modifying `struct` or 'Pure' components.
    //
    //     ref var a = ref world.Unpack<A>(entity1);
    //     a.x = 5; // a is a valid reference and x will be updated.
    //
    //     var b = world.Unpack<B>(entity1); // copy B
    //     world.Remove<B>(entity2);
    //     b.x = 5;
    //     world.Pack(entity1, b); // Ensures b will be updated in the array
    //
    //     ref var c = reg world.Unpack<C>(entity1);
    //     world.Remove<C>(entity2);
    //     c.x = 5; // may not update c.x in the array
    //
    // If you plan on holding the reference it is better to copy the
    // component and then pack it again if you have modified the component.
    // Re-packing a component is a cheap operation and will not invalidate.
    // the cache.
    //
    // > Do not store this reference between frames such as in a member
    // variable, store the entity instead and call unpack each frame. This
    // operation is designed to be called multiple times per frame so it
    // is very fast, there is no need to `cache` a component reference in
    // a member variable.
    public ref T Unpack<T>(Entity entity) {
        ComponentType type;
        // If block prevents allocating the string for the assert every time
        if (!componentTypes.TryGetValue(typeof(T), out type)) {
            // Assume the component was registered when it was packed
            Assert(false, $"Missing component {typeof(T)} (never packed)");
        }
        var a = components[type] as ComponentArray<T>;
        return ref a.Read(entity);
    }

    // Returns true if a component of the given type is associated with an
    // entity. This is a cheap operation.
    public bool Contains(Entity entity, Type component) {
        // This function must work if a component has never been registered,
        // since it's reasonable to check if an entity has a component when
        // a component type has never been added to any entity.
        if (componentTypes.TryGetValue(component, out var type)) {
            return entityMasks[entity.index].Test(type);
        }
        return false;
    }

    // Returns true if a component of the given type is associated with an
    // entity. This is a cheap operation.
    public bool Contains<T>(Entity entity) {
        return Contains(entity, typeof(T));
    }

    public bool Contains<T0, T1>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1>(entity);
    }

    public bool Contains<T0, T1, T2>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1, T2>(entity);
    }

    public bool Contains<T0, T1, T2, T3>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1, T2, T3>(entity);
    }

    public bool Contains<T0, T1, T2, T3, T4>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1, T2, T3, T4>(entity);
    }

    public bool Contains<T0, T1, T2, T3, T4, T5>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1, T2, T3, T4, T5>(entity);
    }

    public bool Contains<T0, T1, T2, T3, T4, T5, T6>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1, T2, T3, T4, T5, T6>(entity);
    }

    public bool Contains<T0, T1, T2, T3, T4, T5, T6, T7>(Entity entity) {
        return Contains<T0>(entity)
            && Contains<T1, T2, T3, T4, T5, T6, T7>(entity);
    }

    // Removes a component from an entity. Removing components invalidates
    // the cache.
    //
    // This function will only check if the component does not exist for an
    // entity if assertions are enabled, otherwise it is unchecked. Use
    // `Contains` if you need to verify the existence of a component before
    // removing it.
    public void Remove<T>(Entity entity) {
        // Assume component was registered when it was packed
        Assert(componentTypes.ContainsKey(typeof(T)));
        var type = componentTypes[typeof(T)];
        var a = components[type] as ComponentArray<T>;
        a.Remove(entity);

        // Invalidate caches
        foreach (var cached in viewCache) {
            if (!cached.Key[type]) {
                continue;
            }
            var lookup = cached.Value.lookup;
            if (!lookup.Contains(entity)) {
                // Entity has already been removed from cache
                continue;
            }
            InavalidateCache(cached.Value,
                new EntityDiff {
                    entity = entity,
                    op = EntityDiff.Operation.Remove,
                });
        }
        entityMasks[entity.index].Reset(type);
    }

    // Adds or removes an Active component. If the entity is a
    // GameObject, GameObject.SeActive is also called.
    public void SetActive(Entity entity, bool active) {
        if (active)
            Pack(entity, new Active());
        else
            Remove<Active>(entity);

        if (Contains<GameObject>(entity)) {
            var go = Unpack<GameObject>(entity);
            go.SetActive(active);
        }
    }

    // Returns all entities that have all requested components. By default
    // only entities with an `Active` component are matched unless
    // `includeInactive` is true.
    //
    //     foreach (var entity in View<A, B, C>()) {
    //         ref var a = Unpack<A>(entity);
    //         // ...
    //     }
    //
    // The first call to this function will build a cache with the entities
    // that contain the requested components, subsequent calls will return
    // the cached data as long as the data is still valid. If a cache is no
    // longer valid, this function will rebuild the cache by applying all
    // necessary diffs to make the cache valid.
    //
    // The cost of rebuilding the cache depends on how many diff operations
    // are needed. Any operation that changes whether an entity should be
    // in this cache (does the entity have all requested components?) counts
    // as a single Add or Remove diff. Functions like `Remove`,
    // `MakeEntity`, `Pack`, `DestroyEntity` may cause a cache to be
    // invalidated. Functions that may invalidate the cache are documented.
    public List<Entity> View(EntityMask mask, bool includeInactive = false) {
        if (!includeInactive) {
            mask.Set(FindOrRegisterComponent<Active>());
        }
        if (viewCache.TryGetValue(mask, out var cached)) {
            if (cached.diffs.Count == 0) {
                return cached.entities;
            }
            ApplyDiffsToCache(cached);
            return cached.entities;
        }

        var cache = new List<Entity>();
        var lookup = new HashSet<Entity>();

        foreach (var entity in entities) {
            if ((mask & entityMasks[entity.index]) == mask) {
                if (entity != Entity.Null) {
                    cache.Add(entity);
                    lookup.Add(entity);
                }
            }
        }
        viewCache.Add(mask, new EntityCache {
            entities = cache,
            diffs    = new List<EntityDiff>(),
            lookup   = lookup,
        });
        return viewCache[mask].entities;
    }

    public List<Entity> View(bool includeInactive = false) {
        EntityMask mask = default;
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0>(bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1>(bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1, T2>(bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        mask.Set(FindOrRegisterComponent<T2>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1, T2, T3>(bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        mask.Set(FindOrRegisterComponent<T2>());
        mask.Set(FindOrRegisterComponent<T3>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1, T2, T3, T4>(
            bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        mask.Set(FindOrRegisterComponent<T2>());
        mask.Set(FindOrRegisterComponent<T3>());
        mask.Set(FindOrRegisterComponent<T4>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1, T2, T3, T4, T5>(
            bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        mask.Set(FindOrRegisterComponent<T2>());
        mask.Set(FindOrRegisterComponent<T3>());
        mask.Set(FindOrRegisterComponent<T4>());
        mask.Set(FindOrRegisterComponent<T5>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1, T2, T3, T4, T5, T6>(
            bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        mask.Set(FindOrRegisterComponent<T2>());
        mask.Set(FindOrRegisterComponent<T3>());
        mask.Set(FindOrRegisterComponent<T4>());
        mask.Set(FindOrRegisterComponent<T5>());
        mask.Set(FindOrRegisterComponent<T6>());
        return View(mask, includeInactive);
    }

    public List<Entity> View<T0, T1, T2, T3, T4, T5, T6, T7>(
            bool includeInactive = false) {
        EntityMask mask = default;
        mask.Set(FindOrRegisterComponent<T0>());
        mask.Set(FindOrRegisterComponent<T1>());
        mask.Set(FindOrRegisterComponent<T2>());
        mask.Set(FindOrRegisterComponent<T3>());
        mask.Set(FindOrRegisterComponent<T4>());
        mask.Set(FindOrRegisterComponent<T5>());
        mask.Set(FindOrRegisterComponent<T6>());
        mask.Set(FindOrRegisterComponent<T7>());
        return View(mask, includeInactive);
    }

    // Returns the **first** entity that contains all components requested.
    // Views always keep entities in the order that the entity was
    // added to the view, so `ViewOne()` will reliabily return the same
    // entity that matches the constraints unless the entity was destroyed
    // or has had one of the required components removed.
    //
    // The returned optional will have no value if no entities exist with
    // all requested components.
    public Entity? ViewOne<T>(bool includeInactive = false) {
        var v = View<T>(includeInactive);
        if (v.Count > 0) {
            return v[0];
        }
        return null;
    }

    // Finds the first entity with the requested component and unpacks
    // the component requested. This is convenience function for getting at
    // a single component in a single entity.
    //
    //     ref var camera = world.UnpackOne<Camera>();
    //
    //     // is equivalent to
    //     var entity = world.ViewOne<Camera>().Value;
    //     ref var camera = world.Unpack<Camera>(entity);
    //
    // Unlike `view_one()` this function will panic if no entities with
    // the requested component were matched. Only use this function if not
    // matching any entity should be an error, if not use `view_one()`
    // instead.
    public ref T UnpackOne<T>(bool includeInactive = false) {
        var v = View<T>(includeInactive);
        Assert(v.Count > 0, "No entities were matched");
        return ref Unpack<T>(v[0]);
    }

    // Returns all entities in the world. Entities returned may be inactive.
    // > Note: Calling `DestroyEntity()` will invalidate the iterator, use
    // `View<>(true)` to get all entities without having `DestroyEntity()`
    // invalidate the iterator.
    public List<Entity> UnsafeViewAll() => entities;

    // Creates and adds a system to the world. This function calls
    // `System.Load` to initialize the system.
    //
    // > Systems are not unique. 'Duplicate' systems, that is different
    // instances of the same system type, are allowed in the world.
    public T MakeSystem<T>() where T : EntitySystem, new() {
        return MakeSystem<T>(new T());
    }

    // Adds a system to the world. This function calls `System.Load` to
    // initialize the system.
    public T MakeSystem<T>(T system) where T : EntitySystem {
        activeSystems.Add(system);
        activeSystemTypes.Add(typeof(T));
        activeSystemNames.Add(typeof(T).Name);
        system.Load(this);
        return system;
    }

    // Adds a system to the world before another system if it exists.
    // `Before` is an existing system.
    // `T` is the system to be added before an existing system.
    public T MakeSystemBefore<Before, T>() where T : EntitySystem, new()
                                           where Before : EntitySystem {
        Assert(activeSystems.Count == activeSystemTypes.Count);

        var pos = activeSystemTypes.IndexOf(typeof(Before));
        var system = new T();
        if (pos == -1) {
            return system;
        }
        activeSystems.Insert(pos, system);
        activeSystemTypes.Insert(pos, typeof(T));
        activeSystemNames.Insert(pos, typeof(T).Name);
        return system;
    }

    // Returns the first system that matches the given type.
    // System returned will be `null` if it is not found.
    public T GetSystem<T>() where T : EntitySystem {
        Assert(activeSystems.Count == activeSystemTypes.Count);
        int index = activeSystemTypes.IndexOf(typeof(T));
        return index == -1 ? null : activeSystems[index] as T;
    }

    // Returns all systemis that match the given type.
    // Systems returned will not be null.
    public void GetAllSystems<T>(List<T> systems) where T : EntitySystem {
        Assert(activeSystems.Count == activeSystemTypes.Count);
        Assert(systems != null);

        for (int i = 0; i < activeSystems.Count; ++i) {
            if (activeSystemTypes[i] != typeof(T)) {
                continue;
            }
            systems.Add(activeSystems[i] as T);
        }
    }

    // System must not be null. Do not destroy a system while the main update
    // loop is running as it could invalidate the system iterator, consider
    // checking if the system should run or not in the system update loop
    // instead.
    public void DestroySystem(EntitySystem system) {
        Assert(activeSystems.Count == activeSystemTypes.Count);
        Assert(system != null);

        var pos = activeSystems.IndexOf(system);
        if (pos == -1) {
            LogWarning("Trying to destroy a system that does not exist");
            return;
        }
        system.Unload(this);
        activeSystems.RemoveAt(pos);
        activeSystemTypes.RemoveAt(pos);
    }

    // Destroy all systems in the world.
    public void DestroyAllSystems() {
        foreach (var system in activeSystems) {
            system.Unload(this);
        }
        activeSystems.Clear();
        activeSystemTypes.Clear();
    }

    // Systems returned will not be null.
    public List<EntitySystem> Systems() => activeSystems;

    // Components will be registered on their own if a new type of component is
    // added to an entity. There is no need to call this function unless you
    // are doing something specific that requires it.
    public void RegisterComponent<T>() {
        // Component must not already exist
        Assert(!componentTypes.ContainsKey(typeof(T)));

        var i = componentTypeIndex++;
        componentTypes.Add(typeof(T), i);
        components[i] = new ComponentArray<T>();
    }

    // Registers a component type if it does not exist and returns it.
    // Components are registered automatically so there is usually no reason
    // to call this function.
    public ComponentType FindOrRegisterComponent<T>() {
        if (componentTypes.TryGetValue(typeof(T), out var type)) {
            return type;
        }
        RegisterComponent<T>();
        return componentTypes[typeof(T)];
    }

    // Recycles entity ids so that they can be safely reused. This function
    // exists to ensure we don't reuse entity ids that are still present in
    // some cache even though the entity has been destroyed. This can happen
    // since cache operations are done in a 'lazy' manner.
    // This function should be called at the end of each frame.
    public void CollectUnusedEntities() {
        if (destroyedEntities.Count == 0) {
            return;
        }
        foreach (var destroyed in destroyedEntities) {
            foreach (var cache in destroyed.caches) {
                // In most cases the cache will have no diffs since if this
                // cache is viewed every frame by some system it would have
                // been rebuilt by this point anyway.
                ApplyDiffsToCache(cache);
            }
            // Make entity id available again
            unusedEntities.Add(destroyed.entity);
        }
        destroyedEntities.Clear();
    }

    // Default system update loop. Calls update on all systems in the world.
    // For more control over the update loop, use `Systems()` to get a list
    // of all systems in the world.
    public void UpdateSystems() {
        Profiler.BeginSample("EntitySystem.Update()");
        for (int i = 0; i < activeSystems.Count; ++i) {
            var system = activeSystems[i];
            Profiler.BeginSample(activeSystemNames[i]);
            system.Update(this);
            Profiler.EndSample();
        }
        Profiler.EndSample();
    }

    // Default system draw loop. Calls draw all systems in the world.
    // For more control over the draw loop, use `Systems()` to get a list
    // of all systems in the world.
    //
    // This function should only be called after all systems have been
    // updated in the MonoBehaviour Update() loop.
    public void DrawSystems() {
        Profiler.BeginSample("EntitySystem.Draw()");
        for (int i = 0; i < activeSystems.Count; ++i) {
            var system = activeSystems[i];
            Profiler.BeginSample(activeSystemNames[i]);
            system.Draw(this);
            Profiler.EndSample();
        }
        Profiler.EndSample();
    }

    // Adds a function to receive events of type Event.
    public void Bind<Event>(Func<Event, bool> fn) {
        var type = typeof(Event);
        if (!channels.ContainsKey(type)) {
            channels.Add(type, new EventChannel<Event>());
        }
        (channels[type] as EventChannel<Event>).Bind(fn);
    }

    // Adds a function to receive events of type Event. The function will also
    // be passed the instance of the world it is bound to.
    public void Bind<Event>(Func<World, Event, bool> fn) {
        Bind((Event e) => fn(this, e));
    }

    // Emits an event to all event handlers. If a handler function in the
    // chain returns true then the event is considered handled and will
    // not propagate to other listeners.
    public void Emit<Event>(in Event e) {
        if (channels.TryGetValue(typeof(Event), out var channel)) {
            (channel as EventChannel<Event>).Emit(in e);
        }
    }

    // Clears all event channels.
    public void ClearEventChannels() {
        channels.Clear();
    }

    // Packs references to components attached to a GameObject.
    void CopyComponentReferences(Entity entity, GameObject go) {
        Assert(go != null);
        // We can't really avoid reflection because Pack may create a new
        // ComponentArray<T> if the component was never registered before.
        // To avoid reflection here components would need to be registered
        // manually, or during startup using reflection.
        var flags = BindingFlags.NonPublic | BindingFlags.Instance;
        var pack = typeof(World).GetMethod("PackComponentReference", flags);
        var args = new object[] { entity, null };
        var components = go.GetComponents(typeof(Component));

        foreach (var c in components) {
            var type = c.GetType();
            args[1] = c;
            pack.MakeGenericMethod(type).Invoke(this, args);
        }
    }

    // Used to pack MonoBehaviour components. Returns a value instead of a
    // reference so it can be used with reflection. It cannot be used with
    // value types.
    T PackComponentReference<T>(Entity entity, T component) where T : class {
        var index = entity.index;
        ref var mask = ref entityMasks[index];

        // Component may not have been registered yet
        var type = FindOrRegisterComponent<T>();
        mask.Set(type);

        var a = components[type] as ComponentArray<T>;
        var newComponent =  a.Write(entity, component);
        InvalidateWriteCaches(entity, in mask);
        return newComponent;
    }

    void InvalidateWriteCaches(Entity entity, in EntityMask mask) {
        foreach (var cached in viewCache) {
            if ((mask & cached.Key) != cached.Key) {
                continue;
            }
            var lookup = cached.Value.lookup;
            if (lookup.Contains(entity)) {
                // Entity is already in the cache
                continue;
            }

            InavalidateCache(cached.Value,
                new EntityDiff {
                    entity = entity,
                    op = EntityDiff.Operation.Add
                });
        }
    }

    void ApplyDiffsToCache(EntityCache cache) {
        Assert(cache != null);
        foreach (var diff in cache.diffs) {
            switch (diff.op) {
            case EntityDiff.Operation.Add:
                cache.entities.Add(diff.entity);
                cache.lookup.Add(diff.entity);
                break;

            case EntityDiff.Operation.Remove:
                cache.entities.Remove(diff.entity);
                cache.lookup.Remove(diff.entity);
                break;

            default:
                Assert(false, "Invalid cache operation");
                break;
            }
        }
        cache.diffs.Clear();
    }

    void InavalidateCache(EntityCache cache, EntityDiff diff) {
        foreach (var d in cache.diffs) {
            if (d.entity == diff.entity && d.op == diff.op) {
                return;
            }
        }
        cache.diffs.Add(diff);
    }
}

} // namespace Liquid.Entities
