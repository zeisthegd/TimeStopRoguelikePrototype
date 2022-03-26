using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class DamageOnTouch : MonoBehaviour
    {
        public LayerMask TargetMask;
        public LayerMask ObstacleMask;

        public float Damage;

        public event UnityAction CollideWithObstacle;

        public virtual void DealDamage(GameObject gObject)
        {
            Character character = gObject.FindComponent<Character>();
            character.Health?.Take(Damage);
        }

        public virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (TargetMask.Contains(col.gameObject.layer))
            {
                DealDamage(col.gameObject);
            }
            else if (ObstacleMask.Contains(col.gameObject.layer))
            {
                CollideWithObstacle?.Invoke();
            }
        }
    }
}
