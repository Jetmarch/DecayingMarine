using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    [RequireComponent(typeof(Actor))]
    public class InputHandler : MonoBehaviour
    {
        private Actor _player;


        private Vector3 _moveVector;
        private Vector3 _lookVector;
        private Camera _mainCamera;
        private RaycastHit _hitInfo;
        private void Start()
        {
            _mainCamera = Camera.main;
            _player = GetComponent<Actor>();
        }

        private void Update()
        {
            _moveVector.x = Input.GetAxis("Horizontal");
            _moveVector.y = Input.GetAxis("Vertical");

            SetLookVector();


            _player.Move(_moveVector);
            _player.LookAt(_lookVector);

            CheckItemUse();
            CheckDash();
            CheckPunch();
            CheckItemChange();
            CheckDropItem();

            ChecKExitFromGame();

            CheckRestart();
        }

        private void CheckRestart()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                FindObjectOfType<SceneChangeAndFade>().RestartScene();
            }
        }

        private void ChecKExitFromGame()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void SetLookVector()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
            {
                _lookVector = _hitInfo.point;
            }
        }

        private void CheckPunch()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.Punch();
            }
        }

        private void CheckItemUse()
        {
            if (Input.GetMouseButton(0))
            {
                _player.UseCurrentItem();
            }
        }

        private void CheckDropItem()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                _player.DropCurrentItem();
            }
        }

        private void CheckDash()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _player.Dash();
            }
        }

        private void CheckItemChange()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                _player.ChangeCurrentItem(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _player.ChangeCurrentItem(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _player.ChangeCurrentItem(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _player.ChangeCurrentItem(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _player.ChangeCurrentItem(4);
            }
        }
    }
}
