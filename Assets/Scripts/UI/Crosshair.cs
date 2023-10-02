using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private Texture2D _cursorTexture;
        private Vector2 _hotSpot = Vector2.zero;

        private void Start()
        {
            Cursor.SetCursor(_cursorTexture, _hotSpot, CursorMode.Auto);
        }
    }
}
