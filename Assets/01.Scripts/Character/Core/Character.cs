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

        private List<CharacterAbility> abilities;

        void Awake()
        {
            GetAbilities();
            WakeUpAbilities();
        }

        void Update()
        {
            for (int i = 0; i < abilities.Count; i++)
            {
                if (abilities[i].AbilityPermitted)
                {
                    abilities[i].UpdateAbility();
                }
            }
        }

        void FixedUpdate()
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

        public T FindAbility<T>() where T : CharacterAbility
        {
            Type typeOfSearchAb = typeof(T);
            foreach (CharacterAbility ability in abilities)
            {
                if (ability is T characterAbility)
                    return characterAbility;
            }
            return null;
        }

        void GetAbilities()
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

        public CharacterController Controller { get => controller; }
        public GameObject Model { get => model; }
        public SpriteRenderer SpriteRenderer { get => model.GetComponent<SpriteRenderer>(); }
        public Animator Animator { get => model.GetComponent<Animator>(); }
        public Vector3 Position { get => transform.position; }
        public List<CharacterAbility> Abilities { get => abilities; }
        public Health Health { get => health; }
    }
}
