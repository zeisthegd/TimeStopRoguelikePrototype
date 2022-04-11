using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
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
        public Projectile Projectile;

        [Header("Settings")]
        public int BulletPerShot = 1;
        public float DelayBetweenBullets = 0.1F;
        public float Angle = 0;
        public float Cooldown = 1;
        [Header("Energy Requirements")]
        public bool RequiresEnergy = false;
        public float EnergyPerUse = 0;
        [Header("Add-On")]
        public bool ProjectileRotating = false;
        public float RotateSpeed = 1;

        [Header("Upgrade")]
        public WeaponData Upgrade;
    }
}

