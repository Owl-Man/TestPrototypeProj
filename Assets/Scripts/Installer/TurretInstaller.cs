using Player;
using UnityEngine;
using Zenject;

public class TurretInstaller : MonoInstaller
{
    [SerializeField] private Transform turretTransform;
    [SerializeField] private Transform turretGunTransform;
    [SerializeField] private Transform gunpointTransform;
    
    [SerializeField] private GameObject bulletPrefab;
    
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TurretTouchController>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        
        Container.BindInterfacesAndSelfTo<ShootingController>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<Transform>()
            .WithId("turretTransform")
            .FromInstance(turretTransform)
            .AsCached();
        
        Container.Bind<Transform>()
            .WithId("turretGunTransform")
            .FromInstance(turretGunTransform)
            .AsCached();
        
        Container.Bind<Transform>()
            .WithId("gunpointTransform")
            .FromInstance(gunpointTransform)
            .AsCached();
        
        Container.Bind<GameObject>()
            .WithId("bulletPrefab")
            .FromInstance(bulletPrefab)
            .AsCached();
    }
}