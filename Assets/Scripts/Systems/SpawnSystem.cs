using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ref var spawner = ref SystemAPI.GetSingletonRW<Spawner>().ValueRW;
            var wordlSize = SystemAPI.GetSingleton<WorldSize>();
            var counter = SystemAPI.GetSingletonRW<Counter>();

            var currentCount = counter.ValueRO.currentCount;

            var position = default(float3);

            position.x = (wordlSize.width / -2f) + WorldSize.HALF_BIRD_SIZE;
            position.y = (wordlSize.height / 2f) - WorldSize.HALF_BIRD_SIZE;

            var entities = state.EntityManager.Instantiate(spawner.prefab, spawner.spawnPerKeyPress, Allocator.Temp);

            for (int i = 0; i < spawner.spawnPerKeyPress; i++)
            {
                var velocityX = spawner.random.NextFloat(spawner.minXVelocity, spawner.maxXVelocity);
                position.z = (currentCount + spawner.spawnPerKeyPress) * 0.00001f;

                state.EntityManager.SetComponentData(entities[i], LocalTransform.FromPosition(position));
                state.EntityManager.SetComponentData(entities[i], new Velocity
                {
                    value = new float3(velocityX, 0, 0)
                });

                state.EntityManager.SetComponentData(entities[i], new URPMaterialPropertyBaseColor
                {
                    Value = (Vector4)Color.HSVToRGB(spawner.random.NextFloat(), 1, 1)
                });
            }

            counter.ValueRW.currentCount += spawner.spawnPerKeyPress;
        }
    }
}