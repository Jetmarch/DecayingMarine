using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DecayingMarine
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _inventoryWeightText;
        [SerializeField] private Image _inventoryWeightImage;
        [SerializeField] private ItemUI[] _itemsUI;
        private Inventory _playerInventory;
        private ItemUser _itemUser;

        private void Start()
        {
            _playerInventory = FindObjectOfType<Inventory>();
            //_itemsUI = new ItemUI[_playerInventory.MaxItemCount];
            _playerInventory.OnItemsUpdate.AddListener(OnItemsUpdate);

            _itemUser = FindObjectOfType<ItemUser>();
            _itemUser.OnCurrentItemIndexChanged.AddListener(OnCurrentItemIndexChanged);

            //_itemsUI = FindObjectsOfType<ItemUI>();
            OnCurrentItemIndexChanged(0);
            OnItemsUpdate();
        }

        private void OnCurrentItemIndexChanged(int newItemIndex)
        {
            for(int i = 0; i < _itemsUI.Length; i++)
            {
                _itemsUI[i].SetInactive();
            }

            _itemsUI[newItemIndex].SetActive();
        }

        private void OnItemsUpdate()
        {
            for(int i = 0; i < _itemsUI.Length; i++)
            {
                _itemsUI[i].SetItem(_playerInventory.GetItem(i));
            }

            _inventoryWeightText.text = $"{_playerInventory.CurrentItemWeight} / {_playerInventory.MaxWeight} ";
            _inventoryWeightImage.fillAmount = _playerInventory.CurrentItemWeight / _playerInventory.MaxWeight;
        }
    }
}
