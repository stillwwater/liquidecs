using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Liquid.Entities.Tests {

// Some dummy components
struct A { public int data; }
struct B { public int data; }
struct C {}
struct D {}

class SystemA : EntitySystem {}
class SystemB : EntitySystem {}

public class EntityTests {
    [Test]
    public void TestMakeEntity() {
        var world = NewWorld();
        var entity = world.MakeEntity();
        Assert.AreNotEqual(entity, Entity.Null);
        Assert.True(world.Contains<Active>(entity));

        // Check that the NullEntity was created
        Assert.AreEqual(2, world.UnsafeViewAll().Count);
    }

    [Test]
    public void TestComponentOperations() {
        var world = NewWorld();
        var entity = world.MakeEntity();
        ref var a0 = ref world.Pack(entity, new A{});
        world.Pack(entity, new B{}, new C{});
        Assert.True(world.Contains<A, B, C>(entity));

        ref var a1 = ref world.Unpack<A>(entity);
        Assert.AreEqual(a0, a1);

        world.Remove<A>(entity);
        Assert.False(world.Contains<A>(entity));

        // Removing a component that has already been removed is a no-op.
        Assert.DoesNotThrow(() => world.Remove<A>(entity));

        world.SetActive(entity, false);
        Assert.False(world.Contains<Active>(entity));
        world.SetActive(entity, true);
        Assert.True(world.Contains<Active>(entity));
    }

    [Test]
    public void TestEntityReuse() {
        var world = NewWorld();
        var e0 = world.MakeEntity();
        Assert.AreEqual(0, e0.version);
        world.Pack(e0, new A{});
        world.DestroyEntity(e0);
        Assert.False(world.Contains<A>(e0));
        world.CollectUnusedEntities();

        var e1 = world.MakeEntity();
        Assert.AreEqual(e0.index, e1.index);
        Assert.AreNotEqual(e0, e1);
        Assert.AreEqual(1, e1.version);
    }

    [Test]
    public void TestView() {
        var world = NewWorld();
        var e0 = world.MakeEntity();
        var e1 = world.MakeEntity();
        var e2 = world.MakeEntity();
        world.Pack(e0, new A{}, new B{});
        world.Pack(e1, new A{});
        world.Pack(e2, new A{}, new B{}, new C{});

        var v0 = world.View<A, B, C>();
        Assert.AreEqual(1, v0.Count);
        Assert.AreEqual(e2, v0[0]);
        Assert.True(world.Contains<A, B, C>(v0[0]));

        Assert.AreEqual(3, world.View<A>().Count);
        Assert.True(world.ViewOne<A>().HasValue);
        Assert.AreEqual(e0, world.ViewOne<A>().Value);

        world.Remove<A>(e0);
        Assert.AreEqual(2, world.View<A>().Count);

        world.DestroyEntity(e1);
        Assert.False(world.Contains<A>(e1));
        world.CollectUnusedEntities();
        Assert.True(world.ViewOne<A>().HasValue);
        Assert.AreEqual(e2, world.ViewOne<A>().Value);

        world.SetActive(e2, false);
        Assert.AreEqual(0, world.View<A>().Count);
        Assert.AreEqual(1, world.View<A>(includeInactive: true).Count);
        Assert.AreEqual(2, world.View(includeInactive: true).Count);

        world.SetActive(e2, true);
        Assert.AreEqual(1, world.View<A>().Count);
    }

    [Test]
    public void TestViewEach() {
        var world = NewWorld();
        var e0 = world.MakeEntity();
        ref var a = ref world.Pack(e0, new A{data=12});
        world.Pack(e0, new B{data=24});

        world.Each((Entity entity, in A a_, in B b) => {
            Assert.AreEqual(entity, e0);
            Assert.AreEqual(12, a_.data);
            Assert.AreEqual(24, b.data);
        });
        world.Each((ref A a_, in B b) => {
            Assert.AreEqual(12, a_.data);
            Assert.AreEqual(24, b.data);
            a_.data = 16;
        });
        Assert.AreEqual(16, a.data);
    }

    [Test]
    public void TestMakeSystem() {
        var world = NewWorld();
        var s0 = world.MakeSystem<SystemA>();
        Assert.AreNotEqual(s0, null);
        Assert.AreEqual(s0, world.GetSystem<SystemA>());

        var sb = world.MakeSystemBefore<SystemA, SystemB>();
        Assert.AreEqual(2, world.Systems().Count);
        Assert.AreEqual(sb, world.Systems()[0]);

        var s1 = world.MakeSystem<SystemA>();
        Assert.AreEqual(s0, world.GetSystem<SystemA>());
        var systemsA = new List<SystemA>();
        world.GetAllSystems(systemsA);
        Assert.AreEqual(2, systemsA.Count);

        world.DestroySystem(s1);
        Assert.AreEqual(2, world.Systems().Count);
    }

    [Test]
    public void TestEvents() {
        var world = NewWorld();
        int res = 0;
        world.Bind((int e) => {
            res = -1;
            return false;
        });
        world.Bind((int e) => {
            res = e;
            return true;
        });
        world.Bind((int e) => {
            res = -2;
            return true;
        });
        world.Emit(12);
        Assert.AreEqual(12, res);

        world.ClearEventChannels();
        // Make sure this is not an error
        world.Emit(24);
        world.Emit(new A{});
        Assert.AreEqual(12, res);
    }

    static World NewWorld() {
        var go = new GameObject("__World");
        return go.AddComponent<World>();
    }
}

} // namespace Liquid.Entities.Tests
