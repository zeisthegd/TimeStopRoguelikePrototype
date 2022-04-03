using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class Weapon : MonoBehaviour
    {
        [Header("Data")]
        [Expandable]
        public WeaponData CurrentData;
        [HorizontalLine]

        [Header("Graphics")]
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        [HorizontalLine]

        [Header("Input")]
        public WeaponInputType InputType;

        [Header("Owner")]
        [ReadOnly] public Character Owner;

        [ReadOnly][SerializeField] protected WeaponState _currentWeaponState;
        protected WeaponAim _weaponAim;


        public virtual void Initialization()
        {
            GetComponents();
            SetUpInput();
        }

        protected virtual void Update()
        {

        }

        public virtual void SetUpInput()
        {
            if (InputType == WeaponInputType.NormalAttack)
                InputReader.Instance.NormalAttackPressed += HandleRequestWeaponUse;
            if (InputType == WeaponInputType.SpecialAttack)
                InputReader.Instance.SpecialAttackPressed += HandleRequestWeaponUse;
        }

        public virtual void GetComponents()
        {
            _weaponAim = GetComponent<WeaponAim>();
        }

        public virtual void HandleRequestWeaponUse()
        {
            //*Derive this
            if (_currentWeaponState == WeaponState.WeaponIdle)
            {
                _currentWeaponState = WeaponState.WeaponUse;
                UseWeapon();
            }
        }

        protected virtual void UseWeapon()
        {
            StartCooldown();
        }

        public virtual void StartCooldown()
        {
            _currentWeaponState = WeaponState.WeaponCooldown;
            StartCoroutine(CooldownCoroutine());
        }

        protected virtual IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(CurrentData.Cooldown);
            _currentWeaponState = WeaponState.WeaponIdle;
        }

        /// <summary>
        /// Load the weapon data from a scriptable data.
        /// </summary>
        public virtual void LoadWeapon(WeaponData data)
        {
            CurrentData = data;
            SpriteRenderer.sprite = data.Icon;
        }

        [Button("Load Weapon Data")]
        public virtual void LoadWeapon()
        {
            if (CurrentData != null)
                LoadWeapon(CurrentData);
            else
                Debug.Log("Please insert Weapon Data");
        }
    }
}
