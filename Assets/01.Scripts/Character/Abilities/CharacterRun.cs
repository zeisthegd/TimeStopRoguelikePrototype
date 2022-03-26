using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Penwyn.Tools;

namespace Penwyn.Game
{
    public class CharacterRun : CharacterAbility
    {
        [Header("Speed")]
        public float RunSpeed;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
        }

        public override void UpdateAbility()
        {
            base.UpdateAbility();
            Vector3.ClampMagnitude(_controller.Velocity, RunSpeed);
        }

        public override void FixedUpdateAbility()
        {
            base.FixedUpdateAbility();
            RunRaw(InputReader.Instance.MoveInput);
        }

        public void RunRaw(Vector2 input)
        {
            _controller.SetVelocity(input.normalized * RunSpeed);
        }

        public override void ConnectEvents()
        {
            base.ConnectEvents();
        }

        public override void DisconnectEvents()
        {
            base.DisconnectEvents();
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
