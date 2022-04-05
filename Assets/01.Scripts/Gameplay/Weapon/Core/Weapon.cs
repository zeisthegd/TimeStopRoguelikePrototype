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
        protected Coroutine _cooldownCoroutine;


        public virtual void Initialization()
        {
            GetComponents();
            SetEnergyRequirements();
        }

        protected virtual void Update()
        {
            if (InputType == WeaponInputType.NormalAttack && InputReader.Instance.IsHoldingNormalAttack)
                HandleRequestWeaponUse();
            if (InputType == WeaponInputType.SpecialAttack && InputReader.Instance.IsHoldingSpecialAttack)
                HandleRequestWeaponUse();
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
            UseEnergy();
        }

        public virtual void StartCooldown()
        {
            _currentWeaponState = WeaponState.WeaponCooldown;
            _cooldownCoroutine = StartCoroutine(CooldownCoroutine());
        }

        protected virtual IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(CurrentData.Cooldown);
            _currentWeaponState = WeaponState.WeaponIdle;
        }

        protected virtual void UseEnergy()
        {
            if (Owner.Energy != null)
                Owner.Energy.Use(CurrentData.EnergyPerUse);
        }

        protected virtual void OnEnergyChanged()
        {
            if (Owner.Energy.CurrentEnergy < CurrentData.EnergyPerUse)
            {
                if (_currentWeaponState == WeaponState.WeaponCooldown && _cooldownCoroutine != null)
                    StopCoroutine(_cooldownCoroutine);
                _currentWeaponState = WeaponState.WeaponNoEnergy;
                // Debug.Break();
            }
            else
            {
                if (_currentWeaponState == WeaponState.WeaponNoEnergy)
                    _currentWeaponState = WeaponState.WeaponIdle;
            }
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

        public virtual void SetEnergyRequirements()
        {
            if (CurrentData.RequiresEnergy)
            {
                if (Owner.Energy != null)
                {
                    Owner.Energy.OnChanged += OnEnergyChanged;
                }
                else
                {
                    Debug.LogWarning($"No energy assigned to {Owner.name} although this {CurrentData.Name} requires energy!");
                }
            }
        }

        public virtual void GetComponents()
        {
            _weaponAim = GetComponent<WeaponAim>();
        }

        protected virtual void OnDisable()
        {
            if (Owner.Energy != null)
                Owner.Energy.OnChanged -= OnEnergyChanged;
        }
    }
}
