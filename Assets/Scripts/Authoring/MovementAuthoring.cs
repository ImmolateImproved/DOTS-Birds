using Unity.Entities;
using UnityEngine;

public class MovementAuthoring : MonoBehaviour
{
    public float moveSpeed;

    class MovementBaker : Baker<MovementAuthoring>
    {
        public override void Bake(MovementAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new MovemeSpeed
            {
                value = authoring.moveSpeed,
            });

            AddComponent(entity, new Velocity
            {
                value = Vector3.down * authoring.moveSpeed,
            });
        }
    }
}