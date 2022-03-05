using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSlowTimeSurround : CharacterAbility
{
    [SerializeField][Range(0, 1)] private float slowTimeScale = 0.75F;
    [SerializeField] float timeZoneRange = 2;
    [SerializeField] private TimeSlowZone timeSlowZone;

    private TimeSlowZone _TimeSlowZone;

    public event UnityAction<float> TimeScaleChanged;

    public override void AwakeAbility(Character character)
    {
        base.AwakeAbility(character);
        _TimeSlowZone = Instantiate(timeSlowZone, character.Position, Quaternion.identity, character.transform);
        _TimeSlowZone.SlowTimeScale = slowTimeScale;
        _TimeSlowZone.transform.localScale = Vector3.one * timeZoneRange;
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
