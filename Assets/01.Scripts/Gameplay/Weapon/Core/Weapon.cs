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
        protected WeaponAutoAim _weaponAutoAim;
        protected Coroutine _cooldownCoroutine;

        protected virtual void Awake()
        {
            GetComponents();
        }
        public virtual void Initialization()
        {
        }

        public virtual void RequestWeaponUse()
        {
            //*Derive this
            if (_currentWeaponState == WeaponState.WeaponIdle)
            {
                UseWeapon();
            }
        }

        protected virtual void UseWeapon()
        {
            _currentWeaponState = WeaponState.WeaponUse;
            StartCooldown();
            UseEnergy();
        }

        public virtual IEnumerator UseWeaponTillNoTargetOrEnergy()
        {
            do
            {
                if (Owner.Health.CurrentHealth < CurrentData.HealthPerUse)
                    break;
                if (_weaponAutoAim)
                {
                    _weaponAutoAim.FindTarget();
                    if (_weaponAutoAim.Target == null)
                        break;
                }
                RequestWeaponUse();
                yield return null;
            }
            while (true);
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
            if (Owner.Health != null)
                Owner.Health.Lose(CurrentData.HealthPerUse);
        }

        protected virtual void OnHealthChanged()
        {
            if (Owner.Health.CurrentHealth <= CurrentData.HealthPerUse)
            {
                if (_currentWeaponState == WeaponState.WeaponCooldown && _cooldownCoroutine != null)
                    StopCoroutine(_cooldownCoroutine);
                _currentWeaponState = WeaponState.WeaponNotEnoughHealth;
            }
            else
            {
                if (_currentWeaponState == WeaponState.WeaponNotEnoughHealth)
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
            SetHealthRequirements();
        }

        [Button("Load Weapon Data")]
        public virtual void LoadWeapon()
        {
            if (CurrentData != null)
                LoadWeapon(CurrentData);
            else
                Debug.Log("Please insert Weapon Data");
        }

        [Button("Upgrade")]
        public virtual void Upgrade()
        {
            if (CurrentData != null)
            {
                if (CurrentData.Upgrade != null)
                    LoadWeapon(CurrentData.Upgrade);
            }
            else
                Debug.Log("Please insert Weapon Data");
        }

        public virtual void SetHealthRequirements()
        {
            if (CurrentData.RequiresHealth)
            {
                if (Owner.Health != null)
                {
                    Owner.Health.OnChanged += OnHealthChanged;
                }
                else
                {
                    Debug.LogWarning($"No health assigned to {Owner.name} although this {CurrentData.Name} requires health!");
                }
            }
        }

        public virtual void GetComponents()
        {
            _weaponAim = GetComponent<WeaponAim>();
            _weaponAutoAim = GetComponent<WeaponAutoAim>();
        }

        protected virtual void OnEnable()
        {
            _currentWeaponState = WeaponState.WeaponIdle;
        }

        protected virtual void OnDisable()
        {
            if (CurrentData.RequiresHealth && Owner.Health != null)
                Owner.Health.OnChanged -= OnHealthChanged;
        }
    }
}
