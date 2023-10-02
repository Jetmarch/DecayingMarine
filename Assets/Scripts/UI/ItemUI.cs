using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DecayingMarine
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _emptyIcon;
        [SerializeField] private GameObject _activeIcon;

        [SerializeField] private float _durationOfShakeAnimationForItemChanged = 0.3f;
        [SerializeField] private float _strengthOfShakeAnimationForItemChanged = 4f;

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
            _activeIcon.transform.DOShakePosition(_durationOfShakeAnimationForItemChanged, _strengthOfShakeAnimationForItemChanged);
        }

        public void SetInactive()
        {
            _activeIcon.SetActive(false);
        }
    }
}
