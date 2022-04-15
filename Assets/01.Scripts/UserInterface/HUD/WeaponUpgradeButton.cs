using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

namespace Penwyn.Game
{
    public class WeaponUpgradeButton : MonoBehaviour
    {
        public Button Button;
        public TMP_Text Name;
        public TMP_Text Description;
        public WeaponData Data;

        public event UnityAction DataChosen;

        public virtual void Set(WeaponData data)
        {
            this.Data = data;
            Name.text = data.Name;
            Description.text = data.Description;
        }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(UpgradeChosen);
        }

        public virtual void UpgradeChosen()
        {
            if (Data != null)
            {
                Characters.Player.CharacterWeaponHandler.CurrentWeapon.Upgrade(Data);
                DataChosen?.Invoke();
            }
        }
    }
}

