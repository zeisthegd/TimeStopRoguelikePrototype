using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class Enemy : Character
    {
        public AIBrain AIBrain;
        public EnemyData InitData;

        protected EnemyData _data;

        protected override void Awake()
        {
            base.Awake();
            if (AIBrain == null)
                AIBrain = GetComponent<AIBrain>();
        }

        public virtual void LoadEnemy(EnemyData data)
        {
            this._data = data;
            LoadEnemy();
        }

        public virtual void LoadEnemy()
        {
            this.Animator.runtimeAnimatorController = _data.RuntimeAnimatorController;
            this.Health.Set(_data.Health, _data.Health);
            this._characterRun.RunSpeed = _data.MoveSpeed;
            this._characterWeaponHandler.ChangeWeapon(_data.WeaponData);
        }
        public EnemyData Data { get => _data; }
    }
}
