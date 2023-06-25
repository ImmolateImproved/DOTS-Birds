using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct MovemeSpeed : IComponentData
{
    public float value;
}

public struct Velocity : IComponentData
{
    public float3 value;
}