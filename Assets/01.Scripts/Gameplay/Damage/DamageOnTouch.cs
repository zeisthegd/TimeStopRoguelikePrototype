using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class DamageOnTouch : MonoBehaviour
    {
        [Header("Masks")]
        public LayerMask TargetMask;
        public LayerMask ObstacleMask;
        [HorizontalLine]

        [Header("Damage To Damageable")]
        public float DamageDeal = 1;
        public float RecoilDamage = 1;
        [Header("Feedbacks")]
        public Feedbacks HitFeedbacks;
        [Header("Collide With Non-Damageable")]
        public bool DestroyWhenCollidingNonDamageable = true;

        protected Health _health;

        public virtual void Awake()
        {
            _health = GetComponent<Health>();
        }

        /// <summary>
        /// Deal damage to an object.
        /// </summary>
        public virtual void DealDamage(GameObject gObject)
        {
            if (this.gameObject.activeInHierarchy)
            {
                Health objectHealth = gObject.FindComponent<Health>();
                objectHealth?.Take(DamageDeal);
            }
        }

        /// <summary>
        /// After touching a number of damageable objects, destroy itself.
        /// </summary>
        protected virtual void HandleRecoilDamage()
        {
            if (RecoilDamage > 0)
            {
                _health.Take(RecoilDamage);
            }
        }

        /// <summary>
        /// if DestroyWhenCollidingNonDamageable, kill this object.
        /// </summary>
        public virtual void HandleCollideNonDamageable()
        {
            if (DestroyWhenCollidingNonDamageable)
            {
                _health.Kill();
            }
        }

        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (this.gameObject.activeInHierarchy && TargetMask.Contains(col.gameObject.layer))
            {
                DealDamage(col.gameObject);
                HandleRecoilDamage();
                HitFeedbacks?.PlayFeedbacks();
            }
            else if (ObstacleMask.Contains(col.gameObject.layer))
            {
                HandleCollideNonDamageable();
                HitFeedbacks?.PlayFeedbacks();
            }
        }


        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }
    }
}
