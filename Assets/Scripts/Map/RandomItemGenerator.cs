using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class RandomItemGenerator : MonoBehaviour
    {
        public static RandomItemGenerator instance;
        [SerializeField] private GameObject[] _itemsToSpawn;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        public GameObject GetRandomItem()
        {
            int randomItemIndex = Random.Range(0, _itemsToSpawn.Length);
            return _itemsToSpawn[randomItemIndex];
        }
    }
}
