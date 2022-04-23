using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;

using Penwyn.Game;
using Penwyn.Tools;

namespace Penwyn.UI
{
    public class HUD : SingletonMonoBehaviour<HUD>
    {
        [Header("Player")]
        public ProgressBar PlayerHealth;
        public TMP_Text PlayerMoney;
        public Button WeaponButton;
        public List<WeaponUpgradeButton> WeaponUpgradeButtons;

        [Header("Level")]
        public ProgressBar LevelProgress;


        protected virtual void Awake()
        {
            LevelManager.PlayerSpawned += OnPlayerSpawned;
        }

        protected virtual void Update()
        {
            UpdateLevelProgress();
        }

        #region Level HUD

        public virtual void UpdateLevelProgress()
        {
            LevelProgress.SetValue(LevelManager.Instance.Progress);
        }

        #endregion

        #region PlayerHUD

        public virtual void SetHealthBar()
        {
            if (PlayerHealth != null)
            {
                PlayerHealth.SetMaxValue(Characters.Player.Health.MaxHealth);
                PlayerHealth.SetValue(Characters.Player.Health.CurrentHealth);
            }
        }

        protected virtual void UpdateHealth()
        {
            PlayerHealth.SetValue(Characters.Player.Health.CurrentHealth);
            if (Characters.Player.Health.MaxHealth != PlayerHealth.ActualValue.maxValue)
            {
                PlayerHealth.SetMaxValue(Characters.Player.Health.MaxHealth);
                Debug.Log(PlayerHealth.ActualValue.maxValue);
            }
        }

        protected virtual void UpdateMoney()
        {
            if (PlayerMoney != null)
                PlayerMoney.SetText(Characters.Player.CharacterMoney.CurrentMoney + "");
        }

        #region Weapon Upgrades

        public virtual void LoadAvailableUpgrades()
        {
            if (Characters.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades.Count <= 0)
                return;
            Time.timeScale = 0;
            for (int i = 0; i < Characters.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades.Count; i++)
            {
                WeaponUpgradeButtons[i].Set(Characters.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Upgrades[i]);
                WeaponUpgradeButtons[i].gameObject.SetActive(true);
            }
        }

        public virtual void EndWeaponUpgrades()
        {
            SetWeaponButtonIcon();
            Time.timeScale = 1;
            for (int i = 0; i < WeaponUpgradeButtons.Count; i++)
            {
                WeaponUpgradeButtons[i].gameObject.SetActive(false);
            }
        }

        public virtual void ConnectEndWeaponUpgradesEvents()
        {
            for (int i = 0; i < WeaponUpgradeButtons.Count; i++)
            {
                WeaponUpgradeButtons[i].DataChosen += EndWeaponUpgrades;
            }
        }

        public virtual void DisconnectEndWeaponUpgradesEvents()
        {
            for (int i = 0; i < WeaponUpgradeButtons.Count; i++)
            {
                WeaponUpgradeButtons[i].DataChosen -= EndWeaponUpgrades;
            }
        }

        public virtual void SetWeaponButtonIcon()
        {
            if (WeaponButton != null)
                WeaponButton.image.sprite = Characters.Player.CharacterWeaponHandler.CurrentWeapon.CurrentData.Icon;
        }

        #endregion

        protected virtual void OnPlayerSpawned()
        {
            SetHealthBar();
            SetWeaponButtonIcon();
            ConnectEvents();
        }

        #endregion

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {
            DisconnectEvents();
        }

        public virtual void ConnectEvents()
        {
            Characters.Player.Health.OnChanged += UpdateHealth;
            Characters.Player.CharacterMoney.MoneyChanged += UpdateMoney;
            Characters.Player.CharacterWeaponHandler.CurrentWeapon.RequestUpgradeEvent += LoadAvailableUpgrades;
            ConnectEndWeaponUpgradesEvents();
        }

        public virtual void DisconnectEvents()
        {
            Characters.Player.Health.OnChanged -= UpdateHealth;
            Characters.Player.CharacterMoney.MoneyChanged -= UpdateMoney;
            Characters.Player.CharacterWeaponHandler.CurrentWeapon.RequestUpgradeEvent -= LoadAvailableUpgrades;
            DisconnectEndWeaponUpgradesEvents();
        }
    }
}

