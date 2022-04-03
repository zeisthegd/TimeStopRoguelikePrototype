using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Penwyn.Tools;

namespace Penwyn.Game
{
    public class AIActionMoveTowardsPlayer : AIAction
    {
        public float Speed = 1;
        public float MinDistance = 2;
        public AnimationCurve DistanceToSpeedCurve;
        protected GameObject _target;

        public override void AwakeComponent(Character character)
        {
            base.AwakeComponent(character);
        }

        public override void UpdateComponent()
        {
            _target = Characters.Player.gameObject;
            float distanceToPlayer = Vector2.Distance(_target.transform.position, _character.transform.position);
            if (_target != null && distanceToPlayer > MinDistance)
            {
                _character.Controller.SetVelocity((_target.transform.position - _character.transform.position).normalized * Speed * DistanceToSpeedCurve.Evaluate(distanceToPlayer));
                //Debug.Log(Vector2.Distance(_target.transform.position, _character.transform.position));
                Debug.DrawRay(_character.transform.position, _character.Controller.Velocity);
            }
        }
    }
}
