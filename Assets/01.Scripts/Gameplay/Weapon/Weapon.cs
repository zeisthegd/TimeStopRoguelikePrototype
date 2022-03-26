using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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

        [Header("Owner")]
        [ReadOnly] public Character Owner;


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
    }
}
