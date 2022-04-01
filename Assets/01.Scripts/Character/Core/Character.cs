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
        [SerializeField] float characterID;


        [SerializeField] CharacterController controller;
        [SerializeField] GameObject model;
        [SerializeField] Health health;

        public List<GameObject> AbilitiesContainer;

        protected List<CharacterAbility> abilities;

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
            for (int i = 0; i < abilities.Count; i++)
            {
                if (abilities[i].AbilityPermitted)
                {
                    abilities[i].UpdateAbility();
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            for (int i = 0; i < abilities.Count; i++)
            {
                if (abilities[i].AbilityPermitted)
                {
                    abilities[i].FixedUpdateAbility();
                }
            }
        }

        protected virtual void WakeUpAbilities()
        {
            for (int i = 0; i < abilities.Count; i++)
            {
                if (abilities[i].AbilityPermitted)
                {
                    abilities[i].AwakeAbility(this);
                }
            }
        }

        protected virtual void GetGeneralAbilities()
        {
            _characterRun = FindAbility<CharacterRun>();
            _characterWeaponHandler = FindAbility<CharacterWeaponHandler>();
        }

        #region Ability Utilities

        public virtual T FindAbility<T>() where T : CharacterAbility
        {
            Type typeOfSearchAb = typeof(T);
            foreach (CharacterAbility ability in abilities)
            {
                if (ability is T characterAbility)
                    return characterAbility;
            }
            return null;
        }

        protected virtual void GetAbilities()
        {
            abilities = GetComponents<CharacterAbility>().ToList();
            foreach (GameObject abilitiesContainer in AbilitiesContainer)
            {
                var abilitiesList = GetComponentsInChildren<CharacterAbility>().ToList();
                if (abilitiesList.Count > 0)
                {
                    abilities.AddRange(abilitiesList);
                }
            }
        }
        #endregion

        public CharacterController Controller { get => controller; }
        public GameObject Model { get => model; }
        public SpriteRenderer SpriteRenderer { get => model.GetComponent<SpriteRenderer>(); }
        public Animator Animator { get => model.GetComponent<Animator>(); }
        public Vector3 Position { get => transform.position; }
        public List<CharacterAbility> Abilities { get => abilities; }
        public Health Health { get => health; }

        public CharacterRun CharacterRun { get => _characterRun; }
        public CharacterWeaponHandler CharacterWeaponHandler { get => _characterWeaponHandler; }
    }
}
