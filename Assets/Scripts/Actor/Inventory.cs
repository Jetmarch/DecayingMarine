using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DecayingMarine
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Item[] _itemArray;
        [SerializeField] private int _maxItemCount;
        [SerializeField] private float _maxWeight = 100f;
        [SerializeField] private float _currentItemsWeight;
        private ItemParent _itemParent;

        public int MaxItemCount { private set { _maxItemCount = value; } get { return _maxItemCount; } }
        public float MaxWeight { get { return _maxWeight; } }
        public float CurrentItemWeight { get { return _currentItemsWeight; } }

        public UnityEvent OnItemsUpdate;
        public UnityEvent OnTooHeavyInventory;
        public UnityEvent OnOkInventoryWeight;

        private void Start()
        {
            _itemArray = new Item[_maxItemCount];
            _itemParent = FindObjectOfType<ItemParent>();
            //OnSetMaxItemCount?.Invoke(_maxItemCount);
        }

        private int FindFirstEmptySlot()
        {
            for(int i = 0; i < _maxItemCount; i++)
            {
                if(_itemArray[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        private void CalculateItemsWeight()
        {
            _currentItemsWeight = 0;
            for (int i = 0; i < _itemArray.Length; i++)
            {
                if (_itemArray[i] == null) continue;
                _currentItemsWeight += _itemArray[i].Weight;
            }

            if (_currentItemsWeight > _maxWeight)
            {
                OnTooHeavyInventory?.Invoke();
            }
            else
            {
                OnOkInventoryWeight?.Invoke();
            }
        }

        public Item GetItem(int itemPos)
        {
            return _itemArray[itemPos];
        }

        public bool AddItem(Item item)
        {
            if (_currentItemsWeight + item.Weight > _maxWeight) return false;

            int emptySlot = FindFirstEmptySlot();
            if (emptySlot == -1) return false;

            _itemArray[emptySlot] = item;
            _itemArray[emptySlot].transform.SetParent(_itemParent.transform);
            _itemArray[emptySlot].transform.position = _itemParent.transform.position + (_itemParent.transform.forward * .5f);
            _itemArray[emptySlot].transform.rotation = _itemParent.transform.rotation;
            CalculateItemsWeight();
            OnItemsUpdate?.Invoke();
            return true;
        }

        public void RemoveItem(int index)
        {
            _itemArray[index] = null;
            CalculateItemsWeight();
            OnItemsUpdate?.Invoke();
        }

        public void DecreaseMaxWeight(float amount)
        {
            _maxWeight -= amount;
            _maxWeight = (int)Mathf.Clamp(_maxWeight, 0f, 100f);
            CalculateItemsWeight();

            OnItemsUpdate?.Invoke();
        }

    }
}
