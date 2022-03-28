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


        public virtual void Initialization()
        {
            SetUpInput();
        }

        public virtual void SetUpInput()
        {
            if (InputType == WeaponInputType.NormalAttack)
                InputReader.Instance.NormalAttackPressed += HandleWeaponRequestInput;
            if (InputType == WeaponInputType.SpecialAttack)
                InputReader.Instance.SpecialAttackPressed += HandleWeaponRequestInput;
        }

        public virtual void HandleWeaponRequestInput()
        {
            //*Derive this
        }

        /// <summary>
        /// Load the weapon data from a scriptable data.
        /// </summary>
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
