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
        [Header("Time Zone")]
        [SerializeField][Range(0, 1)] protected float slowTimeScale = 0.75F;
        public float TimeZoneRange = 2;
        public TimeSlowZone TimeSlowZone;
        [HorizontalLine]

        [Header("Projectiles Grab")]
        public LayerMask TargetMask;


        protected TimeSlowZone _TimeSlowZone;

        public event UnityAction<float> TimeScaleChanged;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            _TimeSlowZone = Instantiate(TimeSlowZone, character.Position, Quaternion.identity, character.transform);
            _TimeSlowZone.SlowTimeScale = slowTimeScale;
            _TimeSlowZone.transform.localScale = Vector3.one * TimeZoneRange;
        }

        /// <summary>
        /// Grab the slowed projectiles inside of the time zone.
        /// </summary>
        public virtual void GrabProjectiles()
        {
            foreach (Collider2D item in _TimeSlowZone.ObjectsInRange)
            {
                if (TargetMask.Contains(item.gameObject.layer))
                {
                    item.gameObject.SetActive(false);
                }
            }
        }

        public override void ConnectEvents()
        {
            InputReader.Instance.GrabProjectilesPressed += GrabProjectiles;
        }

        public override void DisconnectEvents()
        {
            InputReader.Instance.GrabProjectilesPressed -= GrabProjectiles;
        }

        public float SlowTimeScale
        {
            get => slowTimeScale;
            set
            {
                slowTimeScale = value;
                _TimeSlowZone.SlowTimeScale = value;
                TimeScaleChanged?.Invoke(value);
            }
        }
    }
}
