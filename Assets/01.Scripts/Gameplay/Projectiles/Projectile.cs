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

        protected CharacterController _controller;

        protected virtual void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public virtual void FlyTowards(Vector3 direction)
        {
            _controller.SetVelocity(direction.normalized * Speed);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _controller.SetVelocity(Vector3.zero);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

}
