using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] enemiesMovePoints;
    
    [SerializeField] private GameObject enemiesExplosionEffectPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<GameObject>()
            .WithId("planePrefab")
            .FromInstance(planePrefab)
            .AsCached();
        
        Container.Bind<Transform>()
            .WithId("spawnPoint")
            .FromInstance(spawnPoint)
            .AsCached();
        
        Container.Bind<Transform[]>()
            .WithId("enemiesMovePoints")
            .FromInstance(enemiesMovePoints)
            .AsCached();
        
        Container.Bind<GameObject>()
            .WithId("enemiesExplosionEffectPrefab")
            .FromInstance(enemiesExplosionEffectPrefab)
            .AsCached();
        
        Container.BindInterfacesAndSelfTo<WavesManager>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}