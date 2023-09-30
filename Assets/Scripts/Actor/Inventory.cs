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
        [SerializeField] private ItemParent _itemParent;

        public int MaxItemCount { private set { _maxItemCount = value; } get { return _maxItemCount; } }
        public UnityEvent OnItemsUpdate;

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

        public Item GetItem(int itemPos)
        {
            return _itemArray[itemPos];
        }

        public bool AddItem(Item item)
        {
            int emptySlot = FindFirstEmptySlot();
            if (emptySlot == -1) return false;

            _itemArray[emptySlot] = item;
            _itemArray[emptySlot].transform.SetParent(_itemParent.transform);
            _itemArray[emptySlot].transform.position = _itemParent.transform.position + (_itemParent.transform.forward * .5f);
            _itemArray[emptySlot].transform.rotation = _itemParent.transform.rotation;
            OnItemsUpdate?.Invoke();
            return true;
        }

        public void RemoveItem(int index)
        {
            _itemArray[index] = null;
            OnItemsUpdate?.Invoke();
        }

    }
}
