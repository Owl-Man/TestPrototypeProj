using System.Collections;
using UnityEngine;

namespace UI
{
    public class DisappearingAnimation : MonoBehaviour
    {
        private CanvasGroup _showNewWavesPanel;

        private void Awake() => _showNewWavesPanel = GetComponent<CanvasGroup>();
        
        public void StartDisappearingAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(Disappearing());
        }

        private IEnumerator Disappearing()
        {
            _showNewWavesPanel.alpha = 1;
            
            yield return new WaitForSeconds(2);
            
            while (_showNewWavesPanel.alpha > 0)
            {
                _showNewWavesPanel.alpha -= 0.01f;
                
                yield return null;
            }
            
            gameObject.SetActive(false);
        }
    }
}