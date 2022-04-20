using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class LootDropManager : SingletonMonoBehaviour<LootDropManager>
    {
        public GameObject LesserLoot;
        public GameObject GreaterLoot;
        public GameObject EliteLoot;

        public virtual void HandleEnemyDeath(Enemy enemy)
        {
            switch (enemy.Data.Type)
            {
                case EnemyType.Lesser:
                    HandleLesserEnemyDeath();
                    break;
                case EnemyType.Greater:
                    HandleGreaterEnemyDeath();
                    break;
                case EnemyType.Elite:
                    HandleEliteEnemyDeath();
                    break;
                default:
                    break;
            }
        }

        public virtual void HandleLesserEnemyDeath()
        {

        }

        public virtual void HandleGreaterEnemyDeath()
        {

        }

        public virtual void HandleEliteEnemyDeath()
        {

        }

        public virtual void HandleBossEnemyDeath()
        {

        }

        public virtual void HandleWallDestroyed()
        {

        }
    }
}

