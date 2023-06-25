using Unity.Entities;
using UnityEngine;

public class WorldSettingsAuthoring : MonoBehaviour
{
    public WorldSize worldSize;
    public float gravity;

    class WorldSizeBaker : Baker<WorldSettingsAuthoring>
    {
        public override void Bake(WorldSettingsAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, authoring.worldSize);
            AddComponent(entity, new Gravity
            {
                value = authoring.gravity
            });
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(worldSize.width, worldSize.height));
    }
}