using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class EnemySpawner : MonoBehaviour
    {
        public ObjectPooler EnemyPoolPrefab;
        [ReadOnly] public List<ObjectPooler> ObjectPoolers;
        public MapData MapData;

        [Header("Settings")]
        public float MinDistanceToPlayer;
        public float MaxDistanceToPlayer;
        public float TimeTillSpawnNewEnemies = 2;


        protected float _waitToSpawnTime = 0;

        protected virtual void Update()
        {
            WaitToSpawnEnemy();
        }

        public virtual void LoadData()
        {
            CreateEnemyPools();
        }

        protected virtual void CreateEnemyPools()
        {
            ObjectPoolers = new List<ObjectPooler>();
            foreach (EnemySpawnSettings spawnSettings in MapData.SpawnSettings)
            {
                ObjectPooler enemyPool = Instantiate(EnemyPoolPrefab);
                enemyPool.ObjectToPool = spawnSettings.Prefab.gameObject;
                Debug.Log(spawnSettings.Prefab.name);
                enemyPool.Init();
                ObjectPoolers.Add(enemyPool);
                ConnectEnemyInPoolWithDeathEvent(enemyPool);
            }
        }

        /// <summary>
        /// Spawn new enemies until the 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator SpawnRandomEnemies()
        {
            if (MapData.SpawnSettings.Length > 0)
            {
                while (LevelManager.Instance.CurrentThreatLevel < LevelManager.Instance.MaxThreatLevel)
                {
                    EnemySpawnSettings settings = MapData.GetRandomEnemySpawnSettings();
                    EnemyData randomEnemyData = settings.GetRandomEnemyData();
                    foreach (ObjectPooler pooler in ObjectPoolers)
                    {
                        if (pooler.ObjectToPool.gameObject == settings.Prefab.gameObject)
                        {
                            GameObject pooledObject = pooler.PullOneObject();
                            SpawnOneEnemy(pooledObject, randomEnemyData);
                            break;
                        }
                    }
                    yield return null;
                }
            }
            else
                Debug.LogWarning("Not enemy spawn settings inserted");
        }

        /// <summary>
        /// Spawn a new enemy, load data if needed.
        /// Spawn near the player.
        /// </summary>
        public virtual void SpawnOneEnemy(GameObject pooledObject, EnemyData data)
        {
            Enemy enemy = pooledObject.GetComponent<Enemy>();
            enemy.AIBrain.Enabled = true;
            enemy.gameObject.SetActive(true);
            if (enemy.Data != data)
                enemy.LoadEnemy(data);
            enemy.transform.position = GetPositionNearPlayer();
            LevelManager.Instance.CurrentThreatLevel += data.ThreatLevel;
            Debug.Log($"{LevelManager.Instance.CurrentThreatLevel}|{data.ThreatLevel}");
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ConnectEnemyInPoolWithDeathEvent(ObjectPooler pooler)
        {
            foreach (GameObject pooledbject in pooler.ObjectPool.PooledObjects)
            {
                pooledbject.GetComponent<Enemy>().Health.OnDeath += HandleEnemyDeath;
            }
        }

        /// <summary>
        /// Get an empty position near the player.
        /// </summary>
        /// <returns></returns>
        protected virtual Vector3 GetPositionNearPlayer()
        {
            Vector3 randomPosNearPlayer;
            float dst = 0;
            do
            {
                randomPosNearPlayer = LevelManager.Instance.LevelGenerator.GetRandomEmptyPosition();
                dst = Vector3.Distance(randomPosNearPlayer, Characters.Player.Position);
            }
            while (dst < MinDistanceToPlayer || dst > MaxDistanceToPlayer);
            return randomPosNearPlayer;
        }


        /// <summary>
        /// When an enemy dies, delay a bit before spawning new ones.
        /// </summary>
        public virtual void HandleEnemyDeath(Character character)
        {
            Enemy enemy = character.GetComponent<Enemy>();
            LevelManager.Instance.CurrentThreatLevel -= enemy.Data.ThreatLevel;
            StartWaitToSpawnEnemyCounter();
        }

        public virtual void StartWaitToSpawnEnemyCounter()
        {
            _waitToSpawnTime = TimeTillSpawnNewEnemies;
        }

        protected virtual void WaitToSpawnEnemy()
        {
            if (_waitToSpawnTime > 0)
                _waitToSpawnTime -= Time.deltaTime;
            if (_waitToSpawnTime < 0)
            {
                _waitToSpawnTime = 0;
                StartCoroutine(SpawnRandomEnemies());
            }
        }
    }
}

