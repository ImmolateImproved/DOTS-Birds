using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public Spawner spawner;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            authoring.spawner.random = new Unity.Mathematics.Random(53);
            authoring.spawner.prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);
            AddComponent(entity, authoring.spawner);
        }
    }
}