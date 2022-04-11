using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    [CreateAssetMenu(menuName = "Character/EnemyDat")]
    public class EnemyData : ScriptableObject
    {
        public float MoveSpeed = 1;
        public float ThreatLevel = 1;
        public RuntimeAnimatorController RuntimeAnimatorController;
        public WeaponData WeaponData;
    }

}