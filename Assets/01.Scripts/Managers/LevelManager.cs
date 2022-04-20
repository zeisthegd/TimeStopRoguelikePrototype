using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.Tools;
using NaughtyAttributes;


namespace Penwyn.Game
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        [Header("Map Datas")]
        public List<MapData> MapDatas;
        [Header("Player")]
        public GameObject PlayerToSpawn;
        public GameObject ExistedPlayer;

        [Header("Sub-components")]
        public LevelGenerator LevelGenerator;
        public EnemySpawner EnemySpawner;

        [Header("Threat Level")]
        public float CurrentThreatLevel;
        protected float _maxThreatLevel;
        protected float _progress;
        protected MapData _mapData;


        public static event UnityAction PlayerSpawned;

        protected virtual void Start()
        {
            ChangeToRandomData();
            SpawnPlayer();
            LoadLevel();
            InputReader.Instance.EnableGameplayInput();
        }

        protected virtual void Update()
        {
            IncreaseThreatLevelAndProgress();
        }

        /// <summary>
        /// Increase the max threat level of enemies.
        /// Increase the level progress.
        /// </summary>
        public virtual void IncreaseThreatLevelAndProgress()
        {
            _maxThreatLevel += _mapData.ThreatLevelIncrementPerSecond * Time.deltaTime;
            _progress += _mapData.ThreatLevelIncrementPerSecond * Time.deltaTime;
        }

        /// <summary>
        /// Spawn player if they are not existed.
        /// </summary>
        public virtual void SpawnPlayer()
        {
            if (ExistedPlayer != null)
                Characters.Player = ExistedPlayer.GetComponent<Character>();
            else if (PlayerToSpawn != null)
                Characters.Player = Instantiate(PlayerToSpawn).GetComponent<Character>();
            PlayerSpawned?.Invoke();
        }

        /// <summary>
        /// Generate the level and spawn the enemies.
        /// </summary>
        protected virtual void LoadLevel()
        {
            LevelGenerator.GenerateLevel();
            StartCoroutine(EnemySpawner.SpawnRandomEnemies());
        }

        /// <summary>
        /// Change the level's data to a random one of the list.
        /// </summary>
        public virtual void ChangeToRandomData()
        {
            MapData randomData = MapDatas[Randomizer.RandomNumber(0, MapDatas.Count)];
            _mapData = Instantiate(randomData);
            LevelGenerator.MapData = _mapData;
            EnemySpawner.MapData = _mapData;

            EnemySpawner.LoadData();

            CurrentThreatLevel = 0;
            _progress = 0;
            _maxThreatLevel = _mapData.StartingThreatLevel;
        }

        public virtual void MovePlayerTo(Vector2 position)
        {
            Characters.Player.transform.position = position;
        }

        public float MaxThreatLevel { get => _maxThreatLevel; }
        public float Progress { get => _progress; }
    }
}
