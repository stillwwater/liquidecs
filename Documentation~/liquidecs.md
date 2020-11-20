# Liquid.Entities

### Liquid.Entities.Entity

```csharp
[System.Serializable]
public struct Entity : IEquatable<Entity> {
    public static readonly Entity Null = default;

    public readonly int index;

    public readonly int version;
}
```

### Entity.Identifier

```csharp
public ulong Identifier { get; }
```

Returns a unique 64 bit identifier which combines the version and index for the entity.

---

### Liquid.Entities.World

```csharp
public partial class World : MonoBehaviour {
    public const int EntityMax = 8192;

    public const int ComponentMax = EntityMask.Length; // 128
}
```

### World.Update

```csharp
public void Update();
```

The default MonoBehaviour update loop for a World. You can define an Update() function in a world MonoBehaviour to override this function.

### World.OnDestroy

```csharp
public void OnDestroy();
```

The default MonoBehaviour OnDestroy function for a World. This will call `DestroySystems()` which will call `Unload(world)` for each system. You can define `OnDestroy()` in a world to override this function.

### World.InstantiateEntity

```csharp
public void InstantiateEntity();
```

Create a new entity in the world with a new GameObject and an Active component.

```csharp
public void InstantiateEntity(GameObject prefab);
```

Creates a new GameObject entity from a prefab. All components from the prefab will be packed in the entity returned, and can be unpacked like any other component.

> Note: This function uses reflection to call the correct generic `Pack` function for each component, use the generic version of this function to avoid reflection.

### World.InstantiateInactiveEntity

```csharp
public Entity InstantiateInactiveEntity();
```

Creates a new inactive entity in the world and instantiates a new GameObject for the entity. The entity will need to have active set before it can be used by systems. Useful to create entities without initializing them.

> Note: Inactive entities still exist in the world and can have components added to them.

### World.MakeEntity

```csharp
public Entity MakeEntity();
```

Creates a new entity in the world with an Active component.

### World.MakeInactiveEntity

```csharp
public Entity MakeInactiveEntity();
```

Creates a new inactive entity in the world. The entity will need to have active set before it can be used by systems. Useful to create entities without initializing them. > Note: Inactive entities still exist in the world and can have components added to them.

### World.ConvertEntity

```csharp
public Entity ConvertEntity(GameObject go);
```

Convert a GameObject to an entity by packing references to all components in the GameObject.

### World.DestroyEntity

```csharp
public void DestroyEntity(Entity entity);
```

Destroy an entity and all of its components.

### World.GetMask

```csharp
public EntityMask GetMask(Entity entity);
```

Returns the entity mask.

### World.Pack

```csharp
public T Pack<T>(Entity entity) where T : Component;
```

Adds a component and associates an entity with the component.

Unlike the original `Pack(entity, component)` the component will also be created and added to the `GameObject` related to the entity.

```csharp
public ref T Pack<T>(Entity entity, T component);
```

Adds or replaces a component and associates an entity with the component. This function can be used with pure entities.

> Note: When using this function with GameObject entities, make sure you pass the component returned from `GameObject.AddComponent` to this function. Otherwise use the `Pack(entity)` overload instead which also adds the component to the GameObject.

Adding components will invalidate the cache. The number of cached views is *usually* approximately equal to the number of systems, so this operation is not that expensive but you should avoid doing it every frame. This does not apply to 're-packing' components (see note below).

> Note: If the entity already has a component of the same type, the old component will be replaced. Replacing a component with a new instance is *much* faster than calling `Remove` and then `Pack` which would result in the cache being rebuilt twice. Replacing a component does not invalidate the cache and is cheap operation.

### World.Unpack

```csharp
public ref T Unpack<T>(Entity entity)
```
Returns a component of the given type associated with an entity. This function will only check if the component does not exist for an entity if assertions are enabled, otherwise it is unchecked. Use `Contains` if you need to verify the existence of a component before removing it. This is a cheap operation.

This function returns a reference to a component in the packed array. The reference may become invalid after `Remove` is called since `Remove` may move components in memory. This only applies to 'struct' components.

Using `ref` is only necessary when modifying `struct` or 'Pure' components.

```csharp
ref var a = ref world.Unpack<A>(entity1);
a.x = 5; // a is a valid reference and x will be updated.

var b = world.Unpack<B>(entity1); // copy B
world.Remove<B>(entity2);
b.x = 5;
world.Pack(entity1, b); // Ensures b will be updated in the array

ref var c = reg world.Unpack<C>(entity1);
world.Remove<C>(entity2);
c.x = 5; // may not update c.x in the array
```

If you plan on holding the reference it is better to copy the component and then pack it again if you have modified the component. Re-packing a component is a cheap operation and will not invalidate. the cache.

> Do not store this reference between frames such as in a member variable, store the entity instead and call unpack each frame. This operation is designed to be called multiple times per frame so it is very fast, there is no need to `cache` a component reference in a member variable.

### World.Contains

```csharp
public bool Contains<T>(Entity entity);
```

```csharp
public bool Contains(Entity entity, Type component);
```

Returns true if a component of the given type is associated with an entity. This is a cheap operation.

### World.Remove

```csharp
public void Remove<T>(Entity entity);
```

Removes a component from an entity. Removing components invalidates the cache.

This function will only check if the component does not exist for an entity if assertions are enabled, otherwise it is unchecked. Use `Contains` if you need to verify the existence of a component before removing it.

### World.SetActive

```csharp
public void SetActive(Entity entity, bool active);
```

Adds or removes an Active component. If the entity is a GameObject, GameObject.SeActive is also called.

### World.View

```csharp
public List<Entity> View(EntityMask mask, bool includeInactive = false);
```

Returns all entities that have all requested components. By default only entities with an `Active` component are matched unless `includeInactive` is true.

```csharp
foreach (var entity in View<A, B, C>()) {
    ref var a = Unpack<A>(entity);
    // ...
}
```

The first call to this function will build a cache with the entities that contain the requested components, subsequent calls will return the cached data as long as the data is still valid. If a cache is no longer valid, this function will rebuild the cache by applying all necessary diffs to make the cache valid.

The cost of rebuilding the cache depends on how many diff operations are needed. Any operation that changes whether an entity should be in this cache (does the entity have all requested components?) counts as a single Add or Remove diff. Functions like `Remove`, `MakeEntity`, `Pack`, `DestroyEntity` may cause a cache to be invalidated. Functions that may invalidate the cache are documented.

### World.ViewOne

```csharp
public Entity? ViewOne<T>(bool includeInactive = false);
```
Returns the **first** entity that contains all components requested. Views always keep entities in the order that the entity was added to the view, so `ViewOne()` will reliabily return the same entity that matches the constraints unless the entity was destroyed or has had one of the required components removed.

The returned optional will have no value if no entities exist with all requested components.

### World.UnpackOne

```csharp
public ret T UnpackOne<T>(bool includeInactive = false);
```

Finds the first entity with the requested component and unpacks the component requested. This is convenience function for getting at a single component in a single entity.

```csharp
ref var camera = world.UnpackOne<Camera>();

// is equivalent to
var entity = world.ViewOne<Camera>().Value;
ref var camera = world.Unpack<Camera>(entity);
```

Unlike `view_one()` this function will panic if no entities with the requested component were matched. Only use this function if not matching any entity should be an error, if not use `view_one()` instead.

### World.UnsafeViewAll

```csharp
public List<Entity> UnsafeViewAll();
```

Returns all entities in the world. Entities returned may be inactive. > Note: Calling `DestroyEntity()` will invalidate the iterator, use `View<>(true)` to get all entities without having `DestroyEntity()` invalidate the iterator.

### World.Each

```csharp
public void Each<T0>(V<T0> fn, bool includeInactive = false);

// + overloads
```

```csharp
public void Each<T0>(VI<Entity, T0> fn, bool includeInactive = false);

// + overloads
```

### World.MakeSystem

```csharp
public T MakeSystem<T>() where T : EntitySystem, new();
```

Creates and adds a system to the world. This function calls `System::load` to initialize the system.

> Systems are not unique. 'Duplicate' systems, that is different instances of the same system type, are allowed in the world.

```csharp
public T MakeSystem<T>(T system) where T : EntitySystem;
```

Adds a system to the world. This function calls `System.Load` to initialize the system.

### World.MakeSystemBefore

```csharp
public T MakeSystemBefore<Before, T>() where T : EntitySystem, new() where Before : EntitySystem;
```

Adds a system to the world before another system if it exists. `Before` is an existing system. `T` is the system to be added before an existing system.

### World.GetSystem

```csharp
public T GetSystem<T>() where T : EntitySystem;
```

Returns the first system that matches the given type. System returned will be `null` if it is not found.

### World.GetAllSystems

```csharp
public void GetAllSystems<T>(List<T> systems) where T : EntitySystem;
```

Returns all systems that match the given type. Systems returned will not be null.

### World.DestroySystem

```csharp
public void DestroySystem(EntitySystem system)
```

System must not be null. Do not destroy a system while the main update loop is running as it could invalidate the system iterator, consider checking if the system should run or not in the system update loop instead.

### World.DestroyAllSystems

```csharp
public void DestroyAllSystems()
```

Destroy all systems in the world.

### World.Systems

```csharp
public List<EntitySystem> Systems();
```

Systems returned will not be null.

### World.RegisterComponent

```csharp
public void RegisterComponent<T>();
```

Components will be registered on their own if a new type of component is added to an entity. There is no need to call this function unless you are doing something specific that requires it.

### World.FindOrRegisterComponent

```csharp
public ComponentType FindOrRegisterComponent<T>();
```

Registers a component type if it does not exist and returns it. Components are registered automatically so there is usually no reason to call this function.

### World.CollectUnusedEntities

```csharp
public void CollectUnusedEntities();
```

Recycles entity ids so that they can be safely reused. This function exists to ensure we don't reuse entity ids that are still present in some cache even though the entity has been destroyed. This can happen since cache operations are done in a 'lazy' manner.

This function should be called at the end of each frame.

### World.UpdateSystems

```csharp
public void UpdateSystems()
```

Default system update loop. Calls update on all systems in the world. For more control over the update loop, use `Systems()` to get a list of all systems in the world.

### World.DrawSystems

```csharp
public void DrawSystems();
```
Default system draw loop. Calls draw all systems in the world. For more control over the draw loop, use `Systems()` to get a list of all systems in the world.

This function should only be called after all systems have been updated in the MonoBehaviour Update() loop.

### World.Bind

```csharp
public void Bind<Event>(Func<Event, bool> fn);
```

Adds a function to receive events of type Event.

```csharp
public void Bind<Event>(Func<World, Event, bool> fn);
```

Adds a function to receive events of type Event. The function will also be passed the instance of the world it is bound to.

### World.Emit

```csharp
public void Emit<Event>(in Event e);
```

Emits an event to all event handlers. If a handler function in the chain returns true then the event is considered handled and will not propagate to other listeners.

### World.ClearEventChannels

```csharp
public void ClearEventChannels();
```

Clears all event channels.

---

### Liquid.Entities.ComponentArray

> Note: This class is mostly an implementation detail

```csharp
public class ComponentArray<T> : IComponentArray {}
```

### ComponentArray.Count

```csharp
public int Count { get; }
```

Returns the number of valid components in the packed array.

### ComponentArray.Read

```csharp
public ref T Read(Enity entity);
```

Returns a component of type T given an Entity.

> Note: References returned by this function are only guaranteed to be valid during the frame in which the component was read, after that the component may become invalidated. Don't hold reference.

### ComponentArray.ReadData

```csharp
public object ReadData(Entity entity);
```

Returns the component for a given entity boxed in a reference object. This is useful for debugging.

### ComponentArray.Write

```csharp
public ref T Write(Entity entity, T component);
```

Set a component in the packed array and associate an entity with the component.

### ComponentArray.Remove

```csharp
public void Remove(Entity entity);
```

Invalidate this component type for an entity. This function will always succeed, even if the entity does not contain a component of this type.

> References returned by `Read` may become invalid after remove is called.

### ComponentArray.Copy

```csharp
public vois Copy(Entity dst, Enitty src);
```

Copy component to `dst` from `src`. If the component is a  reference object this will only copy the component reference.

### ComponentArray.Contains

```csharp
public bool Contains(Entity entity);
```

Returns true if the entity has a component of type T.

---

### Liquid.Entities.EventChannel

> Note: This class is mostly an implementation detail

```csharp
public class EventChannel<Event> : IEventChannel {}
```

### EventChannel.Bind

```csharp
public void Bind(Func<Event, bool> fn);
```

Adds a function as an event handler

### EventChannel.Emit

```csharp
public void Emit(in Event e);
```

Emits an event to all event handlers

---

# Liquid.Rendering

### Liquid.Rendering.Position

```csharp
public struct Position {
    public float3 Value;
}
```

---

### Liquid.Rendering.Rotation

```csharp
public struct Rotation {
    public quaternion Value;
}
```

---

### Liquid.Rendering.Scale

```csharp
public struct Scale {
    public float3 Value;
}
```
---

### Liquid.Rendering.LocalToWorld

```csharp
public struct Position {
    public float4x4 Value;
}
```

Transformation matrix for an entity. If the entity has any of either a Position, Rotation or Scale component the LocalToWorld matrix will be populated by the LocalToWorldSystem.

---

### Liquid.Rendering.RenderMesh

```csharp
public struct RenderMesh {
    // The mesh to draw.
    public Mesh mesh;

    // The material used to draw the mesh.
    public Material material;

    // Which layer to use.
    public int layer;

    // If null the mesh will be drawn by all cameras.
    public Camera camera;

    // Which sunset of the mesh to draw, only applies to meshes that have
    // several materials
    public int subMesh;

    // Additional material properties to apply onto the material before
    // the mesh is drawn.
    public MaterialPropertyBlock properties;

    // Determines whether the mesh can cast shadows.
    public ShadowCastingMode castShadows;

    // Determines whether the mesh can receive shadows.
    public bool receiveShadows;
}
```

A container component for a mesh and material to be rendered.

---

### Liquid.Rendering.LocalToWorldSystem

```csharp
public class LocalToWorldSystem : EntitySystem {}
```

Populates a `LocalToWorld` matrix component using values from the Position, Rotation and Scale components. If an entity does not have the Position, Rotation or Scale components this system will not modify the `LocalToWorld` matrix so that the matrix can be set directly.

> This system is added by the `MeshRendererSystem` and does not need to be
added manually.

---

### Liquid.Rendering.MeshRendererSystem

```csharp
public class MeshRendererSystem : EntitySystem {}
```

A simple rendering system using `Graphics.DrawMesh` for each entity with a `RenderMesh` and a `LocalToWorld` component.
