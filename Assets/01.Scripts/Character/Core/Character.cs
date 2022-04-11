using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class Character : MonoBehaviour
    {
        [Header("Character ID")]
        public string CharacterID;

        [Header("Controller")]
        public CharacterController Controller;

        [Header("Graphics")]
        public GameObject Model;
        public Animator Animator;

        [Header("Stats")]
        public Health Health;

        [Header("Abilities")]
        public List<GameObject> AbilitiesContainer;


        protected List<CharacterAbility> _abilities;
        protected CharacterWeaponHandler _characterWeaponHandler;
        protected CharacterRun _characterRun;

        protected virtual void Awake()
        {
            GetAbilities();
            WakeUpAbilities();
            GetGeneralAbilities();
        }
        protected virtual void Update()
        {
            UpdateAbilities();
        }

        protected virtual void FixedUpdate()
        {
            FixedUpdateAbilities();
        }


        #region Abilities Handling

        protected virtual void UpdateAbilities()
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                if (_abilities[i].AbilityPermitted)
                {
                    _abilities[i].UpdateAbility();
                }
            }
        }
        protected virtual void FixedUpdateAbilities()
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                if (_abilities[i].AbilityPermitted)
                {
                    _abilities[i].FixedUpdateAbility();
                }
            }
        }

        protected virtual void WakeUpAbilities()
        {
            for (int i = 0; i < _abilities.Count; i++)
            {
                if (_abilities[i].AbilityPermitted)
                {
                    _abilities[i].AwakeAbility(this);
                }
            }
        }

        protected virtual void GetGeneralAbilities()
        {
            _characterRun = FindAbility<CharacterRun>();
            _characterWeaponHandler = FindAbility<CharacterWeaponHandler>();
        }

        public virtual T FindAbility<T>() where T : CharacterAbility
        {
            Type typeOfSearchAb = typeof(T);
            foreach (CharacterAbility ability in _abilities)
            {
                if (ability is T characterAbility)
                    return characterAbility;
            }
            return null;
        }

        protected virtual void GetAbilities()
        {
            _abilities = GetComponents<CharacterAbility>().ToList();
            foreach (GameObject abilitiesContainer in AbilitiesContainer)
            {
                var abilitiesList = GetComponentsInChildren<CharacterAbility>().ToList();
                if (abilitiesList.Count > 0)
                {
                    _abilities.AddRange(abilitiesList);
                }
            }
        }
        #endregion

        public SpriteRenderer SpriteRenderer { get => Model.GetComponent<SpriteRenderer>(); }
        public Vector3 Position { get => transform.position; }
        public List<CharacterAbility> Abilities { get => _abilities; }
        public CharacterRun CharacterRun { get => _characterRun; }
        public CharacterWeaponHandler CharacterWeaponHandler { get => _characterWeaponHandler; }
    }
}
