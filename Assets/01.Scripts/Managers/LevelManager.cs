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
        public List<MapData> MapDatas;
        public GameObject PlayerToSpawn;
        public GameObject ExistedPlayer;

        public LevelGenerator LevelGenerator;
        public EnemySpawner EnemySpawner;


        public float CurrentThreatLevel;
        protected float _maxThreatLevel;
        protected float _progress;
        protected MapData _mapData;


        public static event UnityAction PlayerSpawned;

        protected virtual void Start()
        {
            ChangeToRandomData();
            StartCoroutine(SpawnPlayer());
            LoadLevel();
        }

        protected virtual void Update()
        {
            IncreaseThreatLevelAndProgress();
        }

        public virtual void IncreaseThreatLevelAndProgress()
        {
            _maxThreatLevel += _mapData.ThreatLevelIncrementPerSecond * Time.deltaTime;
            _progress += _mapData.ThreatLevelIncrementPerSecond * Time.deltaTime;
        }

        public virtual IEnumerator SpawnPlayer()
        {
            if (ExistedPlayer != null)
                Characters.Player = ExistedPlayer.GetComponent<Character>();
            else if (PlayerToSpawn != null)
            {
                Characters.Player = Instantiate(PlayerToSpawn).GetComponent<Character>();
            }
            yield return new WaitForSeconds(1);
            PlayerSpawned?.Invoke();
        }

        protected virtual void LoadLevel()
        {
            //LevelGeneration.LoadData();
            StartCoroutine(EnemySpawner.SpawnRandomEnemies());
        }

        public virtual void ChangeToRandomData()
        {
            MapData randomData = MapDatas[Randomizer.RandomNumber(0, MapDatas.Count)];
            _mapData = Instantiate(randomData);
            LevelGenerator.MapData = _mapData;
            EnemySpawner.MapData = _mapData;

            LevelGenerator.LoadData();
            EnemySpawner.LoadData();

            CurrentThreatLevel = 0;
            _progress = 0;
            _maxThreatLevel = _mapData.StartingThreatLevel;
        }
        public float MaxThreatLevel { get => _maxThreatLevel; }
        public float Progress { get => _progress; }
    }
}
