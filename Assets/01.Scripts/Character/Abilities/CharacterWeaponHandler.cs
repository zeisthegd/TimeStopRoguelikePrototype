using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class CharacterWeaponHandler : CharacterAbility
    {
        public Weapon InitialWeaponPrefab;
        public WeaponData InitialWeaponData;

        public Transform WeaponHolder;

        public override void AwakeAbility(Character character)
        {
            base.AwakeAbility(character);
            CreateWeapon();
        }

        public void CreateWeapon()
        {
            if (WeaponHolder == null)
                WeaponHolder = this.transform;

            _currentWeapon = Instantiate(InitialWeaponPrefab, WeaponHolder.position, Quaternion.identity, WeaponHolder);
            _currentWeapon.LoadWeapon(InitialWeaponData);
            _currentWeapon.Owner = this._character;
        }

        protected Weapon _currentWeapon;
        protected WeaponData _currentWeaponData;
    }
}