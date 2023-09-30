using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DecayingMarine
{
    [RequireComponent(typeof(Inventory))]
    public class ItemUser : MonoBehaviour
    {
        [SerializeField] private int _currentItemIndex;
        private Inventory _inventory;

        public int CurrentItemIndex { private set { _currentItemIndex = value; } get { return _currentItemIndex; } }
        public UnityEvent<int> OnCurrentItemIndexChanged;

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
        }

        public void UseCurrentItem()
        {
            Item item = _inventory.GetItem(_currentItemIndex);

            if(item != null)
            {
                item.Use();
            }
        }

        public void ChangeCurrentItem(int newItemIndex)
        {
            _currentItemIndex = newItemIndex;
            OnCurrentItemIndexChanged?.Invoke(_currentItemIndex);
        }
    }
}
