using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace Penwyn.Game
{
    public class Projectile : PoolableObject
    {
        [HorizontalLine]
        [Header("Velocity")]
        public float Speed;
        public DamageOnTouch DamageOnTouch;
        public CharacterController Controller;

        protected Health _health;

        void Awake()
        {
            _health = GetComponent<Health>();
        }
        
        public virtual void FlyTowards(Vector3 direction)
        {
            Controller.SetVelocity(direction.normalized * Speed);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _health?.SetHealthAtAwake();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Controller.SetVelocity(Vector3.zero);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
