using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        public List<MapData> MapDatas;
        public GameObject PlayerToSpawn;
        public GameObject ExistedPlayer;

        public LevelGeneration LevelGeneration;
        public EnemySpawner EnemySpawner;


        public float ThreatLevel;
        protected float _maxThreatLevel;


        public static event UnityAction PlayerSpawned;

        protected virtual void Start()
        {
            ChangeToRandomData();
            SpawnPlayer();
            LoadLevel();
        }

        public virtual void SpawnPlayer()
        {
            if (ExistedPlayer != null)
                Characters.Player = ExistedPlayer.GetComponent<Character>();
            else if (PlayerToSpawn != null)
            {
                Characters.Player = Instantiate(PlayerToSpawn).GetComponent<Character>();
            }
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
            LevelGeneration.MapData = randomData;
            EnemySpawner.MapData = randomData;

            LevelGeneration.LoadData();
            EnemySpawner.LoadData();

            ThreatLevel = 0;
            _maxThreatLevel = randomData.StartingThreatLevel;
        }
        public float MaxThreatLevel { get => _maxThreatLevel; }
    }
}
