using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DecayingMarine
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _emptyIcon;
        [SerializeField] private GameObject _activeIcon;

        public void SetItem(Item item)
        {
            if (item == null)
            {
                _icon.sprite = _emptyIcon;
            }
            else
            {
                _icon.sprite = item.Icon;
            }
        }

        public void SetActive()
        {
            _activeIcon.SetActive(true);
        }

        public void SetInactive()
        {
            _activeIcon.SetActive(false);
        }
    }
}
