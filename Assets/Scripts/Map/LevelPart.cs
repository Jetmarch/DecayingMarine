using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class LevelPart : MonoBehaviour
    {
        [SerializeField] private Door _topDoor;
        [SerializeField] private Door _bottomDoor;
        [SerializeField] private Door _rightDoor;
        [SerializeField] private Door _leftDoor;

        private void Start()
        {
            //_topDoor.Close();
            //_bottomDoor.Close();
            //_rightDoor.Close();
            //_leftDoor.Close();
        }

        public void OpenDoor(DoorPosition position)
        {
            switch(position)
            {
                case DoorPosition.Top:
                    _topDoor.Open();
                    break;
                case DoorPosition.Bottom:
                    _bottomDoor.Open();
                    break;
                case DoorPosition.Right:
                    _rightDoor.Open();
                    break;
                case DoorPosition.Left:
                    _leftDoor.Open();
                    break;
            }
        }

        public void CloseDoor(DoorPosition position)
        {
            switch (position)
            {
                case DoorPosition.Top:
                    _topDoor.Close();
                    break;
                case DoorPosition.Bottom:
                    _bottomDoor.Close();
                    break;
                case DoorPosition.Right:
                    _rightDoor.Close();
                    break;
                case DoorPosition.Left:
                    _leftDoor.Close();
                    break;
            }
        }
    }

    public enum DoorPosition
    {
        Top,
        Bottom,
        Right,
        Left
    }
}

