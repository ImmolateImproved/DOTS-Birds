using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
public partial struct MovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<WorldSize>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var wordlSize = SystemAPI.GetSingleton<WorldSize>();

        var gravity = SystemAPI.GetSingleton<Gravity>();

        new MovementJob
        {
            dt = SystemAPI.Time.DeltaTime,
            gravity = gravity.value

        }.ScheduleParallel();

        new CollisionJob
        {
            worldSize = wordlSize

        }.ScheduleParallel();
    }

    [BurstCompile]
    partial struct MovementJob : IJobEntity
    {
        public float dt;

        public float gravity;

        public void Execute(ref LocalTransform localTransform, in MovemeSpeed movementSpeed, ref Velocity velocity)
        {
            localTransform.Position += movementSpeed.value * velocity.value * dt;
            velocity.value.y -= gravity * dt;
        }
    }

    [BurstCompile]
    partial struct CollisionJob : IJobEntity
    {
        public WorldSize worldSize;

        public void Execute(ref Velocity velocity, in LocalTransform transform)
        {
            var half_width = worldSize.width * 0.5;
            var half_height = worldSize.height * 0.5;

            var xVel = velocity.value.x;
            var yVel = velocity.value.y;
            var xPos = transform.Position.x;
            var yPos = transform.Position.y;

            if ((xVel > 0f && xPos + WorldSize.HALF_BIRD_SIZE > half_width)
                || (xVel <= 0 && xPos - WorldSize.HALF_BIRD_SIZE < -half_width))
            {
                velocity.value.x = -xVel;
            }

            if (yVel < 0f && yPos - WorldSize.HALF_BIRD_SIZE < -half_height)
            {
                velocity.value.y = -yVel;
            }
            if (yVel > 0f && yPos + WorldSize.HALF_BIRD_SIZE > half_height)
            {
                velocity.value.y = 0f;
            }
        }
    }
}