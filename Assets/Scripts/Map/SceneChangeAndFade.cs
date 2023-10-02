using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace DecayingMarine
{
    public class SceneChangeAndFade : MonoBehaviour
    {
        [SerializeField] private Image _fadeInScenePanel;
        [SerializeField] private float _fadeInOutDuration = 1f;

        private void Start()
        {
            _fadeInScenePanel.DOFade(0f, _fadeInOutDuration);
        }

        public void RestartScene()
        {
            _fadeInScenePanel.DOFade(1f, _fadeInOutDuration).OnComplete(() => {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            });
        }
    }
}
