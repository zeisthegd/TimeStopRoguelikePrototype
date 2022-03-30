using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Penwyn.Tools;

namespace Penwyn.Game
{
    public class AIActionMoveTowardsPlayer : AIAction
    {
        public float Speed = 1;
        GameObject target;

        void FixedUpdate()
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target != null)
            {
                _character.Controller.SetVelocity((target.transform.position - this.transform.position).normalized * Speed);
            }
        }
    }
}
