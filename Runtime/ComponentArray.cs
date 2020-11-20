using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace Liquid.Entities {

public interface IComponentArray {
    void Remove(Entity entity);
    void Copy(Entity dst, Entity src);
    object ReadData(Entity entity);
}

// Manages all instances of a component type and keeps track of which
// entity a component is attached to.
public class ComponentArray<T> : IComponentArray {
    // Defines the number of entities per component array chunk.
    const int ChunkSize = 512;

    // Used to represent an invalid entity index in the ComponentArray.
    const int InvalidIndex = -1;

    // Maps an Entity id to an index in the packed array.
    readonly List<int[]> chunks = new List<int[]>();

    // Maps an index in the packed component array to an Entity.
    readonly Dictionary<int, Entity> packedToEntity
        = new Dictionary<int, Entity>();

    // All instances of component type T are stored in a contiguous vector.
    T[] packed = new T[128];

    // Number of valid entries in the packed array, other entries beyond
    // this count may be uninitialized or invalid data.
    int packedCount = 0;

    // Returns the number of valid components in the packed array.
    public int Count => packedCount;

    // Returns a component of type T given an Entity.
    // Note: References returned by this function are only guaranteed to be
    // valid during the frame in which the component was read, after
    // that the component may become invalidated. Don't hold reference.
    public ref T Read(Entity entity) {
        var index = FindIndex(entity);
        // The if block prevents allocationg the format string for the assertion.
        if (index == InvalidIndex) {
            Assert(false, $"Missing component {typeof(T)} on Entity {entity}");
        }
        return ref packed[index];
    }

    // Returns the component for a given entity boxed in a reference object.
    // This is useful for debugging.
    public object ReadData(Entity entity) {
        return (object)Read(entity);
    }

    // Set a component in the packed array and associate an entity with the
    // component.
    public ref T Write(Entity entity, T component) {
        var pos = FindIndex(entity);
        if (pos != InvalidIndex) {
            // Replace component
            packed[pos] = component;
            return ref packed[pos];
        }
        pos = packedCount++;
        InsertIndex(entity, pos);
        packedToEntity[pos] = entity;

        if (pos >= packed.Length) {
            Array.Resize(ref packed, packed.Length << 1);
        }
        packed[pos] = component;
        return ref packed[pos];
    }

    // Invalidate this component type for an entity.
    // This function will always succeed, even if the entity does not
    // contain a component of this type.
    //
    // > References returned by `Read` may become invalid after remove is
    // called.
    public void Remove(Entity entity) {
        var removed = FindIndex(entity);
        if (removed == InvalidIndex) {
            // This is a no-op since calling this as a virtual member function
            // means there is no way for the caller to check if the entity
            // contains a component. `contains` is not virtual as it needs to
            // be fast.
            return;
        }
        // Move the last component into the empty slot to keep the array packed
        var last = packedCount - 1;
        packed[removed] = packed[last];

        // Need to know which entity "owns" the component we just moved
        var movedEntity = packedToEntity[last];
        packedToEntity[removed] = movedEntity;

        // Update the entity that has its component moved to reference
        // the new location in the packed array.
        InsertIndex(movedEntity, removed);
        InsertIndex(entity, InvalidIndex);
        packedToEntity.Remove(last);
        --packedCount;
    }

    // Copy component to `dst` from `src`. If the component is a
    // reference object this will only copy the component reference.
    public void Copy(Entity dst, Entity src) {
        Write(dst, Read(src));
    }

    // Returns true if the entity has a component of type T.
    public bool Contains(Entity entity) {
        return FindIndex(entity) != InvalidIndex;
    }

    // Returns the index into the packed array from an Entity
    int FindIndex(Entity entity) {
        var chunk = entity.index / ChunkSize;
        var index = entity.index & (ChunkSize - 1);

        if (chunk >= chunks.Count || chunks[chunk] == null) {
            return InvalidIndex;
        }
        return chunks[chunk][index];
    }

    // Sets the index into the packed array
    void InsertIndex(Entity entity, int value) {
        var chunk = entity.index / ChunkSize;
        var index = entity.index & (ChunkSize - 1);

        if (chunks.Count <= chunk) {
            chunks.Capacity = chunk + 1;
            while (chunks.Count <= chunk) {
                chunks.Add(null);
            }
        }
        if (chunks[chunk] == null) {
            var p = new int[ChunkSize];
            for (int i = 0; i < ChunkSize; ++i)
                p[i] = InvalidIndex;
            chunks[chunk] = p;
        }
        chunks[chunk][index] = value;
    }
}

} // namespace Liquid.Entities
