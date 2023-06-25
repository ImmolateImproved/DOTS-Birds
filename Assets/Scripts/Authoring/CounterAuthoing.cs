using Unity.Entities;
using UnityEngine;

public class CounterAuthoing : MonoBehaviour
{
    public Counter counter;

    class CounterBaker : Baker<CounterAuthoing>
    {
        public override void Bake(CounterAuthoing authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, authoring.counter);
        }
    }
}