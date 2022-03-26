using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Penwyn.Tools;

namespace Penwyn.Game
{
    public class AIActionMoveTowardsPlayer : MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] float speed;
        GameObject target;
        void Start()
        {
            character = gameObject.FindComponent<Character>();
        }

        void FixedUpdate()
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target != null)
            {
                character.Controller.SetVelocity((target.transform.position - this.transform.position).normalized * speed);
            }
        }
    }
}
