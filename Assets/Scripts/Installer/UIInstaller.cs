using TMPro;
using UI;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private CanvasGroup showNewWavesPanel;
        [SerializeField] private TMP_Text wavesCountText;
        [SerializeField] private GameObject showWinPanel;

        public override void InstallBindings()
        {
            Container.Bind<CanvasGroup>()
                .WithId("showNewWavesPanel")
                .FromInstance(showNewWavesPanel)
                .AsCached();
            
            Container.Bind<TMP_Text>()
                .WithId("wavesCountText")
                .FromInstance(wavesCountText)
                .AsCached();
            
            Container.Bind<GameObject>()
                .WithId("showWinPanel")
                .FromInstance(showWinPanel)
                .AsCached();
            
            Container.BindInterfacesAndSelfTo<LevelUIVisualizer>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}