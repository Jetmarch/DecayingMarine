using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

namespace DecayingMarine
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        private Health _playerHealth;
        [SerializeField] private float _backgroundAlpha = 0.5f;
        [SerializeField] private float _backgroundRevealDuration = 1f;

        private void Start()
        {
            _playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
            _playerHealth.OnDie.AddListener(ShowGameOverScreen);

            _restartButton.onClick.AddListener(RestartLevel);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void ShowGameOverScreen()
        {
            _gameOverPanel.SetActive(true);
            _gameOverPanel.GetComponent<Image>().DOFade(_backgroundAlpha, _backgroundRevealDuration);

            DisableInputAndEnemies();
        }

        private void DisableInputAndEnemies()
        {
            FindObjectOfType<InputHandler>().enabled = false;
            

            DisableObjectsByType<NavMeshAgent>();
            DisableObjectsByType<MeleeAttacker>();
            DisableObjectsByType<Enemy>();
        }

        private void DisableObjectsByType<T>() where T : Behaviour
        {
            var objects = FindObjectsOfType<T>();
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    objects[i].enabled = false;
                }
            }
        }

        private void RestartLevel()
        {
            FindObjectOfType<SceneChangeAndFade>().RestartScene();
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        public void ShowVictoryScreen()
        {
            _gameOverText.text = "Victory!";
            ShowGameOverScreen();
        }
    }
}
