using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[System.Serializable]
public struct Spawner : IComponentData
{
    public Entity prefab;
    public int spawnPerKeyPress;

    public float minXVelocity;
    public float maxXVelocity;

    public Random random;
}

[System.Serializable]
public struct Counter : IComponentData
{
    public int currentCount;
}

[System.Serializable]
public struct WorldSize : IComponentData
{
    public float width;
    public float height;

    public const float HALF_BIRD_SIZE = 0.5f;
}

public struct Gravity : IComponentData
{
    public float value;
}