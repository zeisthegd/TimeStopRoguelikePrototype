using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Movement")]
public class PhysicsSettings : ScriptableObject
{
    [Header(" ------Movement ------")]
    [Range(0, 1)] [Min(0)] public float runPower;
    [Min(0)] public float friction;


    [Header(" ------Acceleration and Deceleration ------")]
    [Min(0)] public float accelleration;
    [Min(0)] public float decelleration;
}
