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
        public ProgressBar PlayerHealth;
        public ProgressBar PlayerEnergy;

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
            }
        }

        protected virtual void UpdateHealth()
        {
            PlayerHealth.SetValue(Characters.Player.Health.CurrentHealth);
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

        }

        public virtual void DisconnectEvents()
        {
            Characters.Player.Health.OnHit -= UpdateHealth;

        }
    }
}

