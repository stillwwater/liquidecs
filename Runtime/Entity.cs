using System;

namespace Liquid.Entities {

// A unique identifier representing each entity in the world.
public struct Entity : IEquatable<Entity> {
    // Used to represent an entity that has no value. The `NullEntity` exists
    // in the world but has no components.
    public static readonly Entity Null = default;

    public readonly int index;

    // The entity version that is incremented each time the entity index
    // is reused.
    public readonly int version;

    public Entity(int index, int version) {
        this.index = index;
        this.version = version;
    }

    // Returns a unique 64 bit identifier which combines the version and index
    // for the entity.
    public ulong Identifier => ((ulong)version << 32) | (uint)index;

    public static bool operator==(Entity a, Entity b) {
        return a.Identifier == b.Identifier;
    }

    public static bool operator!=(Entity a, Entity b) {
        return !(a == b);
    }

    // Compare an entity without boxing.
    public bool Equals(Entity entity) {
        return entity == this;
    }

    public override bool Equals(object obj) {
        return obj is Entity && (Entity)obj == this;
    }

    // Returns a hash for this entity. Note that the hash code does not
    // include the entity version, so two entities with the same index
    // but with different versions will hash to the same value.
    public override int GetHashCode() {
        return index;
    }

    public override string ToString() {
        return $"#{version:x8}{index:x8}";
    }
}

} // namespace Liquid.Entities
