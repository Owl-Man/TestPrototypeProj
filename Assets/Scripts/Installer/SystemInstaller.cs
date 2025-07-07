using UnityEngine;
using Zenject;

public class SystemInstaller : MonoInstaller
{
    [SerializeField] private EventHandler eventHandler;
    
    public override void InstallBindings()
    {
        Container.Bind<InputSystem_Actions>()
            .FromNew()
            .AsTransient();
        
        Container.Bind<EventHandler>()
            .WithId("eventHandler")
            .FromInstance(eventHandler)
            .AsCached();
    }
}