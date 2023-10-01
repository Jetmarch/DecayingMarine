using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class LabyrintGenerator : MonoBehaviour
    {
        [SerializeField] private int _maxLevelParts = 10;
        [SerializeField] private int _minLevelParts = 7;
        [SerializeField] private int _currentCountOfLevelParts;

        [SerializeField] private float _xLevelPartOffset = 45;
        [SerializeField] private float _zLevelPartOffset = 30.2f;

        [SerializeField] private LevelPart _startLevelPart;
        [SerializeField] private Transform _levelPartsParent;

        [Header("Level part prefabs")]
        [SerializeField] private GameObject _enemyLevelPartPrefab;
        [SerializeField] private GameObject _healLevelPartPrefab;
        [SerializeField] private int _howManyRoomsWithoutHealLevelPart;
        [SerializeField] private GameObject _bossLevelPartPrefab;

        private Vector3 _currentLevelPartPosition;
        private LevelPart _currentLevelPart;
        private List<LevelPart> _instantiadedLevelParts;
        private int _maxGenerateIterations = 200;
        private int _currentCountOfRoomsWithoutHealLevelPart;

        private NavMeshBaker _navMeshBaker;

        private void Awake()
        {
            _instantiadedLevelParts = new List<LevelPart>();
            //_levelParts = new GameObject[_minLevelParts];
        }

        private void Start()
        {
            GenerateLabyrinth();
            _navMeshBaker = GetComponent<NavMeshBaker>();
            _navMeshBaker.BuildNavMesh();
        }

        public void GenerateLabyrinth()
        {

            /* 
             * Positions of level parts:
             * Top - 0
             * Bottom - 1
             * Right - 2
             * Left - 3
             */
            _currentLevelPartPosition = Vector3.zero;
            _currentCountOfLevelParts = Random.Range(_minLevelParts, _maxLevelParts);
            _currentLevelPart = _startLevelPart;

            int i = 0;
            while (_instantiadedLevelParts.Count < _currentCountOfLevelParts && i < _maxGenerateIterations)
            {
                int nextLevelPartPosition = Random.Range(0, 4);
                switch (nextLevelPartPosition)
                {
                    case 0:
                        PlaceTopLevelPart(GetRandomLevelPartPrefab());
                        break;
                    case 1:
                        PlaceBottomLevelPart(GetRandomLevelPartPrefab());
                        break;
                    case 2:
                        PlaceRightLevelPart(GetRandomLevelPartPrefab());
                        break;
                    case 3:
                        PlaceLeftLevelPart(GetRandomLevelPartPrefab());
                        break;
                }
                i++;
            }
            PlaceBossRoom();
        }

        private LevelPart GetLevelPartInCurrentPosition()
        {
            Collider[] colliders = Physics.OverlapSphere(_currentLevelPartPosition, 2f);
            for(int i = 0; i < colliders.Length; i++)
            {
                var levelPart = colliders[i].GetComponent<LevelPart>();
                if(levelPart != null)
                {
                    return levelPart;
                }
            }

            return null;
        }

        private void PlaceBossRoom()
        {
            bool isBossRoomCreated = false;
            int countOfIterations = 0;
            while (!isBossRoomCreated && countOfIterations < _maxGenerateIterations)
            {
                //Move on the top from last position to create boss room
                _currentLevelPart.OpenDoor(DoorPosition.Top);
                _currentLevelPartPosition.z += _zLevelPartOffset;
                LevelPart levelPart = GetLevelPartInCurrentPosition();
                if (levelPart != null)
                {
                    levelPart.OpenDoor(DoorPosition.Top);
                    continue;
                }
                else
                {
                    //levelPart = Instantiate(_healLevelPartPrefab, _currentLevelPartPosition, Quaternion.identity, _levelPartsParent).GetComponent<LevelPart>();
                    //_instantiadedLevelParts.Add(levelPart);
                    //levelPart.OpenDoor(DoorPosition.Bottom);
                    //_currentLevelPart = levelPart;
                    //_currentLevelPart.OpenDoor(DoorPosition.Top);
                    //_currentLevelPartPosition.z += _zLevelPartOffset;

                    levelPart = Instantiate(_bossLevelPartPrefab, _currentLevelPartPosition, Quaternion.identity, _levelPartsParent).GetComponent<LevelPart>();
                    _instantiadedLevelParts.Add(levelPart);
                    levelPart.OpenDoor(DoorPosition.Bottom);
                    _currentLevelPart = levelPart;
                    isBossRoomCreated = true;
                }
                countOfIterations++;
            }
        }

        private GameObject GetRandomLevelPartPrefab()
        {
            if(_currentCountOfRoomsWithoutHealLevelPart >= _howManyRoomsWithoutHealLevelPart)
            {
                _currentCountOfRoomsWithoutHealLevelPart = 0;
                return _healLevelPartPrefab;
            }

            _currentCountOfRoomsWithoutHealLevelPart++;
            return _enemyLevelPartPrefab;
        }

        private void PlaceTopLevelPart(GameObject levelPartPrefab)
        {
            _currentLevelPart.OpenDoor(DoorPosition.Top);
            _currentLevelPartPosition.z += _zLevelPartOffset;

            LevelPart levelPart = GetLevelPartInCurrentPosition();
            if (levelPart == null)
            {
                levelPart = Instantiate(levelPartPrefab, _currentLevelPartPosition, Quaternion.identity, _levelPartsParent).GetComponent<LevelPart>();
                _instantiadedLevelParts.Add(levelPart);
            }
            levelPart.OpenDoor(DoorPosition.Bottom);
            _currentLevelPart = levelPart;
        }

        private void PlaceBottomLevelPart(GameObject levelPartPrefab)
        {
            _currentLevelPart.OpenDoor(DoorPosition.Bottom);
            _currentLevelPartPosition.z -= _zLevelPartOffset;

            LevelPart levelPart = GetLevelPartInCurrentPosition();
            if (levelPart == null)
            {
                levelPart = Instantiate(levelPartPrefab, _currentLevelPartPosition, Quaternion.identity, _levelPartsParent).GetComponent<LevelPart>();
                _instantiadedLevelParts.Add(levelPart);
            }
            levelPart.OpenDoor(DoorPosition.Top);
            _currentLevelPart = levelPart;
        }

        private void PlaceRightLevelPart(GameObject levelPartPrefab)
        {
            _currentLevelPart.OpenDoor(DoorPosition.Right);
            _currentLevelPartPosition.x += _xLevelPartOffset;

            LevelPart levelPart = GetLevelPartInCurrentPosition();
            if (levelPart == null)
            {
                levelPart = Instantiate(levelPartPrefab, _currentLevelPartPosition, Quaternion.identity, _levelPartsParent).GetComponent<LevelPart>();
                _instantiadedLevelParts.Add(levelPart);
            }
            levelPart.OpenDoor(DoorPosition.Left);
            _currentLevelPart = levelPart;
        }

        private void PlaceLeftLevelPart(GameObject levelPartPrefab)
        {
            _currentLevelPart.OpenDoor(DoorPosition.Left);
            _currentLevelPartPosition.x -= _xLevelPartOffset;

            LevelPart levelPart = GetLevelPartInCurrentPosition();
            if (levelPart == null)
            {
                levelPart = Instantiate(levelPartPrefab, _currentLevelPartPosition, Quaternion.identity, _levelPartsParent).GetComponent<LevelPart>();
                _instantiadedLevelParts.Add(levelPart);
            }
            levelPart.OpenDoor(DoorPosition.Right);
            _currentLevelPart = levelPart;
        }
    }
}
