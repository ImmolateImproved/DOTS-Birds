using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
[UpdateAfter(typeof(SpawnSystem))]
public partial class CounterSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var uiManager = UIManager.inst;

        Entities.ForEach((in Counter counter) =>
        {
            uiManager.counterText.text = $"Bird count: {counter.currentCount} \nFPS: {(1 / SystemAPI.Time.DeltaTime):00}";

        }).WithoutBurst().Run();
    }
}
