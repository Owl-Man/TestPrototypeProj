using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelUIVisualizer : IInitializable
    {
        [Inject(Id = "showNewWavesPanel")]
        private CanvasGroup _showNewWavesPanel;
        private DisappearingAnimation _showNewWavesPanelAnim;
        
        [Inject(Id = "showWinPanel")]
        private GameObject _showWinPanel;
        
        [Inject(Id = "wavesCountText")]
        private TMP_Text _wavesCountText;
        
        [Inject(Id = "eventHandler")]
        private EventHandler _eventHandler;
        
        private WavesManager _wavesManager;
        
        [Inject]
        public void Construct(WavesManager wavesManager)
        {
            _wavesManager = wavesManager;
        }

        public void Initialize()
        {
            _eventHandler.onStartNewWave += ShowStartNewWavePanel;

            _eventHandler.onWin += ShowWinPanel;
            
            _showNewWavesPanelAnim = _showNewWavesPanel.GetComponent<DisappearingAnimation>();
        }
        
        private void ShowStartNewWavePanel()
        {
            _showNewWavesPanel.gameObject.SetActive(true);
            _wavesCountText.text = $"{_wavesManager.CurrentWave}/{_wavesManager.CountOfWaves}";

            _showNewWavesPanelAnim.StartDisappearingAnimation();
        }
        
        private void ShowWinPanel()
        {
            _showWinPanel.SetActive(true);
        }
    }
}