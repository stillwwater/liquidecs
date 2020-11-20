using UnityEngine;
using Unity.Mathematics;
using Liquid.Entities;
using Liquid.Rendering;

// Pure components can be any struct or class.
// For this demo we use structs for components and render using Liquid.Rendering
// instead of using GameObjects since having this many objects in a scene
// would not be great. That said it is possible to use normal GameObjects as
// entities as well.
struct Particle {
    public float lifetime;
    public float3 velocity;
}

// The Emitter component is a regular Unity MonoBehaviour.

// Events can be any struct
struct SpawnEvent {
    public Entity entity;
    public float3 origin;
}

class ParticleSystem : EntitySystem {
    public override void Update(World world) {
        world.Each((Entity entity, ref Position pos, ref Particle p) => {
            var emitter = world.UnpackOne<Emitter>();
            float dt = Time.deltaTime;

            pos.Value += p.velocity * dt;
            p.lifetime -= dt;
            p.velocity.y += emitter.gravity * dt;

            if (p.lifetime <= 0f) {
                // Using event system to spawn a new particle, events are
                // useful to communicate between systems and between a
                // system and the world.
                world.Emit(new SpawnEvent {
                    entity = entity,
                    origin = emitter.transform.position
                });
            }
        });
    }
}

// System to update the particle emitter position based on the mouse position
class MoveSystem : EntitySystem {
    public override void Update(World world) {
        world.Each((Transform tf, Emitter emitter) => {
            float3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tf.position = math.float3(pos.xy, 0f);
        });
    }
}

// A World inherits from MonoBehaviour, you can add a Demo component to an
// empty GameObject to run the world. The World base class defines the Update
// method, but you can also define update yourself here.
//
// You can have multiple Worlds, usually it makes sense to have one World per
// scene.
class Demo : World {
    [SerializeField] GameObject emitterPrefab = default;
    [SerializeField] Mesh mesh = default;
    [SerializeField] Material mat = default;

    // Configuration
    [SerializeField] float minLifetime = 1f;
    [SerializeField] float maxLifetime = 5f;
    [SerializeField] int maxParticles  = 500;
    [SerializeField] float maxSpeed    = 4f;
    [SerializeField] float minSize     = 0.2f;
    [SerializeField] float maxSize     = 0.6f;
    [SerializeField] float gravity     = -9.8f;

    void Start() {
        // Systems execute in the order in which they are added
        MakeSystem<MoveSystem>();
        MakeSystem<ParticleSystem>();

        // This system is a way to render pure entities, if you are using a
        // GameObject entity you can use the standard MeshRenderer and
        // SpriteRenderer components.
        MakeSystem<MeshRendererSystem>();

        // Binds an event handler
        Bind((SpawnEvent e) => {
            SpawnParticle(e.entity, e.origin);
            return true;
        });

        // Non pure entities can be instantiated with `InstantiateEntity`
        // while pure entities are created with `MakeEntity`. The only difference
        // is that `InstantiateEntity` also adds a reference to the GameObject
        // and Transform objects as components. Prefabs can also be used.
        var emitterEntity = InstantiateEntity(emitterPrefab);

        // When unpacking components that are reference objects (class) there is
        // no need for the `ref` keyword.
        var emitter = Unpack<Emitter>(emitterEntity);

        emitter.transform.position = transform.position;
        emitter.gravity = gravity;

        for (int i = 0; i < maxParticles; ++i) {
            // Create a pure entity without a GameObject or transform attached
            var particleEntity = MakeEntity();
            SpawnParticle(particleEntity, emitter.transform.position);
        }
    }

    void SpawnParticle(Entity entity, float3 origin) {
        float2 v = UnityEngine.Random.insideUnitCircle * maxSpeed;
        float3 scale = math.float3(UnityEngine.Random.Range(minSize, maxSize));
        float lifetime = UnityEngine.Random.Range(minLifetime, maxLifetime);

        // This Pack overload is used to pack pure components (non MonoBehaviour).
        // There is also an overload of Pacck that will call GameObject.AddComponent
        //
        // The RenderMesh and LocalToWorld are the only required components to
        // get entities rendering with the MeshRendererSystem.
        Pack(entity,
             new RenderMesh { mesh = mesh, material = mat },
             new LocalToWorld { },
             new Position { Value = origin },
             new Scale { Value = scale },
             new Particle { lifetime = lifetime, velocity = math.float3(v, 0f) });
    }
}
