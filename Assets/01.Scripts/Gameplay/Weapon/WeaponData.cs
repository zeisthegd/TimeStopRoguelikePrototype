using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Character/Weapon/Data", fileName = "NewWeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Name")]
    public string Name = "No Name";
    [Header("Description")]
    public string Description = "Description Here";

    [Header("Graphics")]
    public Sprite Icon;
    public Animator Animator;

    [Header("Bullet")]
    public GameObject Bullet;

    [Header("Settings")]
    public float BulletPerShot = 1;
    public float TimeBetweenBulletSpawn = 0;
    public float Cooldown = 1;
}
