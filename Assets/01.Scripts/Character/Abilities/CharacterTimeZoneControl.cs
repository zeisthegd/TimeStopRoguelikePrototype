using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

using DG.Tweening;

namespace Penwyn.Game
{
    public class CharacterTimeZoneControl : CharacterAbility
    {
        [Header("Settings")]
        [Range(0, 1)] public float SlowTimeScale = 0.75F;
        public float TimeZoneRange = 2;
        [Header("Prefab")]
        public TimeSlowZone TimeSlowZone;

        [Header("The Grab")]
        public float EnergyPerObject = 0.2F;
        public float ZoneClosingDuration = 0.5F;

        [Header("Cooldown After Grab")]
        public float DelayBeforeRecoverDuration = 1F;
        public float RecoverDuration = 1F;

        protected TimeSlowZone _timeSlowZone;

        public event UnityAction<float> TimeScaleChanged;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            _timeSlowZone = Instantiate(TimeSlowZone, character.Position, Quaternion.identity, character.transform);
            _timeSlowZone.Initialization(this);
        }

        /// <summary>
        /// Grab the slowed projectiles inside of the time zone.
        /// </summary>
        public virtual void GrabProjectiles()
        {
            if (_timeSlowZone.IsUseable)
            {
                GameObject[] grabableInRange = new GameObject[_timeSlowZone.ObjectsInRange.Count];
                _timeSlowZone.ObjectsInRange.CopyTo(grabableInRange);
                foreach (GameObject item in grabableInRange)
                {
                    item.SetActive(false);
                    _character.Energy.Add(EnergyPerObject);
                    //TODO Play feedback
                }
                _timeSlowZone.CloseZone();
                StartCoroutine(_character.CharacterWeaponHandler.CurrentWeapon.UseWeaponTillNoTargetOrEnergy());
            }
        }


        public void SetTimeScale(float newScale)
        {
            SlowTimeScale = newScale;
            _timeSlowZone.SlowTimeScale = newScale;
            TimeScaleChanged?.Invoke(newScale);
        }

        public override void ConnectEvents()
        {
            InputReader.Instance.GrabProjectilesPressed += GrabProjectiles;
        }

        public override void DisconnectEvents()
        {
            InputReader.Instance.GrabProjectilesPressed -= GrabProjectiles;
        }
    }
}
