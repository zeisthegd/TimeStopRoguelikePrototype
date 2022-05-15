using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
namespace Penwyn.Tools
{
    [RequireComponent(typeof(CharacterController))]
    public class FlyToPlayer : MonoBehaviour
    {
        public float Speed = 10;
        public float WaitTillFly = 0.5F;
        protected bool _flyStarted = false;

        protected CharacterController _controller;
        void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }
        void OnEnable()
        {
            StopCoroutine(StartFlying());
            StartCoroutine(StartFlying());
        }

        void OnDisable()
        {
            StopFlying();
        }

        void Update()
        {
            if (_flyStarted)
            {
                _controller.SetVelocity((Characters.Player.Position - this.transform.position).normalized * Speed);
                Debug.DrawRay(this.transform.position, (Characters.Player.Position - this.transform.position).normalized * Speed);
            }
        }

        protected virtual IEnumerator StartFlying()
        {
            yield return new WaitForSeconds(WaitTillFly);
            _flyStarted = true;
        }

        protected virtual void StopFlying()
        {
            StopCoroutine(StartFlying());
            _flyStarted = false;
        }
    }
}

