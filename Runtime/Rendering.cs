using UnityEngine;
using UnityEngine.Rendering;
using Unity.Mathematics;
using Liquid.Entities;

namespace Liquid.Rendering {

public struct Position {
    public float3 Value;
}

public struct Rotation {
    public quaternion Value;
}

public struct Scale {
    public float3 Value;
}

// Transformation matrix for an entity. If the entity has any of either
// a Position, Rotation or Scale component the LocalToWorld matrix
// will be populated by the LocalToWorldSystem.
public struct LocalToWorld {
    public float4x4 Value;
}

// A container component for a mesh and material to be rendered.
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

// Populates a `LocalToWorld` matrix component using values from the
// Position, Rotation and Scale components. If an entity does not have
// the Position, Rotation or Scale components this system will not
// modify the `LocalToWorld` matrix so that the matrix can be set directly.
//
// > This system is added by the MeshRendererSystem and does not need to be
// added manually.
public class LocalToWorldSystem : EntitySystem {
    // The system is split into separate operations for each Position, Rotation
    // and Scale component. This way an entity can have any combination of
    // each component and still get it's transformation matrix set.
    public override void Draw(World world) {
        // Reset the transformation matrix only if the entity contains any of
        // either a position, scale or rotation component.
        foreach (var entity in world.View<LocalToWorld>()) {
            if (world.Contains<Position>(entity)
                || world.Contains<Rotation>(entity)
                || world.Contains<Scale>(entity)) {

                ref var m = ref world.Unpack<LocalToWorld>(entity);
                m.Value = float4x4.identity;
            }
        }

        world.Each((ref LocalToWorld m, in Position pos) => {
            m.Value.c3 = math.float4(pos.Value, 1f);
        });

        world.Each((ref LocalToWorld m, in Scale scale) => {
            m.Value.c0 *= math.float4(scale.Value.x, 1f, 1f, 1f);
            m.Value.c1 *= math.float4(1f, scale.Value.y, 1f, 1f);
            m.Value.c2 *= math.float4(1f, 1f, scale.Value.z, 1f);
        });

        world.Each((ref LocalToWorld m, in Rotation rot) => {
            float3x3 r = math.float3x3(rot.Value);
            m.Value.c0 *= math.float4(r.c0, 1f);
            m.Value.c1 *= math.float4(r.c1, 1f);
            m.Value.c2 *= math.float4(r.c2, 1f);
        });
    }
}

// A simple rendering system using `Graphics.DrawMesh` for each
// entity with a `RenderMesh` and a `LocalToWorld` component.
public class MeshRendererSystem : EntitySystem {
    public override void Load(World world) {
        if (world.GetSystem<LocalToWorldSystem>() == null) {
            world.MakeSystemBefore<MeshRendererSystem, LocalToWorldSystem>();
        }
    }

    public override void Draw(World world) {
        world.Each((in LocalToWorld m, in RenderMesh renderMesh) => {
            Graphics.DrawMesh(
                mesh:           renderMesh.mesh,
                matrix:         (Matrix4x4)m.Value,
                material:       renderMesh.material,
                layer:          renderMesh.layer,
                camera:         renderMesh.camera,
                submeshIndex:   renderMesh.subMesh,
                properties:     renderMesh.properties,
                castShadows:    renderMesh.castShadows,
                receiveShadows: renderMesh.receiveShadows);
        });
    }
}

} // namespace Liquid.Rendering
