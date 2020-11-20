# Liquid Entities :droplet:

A tiny Entity Component System library for Unity supporting Game Objects as entities. This implementation is a C# port of the C++ [Two ECS](https://github.com/stillwwater/twoecs) library.

## Installation

Modify `Packages/manifest.json` as follows:

```
{
  "dependencies": {
    ...
    "io.stillwwater.liquidecs": "https://github.com/stillwwater/liquidecs.git"
  }
}
```

## Getting Started

> For the full API reference see the [docs](./Documentation~/liquidecs.md). For a more complete example see the [particle system example](./Samples~/Demo), the demo scene can be download from the [Releases](https://github.com/stillwwater/liquidecs/releases) page.

There are two work-flows in Liquid ECS. One work-flow is to have entities which do not exist as `GameObject`s and don't have any `MonoBehaviour`s,  for the purposes of this documentation these will be referred to as *pure* entities. The second work-flow is to have entities that exist as a `GameObject`, as such they will show up in the Editor. These `GameObject` entities can have `MonoBehaviour`s and standard unity components attached to them, as well as pure components.

```csharp
using UnityEngine;
using Liquid.Entities;

class MyWorld : World {
    void Start() {
        // ...
    }
}
```

First we create a world `MyWorld` in its own file. `World` is a `MonoBehaviour` so we can create an empty game object in the Editor and add a `MyWorld` component to it. A `World` will hold all entities, components, and systems.

### Components & Game Objects

```csharp
var entity = world.InstantiateEntity();
```

This will create a new empty `GameObject`. `Transform` and `GameObject` will be attached to the entity and can be retrieved using `Unpack<Transform>(entity)` for example. An `Active` component will also be added to the entity.

```csharp
var entity = world.InstantiateEntity(prefab);
```

Entities can also be instantiated from a prefab.

```csharp
var component = world.Pack<MyComponent>(entity);
```

The above will create a new component of type `MyComponent` by using `GameObject.AddComponent` and attach it to the entity. This method can only be used in entities that have a `GameObject` attached.

```csharp
var go = world.Unpack<GameObject>(entity);
var component = world.Pack(entity, go.AddComponent<MyComponent>());
```

The previous example is equivalent to the code above. This overload of `Pack` takes in an existing component instance and thus can be used for both *pure* and regular entities.

> Note that components in these types of entities do not necessarily have to be `MonoBehaviour`s to be attached. Any instance of a class or struct can also be added as a component.

### Components & Pure Entities

Pure entities do not have an underlying `GameObject` and so can only have pure (non-`MonoBehaviour`) components attached to them. The benefit of this is that it avoids the overhead of `GameObject` meaning thousands of pure entities can be added to a Scene without issue.

```csharp
var entity = world.MakeEntity();
```

The above creates an entity with only an `Active` component attached.

```csharp
struct MyStructComponent {
    public float x;
}

class MyClassComponent {
    public float x;
}
```

Pure components can be reference (`class`) or value (`struct`) types. For most cases, prefer value types for pure components to take advantage of the better ECS memory layout (structs will be packed sequentially in memory). Note that no annotations are necessary to declare a component.

```csharp
ref var componentStruct = ref world.Pack(entity, new MyStructComponent { x = 1 });

var componentClass = world.Pack(entity, new MyClassComponent { x = 2 });

// For convenience, multiple components are also allowed
world.Pack(entity, new MyStructComponent(), new MyClassComponent());
```

```csharp
ref var componentStruct = ref world.Unpack<MyStructComponent>(entity);
```

Value type components are returned by reference and so the `ref` keyword may be used. This avoids a potentially expensive copy and allows the component members to be modified. Components defined as a `class` do not need this as they are already a reference.

There is nothing special about using `GameObject` entities as pure entities can be "converted" to normal entities by having a `GameObject` and `Transform` attached:

```csharp
var go = new GameObject();
var entity = world.MakeEntity();

world.Pack(entity, go);
world.Pack(entity, go.transform);
world.Pack(entity, go.AddComponent<RigidBody>());
```

### Systems

Systems loop through a set of entities every that match some constraint and perform some operation on their components.

```csharp
class MySystem : EntitySystem {
    public override void Update(World world) {
        world.Each((Transform tf, ref MyComponentA a, in MyComponentB b) => {
            // Process entity.
        });
    }
    // May also override System.Load, System.Draw, System.Unload
}
```
Here we iterate through all entities that have has at least all of `Transform`, `MyComponenA` and `MyComponentB`. The unpacked components are passed in as arguments to the lambda function.

An alternative to `World.Each`:

```csharp
public override void Update(World world) {
    foreach (var entity in world.View<MyComponentA, MyComponentB>()) {
        ref var a = ref world.Unpack<MyComponentA>();
        ref var b = ref world.Unpack<MyComponentB>();
        // process entity...
    }
}
```
There is no performance penalty for using the `World.Each` method for lambdas without a capture. Lambdas that access variables declared outside the function body on the other hand will heap allocate in C#. The allocation is generally quite small, but it is good to profile it if it becomes a problem.

As mentioned in the `Components & Pure Entities` section, components declared as structs should be passed using either `ref` or `in` to avoid a copy. `class` components (including `MonoBehaviour`s) should be passed by value.

Finally, if you need the entity id you can simply add it as the first argument to the function:

```csharp
public override void Update(World world) {
    world.Each((Entity entity, ref MyComponentA a, in MyComponentB b) => {
        // Process entity.
    });
}
```

Systems can be created using MakeSystem. This should generally be done during Start(). Systems are executed in the order which they are created.

```csharp
class MyWorld : World {
    void Start() {
		var system = MakeSystem<MySystem>();
    }
}
```

The `Load` method is called when a system is first created. By default, the `World` base class will implement an `Update` method that calls `Update` and `Draw` for each system. The `World` class also defines `OnDestroy` which calls `Unload` for each system.

> Note: When implementing your own `Update` method in the world make sure to call `world.CollectUnusedEntities()` every frame, otherwise you may run out of entity ids.


### Rendering

When using the standard entities that use `GameObject`s, nothing special needs to be done in terms of rendering. Standard `MeshFilter`, `MeshRenderer` and `SpriteRenderer` components can be used.

Pure entities have to be rendered differently. The `Liquid.Rendering` namespace includes a few components and systems for rendering pure entities. At a minimum, a pure entity requires a `RenderMesh` and `LocalToWorld` components, the `MeshRendererSystem` also needs to be added to a world.

### Events

Events are a way to communicate between systems.

```csharp
struct MyEvent {
    public int Value;
}
```

An event can be any struct or class.

```csharp
world.Bind((MyEvent e) => {
    Debug.Log($"Received event with value {e.Value}");
    return true;
});
```

A method or lambda function can be added to receive a type of event. Returning true means this event will not propagate to other event handlers.

Emit an event to all event handlers of type `MyEvent`:

```csharp
world.Emit(new MyEvent { x = 1 });
```

---

### More Component Methods

Aside from `Pack` and `Unpack` there are a few more methods for working with components that you might expect to have.

```csharp
bool hasComponent = world.Contains<RigidBody>(entity);
```

`Contains` checks if an entity has a component. This is done by checking the entity mask only, so it is a very cheap operation.

```csharp
world.Remove<RigidBody>(entity);
```

Removes a component. For `GameObject` entities, `GameObject.RemoveComponent` is also called.

```csharp
world.SetActive(entity, true);
```

`SetActive` is used to enable or disable an entity. Disabled entities won't be included in `Views` unless specified. For entities with `GameObject`s the `GameObject.SetActive` method is also called.

### More On Views

```csharp
// OK
world.Each((Transform tf, ref MyComponentA, in MyComponentB) => {});

// Not OK
world.Each((ref MyComponentA, Transform tf, in MyComponentB) => {});
```

Similar to Unity ECS, the order in which `value`, `ref`, and `in` are defined in the function parameters matters. This is mostly a C# limitation, as accounting for all permutations of these types of parameters would generate a lot of unnecessary code. Hence value components go first, then reference components go second, and constant reference (`in`) components go last.

```csharp
ref var room = ref world.UnpackOne<Room>();
```

Sometimes you may want to have components which only have a single instance in the world. Lets call these singleton components. These can be useful to hold the level information in a grid based game for example. In this example, instead of iterating through all `Room` components, we can use `UnpackOne` as a shorthand to find the entity holding the `Room` component

```csharp
var view = world.View<Room>();
Debug.Assert(view.Count > 0);
ref var room = ref world.Unpack<Room>(view[0]);
```

`UnpackOne` is simply a shorthand for the code above.


```csharp
Entity? entity = world.ViewOne<Room>();
```

To get the entity id instead of the component, use `ViewOne`. Note that this returns a `nullable` entity which will be `null` if such an entity does not exist. If more than one entity is matched, only the first will be returned.

## Performance

This library prioritizes API flexibility over being as fast as possible. Using entities with GameObjects means that there is no performance improvement over the standard Unity work-flow. Using pure entities allows for significant performance improvement when rendering thousands of entities, but this library will never be as fast as Unity's multi-threaded ECS implementation, nor is that a goal for this implementation.
