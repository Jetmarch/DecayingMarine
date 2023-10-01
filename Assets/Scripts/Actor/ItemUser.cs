using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

namespace DecayingMarine
{
    [RequireComponent(typeof(Inventory))]
    public class ItemUser : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private int _currentItemIndex;
        private Inventory _inventory;

        public int CurrentItemIndex { private set { _currentItemIndex = value; } get { return _currentItemIndex; } }
        public UnityEvent<int> OnCurrentItemIndexChanged;

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            _inventory.OnItemsUpdate.AddListener(OnItemsUpdate);
        }

        public void UseCurrentItem()
        {
            Item item = _inventory.GetItem(_currentItemIndex);

            if(item != null)
            {
                item.Use();

                if(item.IsDisposable)
                {
                    _inventory.RemoveItem(_currentItemIndex);
                }
            }
        }

        public void ChangeCurrentItem(int newItemIndex)
        {
            _currentItemIndex = newItemIndex;
            HideOtherItems();
            OnCurrentItemIndexChanged?.Invoke(_currentItemIndex);
        }

        public void DropCurrentItem()
        {
            Item item = _inventory.GetItem(_currentItemIndex);
            if (item == null) return;
            item.gameObject.transform.SetParent(null);
            item.gameObject.transform.Translate(transform.forward);
            _inventory.RemoveItem(_currentItemIndex);
        }

        private void OnItemsUpdate()
        {
            HideOtherItems();
        }

        private void HideOtherItems()
        {
            foreach(Transform item in _itemsParent)
            {
                item.GetComponent<Item>().SetActiveAndVisible(false);
            }

            Item selectedItem = _inventory.GetItem(_currentItemIndex);
            if(selectedItem != null)
            {
                selectedItem.GetComponent<Item>().SetActiveAndVisible(true);
            }
        }
    }
}
