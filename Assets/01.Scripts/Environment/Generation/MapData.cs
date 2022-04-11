using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Environment/LevelGeneration/MapData")]
    public class MapData : ScriptableObject
    {
        public string Name;
        public EnemySpawnSettings[] SpawnSettings;
        public float StartingThreatLevel = 3;
        public float ThreatLevelIncrementPerSecond = 0.1F;

        public EnemySpawnSettings GetRandomEnemySpawnSettings()
        {
            if (SpawnSettings.Length > 0)
                return SpawnSettings[Randomizer.RandomNumber(0, SpawnSettings.Length)];
            return new EnemySpawnSettings();
        }
    }

    [System.Serializable]
    public struct EnemySpawnSettings
    {
        public string Name;
        public Enemy Prefab;
        public EnemyData[] Datas;

        public EnemyData GetRandomEnemyData()
        {
            if (Datas.Length > 0)
                return Datas[Randomizer.RandomNumber(0, Datas.Length)];
            Debug.Log("No enemy data inserted!");
            return null;
        }
    }
}

