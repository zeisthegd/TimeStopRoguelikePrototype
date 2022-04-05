using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;
using Penwyn.Game;

namespace Penwyn.UI
{
    public class HUD : MonoBehaviour
    {
        [Header("Stats Bar")]
        public ProgressBar PlayerHealth;
        public ProgressBar PlayerEnergy;
        [HorizontalLine]
        public float WidthPerValue = 5;

        protected virtual void Awake()
        {
            LevelManager.PlayerSpawned += OnPlayerSpawned;
        }


        public virtual void SetHealthAndEnergyBar()
        {
            if (PlayerHealth != null)
            {
                PlayerHealth.SetMaxValue(Characters.Player.Health.StartingHealth);
                PlayerHealth.SetValue(Characters.Player.Health.CurrentHealth);
                PlayerHealth.SetWidth(WidthPerValue * PlayerHealth.ActualValue.maxValue);
            }

            if (PlayerEnergy != null)
            {
                PlayerEnergy.SetMaxValue(Characters.Player.Energy.MaxEnergy);
                PlayerEnergy.SetValue(Characters.Player.Energy.CurrentEnergy);
                PlayerEnergy.SetWidth(WidthPerValue * PlayerEnergy.ActualValue.maxValue);
            }
        }

        protected virtual void UpdateHealth()
        {
            PlayerHealth.SetValue(Characters.Player.Health.CurrentHealth);
        }

        protected virtual void UpdateEnergy()
        {
            PlayerEnergy.SetValue(Characters.Player.Energy.CurrentEnergy);
        }

        protected virtual void OnPlayerSpawned()
        {
            SetHealthAndEnergyBar();
            ConnectEvents();
        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {
            DisconnectEvents();
        }

        public virtual void ConnectEvents()
        {
            Characters.Player.Health.OnHit += UpdateHealth;
            Characters.Player.Energy.OnChanged += UpdateEnergy;
        }

        public virtual void DisconnectEvents()
        {
            Characters.Player.Health.OnHit -= UpdateHealth;
            Characters.Player.Energy.OnChanged -= UpdateEnergy;
        }
    }
}
