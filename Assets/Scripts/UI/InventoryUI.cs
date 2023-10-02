using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DecayingMarine
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _inventoryWeightText;
        [SerializeField] private Image _inventoryWeightImage;
        [SerializeField] private TextMeshProUGUI _tooHeavyInventoryMessage;
        [SerializeField] private ItemUI[] _itemsUI;

        [SerializeField] private float _durationOfShakeAnimationForItemsUpdated = 0.3f;
        [SerializeField] private float _strengthOfShakeAnimationForItemsUpdated = 4f;
        [SerializeField] private float _durationOfShakeAnimationForTooHeavyMessage = 0.3f;
        [SerializeField] private float _strengthOfShakeAnimationForTooHeavyMessage = 8f;
        private Inventory _playerInventory;
        private ItemUser _itemUser;

        private void Start()
        {
            _playerInventory = FindObjectOfType<Inventory>();
            //_itemsUI = new ItemUI[_playerInventory.MaxItemCount];
            _playerInventory.OnItemsUpdate.AddListener(OnItemsUpdate);
            _playerInventory.OnTooHeavyInventory.AddListener(OnTooHeavyInventory);
            _playerInventory.OnOkInventoryWeight.AddListener(OnOkInventoryWeight);

            _itemUser = FindObjectOfType<ItemUser>();
            _itemUser.OnCurrentItemIndexChanged.AddListener(OnCurrentItemIndexChanged);

            //_itemsUI = FindObjectsOfType<ItemUI>();
            OnCurrentItemIndexChanged(0);
            OnItemsUpdate();
            OnOkInventoryWeight();
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
            _inventoryWeightImage.transform.DOShakePosition(_durationOfShakeAnimationForItemsUpdated, _strengthOfShakeAnimationForItemsUpdated);
        }

        private void OnTooHeavyInventory()
        {
            _inventoryWeightImage.transform.DOShakePosition(_durationOfShakeAnimationForTooHeavyMessage, _strengthOfShakeAnimationForTooHeavyMessage);
            _tooHeavyInventoryMessage.gameObject.SetActive(true);
        }

        private void OnOkInventoryWeight()
        {
            _tooHeavyInventoryMessage.gameObject.SetActive(false);
        }
    }
}
