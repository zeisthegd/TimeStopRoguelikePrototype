using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Character/EnemyDat")]
    public class EnemyData : ScriptableObject
    {
        [Header("General")]
        public float Health = 1;
        public float MoveSpeed = 1;
        public float ThreatLevel = 1;
        public EnemyType Type;
        [Header("Weapon")]
        public WeaponData WeaponData;
        public RuntimeAnimatorController RuntimeAnimatorController;
    }
}