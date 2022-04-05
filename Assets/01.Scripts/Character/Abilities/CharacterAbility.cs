using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class CharacterAbility : MonoBehaviour
    {
        public bool AbilityPermitted = true;
        public string Name;

        public List<CharacterAbilityStates> forbidStates;

        protected Character _character;
        protected CharacterController _controller;


        public virtual void AwakeAbility(Character character)
        {
            this._character = character;
            this._controller = character.Controller;
            if (AbilityPermitted)
            {
                ConnectEvents();
            }
        }

        public void ChangePermission(bool permission)
        {
            if (permission)
            {
                ConnectEvents();
            }
            else
            {
                DisconnectEvents();
            }
        }

        public virtual void UpdateAbility() { }
        public virtual void FixedUpdateAbility() { }
        public virtual void ConnectEvents() { }
        public virtual void DisconnectEvents() { }
        public virtual void OnDisable() { }

    }
}