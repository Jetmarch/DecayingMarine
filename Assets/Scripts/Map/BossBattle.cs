using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class BossBattle : MonoBehaviour
    {
        [SerializeField] private Boss _boss;

        private void Start()
        {
            _boss.OnBossDead.AddListener(Victory);
        }

        private void Victory()
        {
            FindObjectOfType<GameOverScreen>().ShowVictoryScreen();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;

            _boss.StartAction();
        }
    }
}
