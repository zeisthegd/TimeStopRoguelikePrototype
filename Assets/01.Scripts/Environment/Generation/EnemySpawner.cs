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
        public Vector2 DistanceToPlayer;
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

        public virtual void SpawnOneEnemy(GameObject pooledObject, EnemyData data)
        {
            Enemy enemy = pooledObject.GetComponent<Enemy>();
            enemy.AIBrain.Enabled = true;
            enemy.gameObject.SetActive(true);
            enemy.LoadEnemy(data);
            enemy.transform.position = LevelManager.Instance.LevelGenerator.GetRandomEmptyPosition();

            LevelManager.Instance.CurrentThreatLevel += data.ThreatLevel;
        }

        public virtual void ConnectEnemyInPoolWithDeathEvent(ObjectPooler pooler)
        {
            foreach (GameObject pooledbject in pooler.ObjectPool.PooledObjects)
            {
                pooledbject.GetComponent<Enemy>().Health.OnDeath += HandleEnemyDeath;
            }
        }

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

