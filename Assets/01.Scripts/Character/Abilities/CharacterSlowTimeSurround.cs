using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Penwyn.Game
{
    public class CharacterSlowTimeSurround : CharacterAbility
    {
        [Header("")]
        [SerializeField][Range(0, 1)] protected float slowTimeScale = 0.75F;
        public float TimeZoneRange = 2;
        public TimeSlowZone TimeSlowZone;

        private TimeSlowZone _TimeSlowZone;

        public event UnityAction<float> TimeScaleChanged;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            _TimeSlowZone = Instantiate(TimeSlowZone, character.Position, Quaternion.identity, character.transform);
            _TimeSlowZone.SlowTimeScale = slowTimeScale;
            _TimeSlowZone.transform.localScale = Vector3.one * TimeZoneRange;
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
