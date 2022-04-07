using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class WeaponAim : MonoBehaviour
    {
        public WeaponAimType AimType;
        protected Weapon _weapon;


        protected virtual void Start()
        {
            _weapon = GetComponent<Weapon>();
        }

        protected virtual void Update()
        {
            switch (AimType)
            {
                case WeaponAimType.Mouse:
                    Aim();
                    break;
                default:
                    break;
            }
        }

        protected virtual void Aim()
        {
            Vector3 dirToMouse = CursorUtility.GetMousePosition() - _weapon.transform.position;
            _weapon.transform.right = dirToMouse;
        }
    }

    public enum WeaponAimType
    {
        Mouse,
        Script
    }
}

