using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using Penwyn.Tools;

namespace Penwyn.Game
{
    public class CharacterTimeZoneControl : CharacterAbility
    {
        [Header("Settings")]
        [Range(0, 1)] public float SlowTimeScale = 0.75F;
        public float TimeZoneRange = 2;
        [Header("Prefab")]
        public TimeSlowZone TimeSlowZone;

        [Header("Energy Regen")]
        public float EnergyPerObject = 0.2F;

        protected TimeSlowZone _timeSlowZone;

        public event UnityAction<float> TimeScaleChanged;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            _timeSlowZone = Instantiate(TimeSlowZone, character.Position, Quaternion.identity, character.transform);
            _timeSlowZone.SlowTimeScale = SlowTimeScale;
            _timeSlowZone.transform.localScale = Vector3.one * TimeZoneRange;
        }

        /// <summary>
        /// Grab the slowed projectiles inside of the time zone.
        /// </summary>
        public virtual void GrabProjectiles()
        {
            GameObject[] grabableInRange = new GameObject[_timeSlowZone.ObjectsInRange.Count];
            _timeSlowZone.ObjectsInRange.CopyTo(grabableInRange);
            foreach (GameObject item in grabableInRange)
            {
                item.SetActive(false);
                _character.Energy.Add(EnergyPerObject);
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
