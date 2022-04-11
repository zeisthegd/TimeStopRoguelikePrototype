using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;


namespace Penwyn.Game
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        public float StartingHealth = 10;
        [Header("Invincible")]
        public bool Invincible = false;
        public Color InvincibleFlickerColor = Color.yellow;

        [Header("Invulnerable")]
        public float InvulnerableDuration = 1;
        [Header("Damaged Feedback")]
        public Color DamageTakenFlickerColor = Color.red;

        [SerializeField][ReadOnly] protected float _health = 0;
        protected float _invulnerableTime = 0;
        protected bool _currentlyInvulnerable = false;
        protected Character _character;
        protected HealthBar _healthBar;

        public event UnityAction OnChanged;
        public event UnityAction<Character> OnDeath;

        void Start()
        {
            _character = GetComponent<Character>();
            _health = StartingHealth;

            CreateHealthBar();
        }

        #region Damage Taken

        public virtual void Take(float damage)
        {
            if (_health > 0 && !_currentlyInvulnerable && !Invincible)
            {
                _health -= damage;
                if (_health > 0)
                {
                    MakeInvulnerable();
                    OnChanged?.Invoke();
                    if (_healthBar)
                        _healthBar.SetHealth(_health);
                }
                else
                {
                    Kill();
                }
            }
        }

        /// <summary>
        /// Lose flat HP. No invulnerable started.
        /// </summary>
        /// <param name="health">Amount</param>
        public virtual void Lose(float health)
        {
            _health -= health;
            if (_health > 0)
            {
                OnChanged?.Invoke();
                if (_healthBar)
                    _healthBar.SetHealth(_health);
            }
            else
            {
                Kill();
            }
        }

        /// <summary>
        /// Get a flat amount of HP, positive value only.
        /// </summary>
        /// <param name="health"></param>
        public virtual void Get(float health)
        {
            if (health < 0)
                return;
            _health += health;
            OnChanged?.Invoke();
        }

        #endregion

        #region Kill
        public virtual void Kill()
        {
            _health = 0;
            OnDeath?.Invoke(_character);
            gameObject.SetActive(false);
        }
        #endregion

        #region Invulnerable and Invincible
        public void MakeInvulnerable()
        {
            if (InvulnerableDuration > 0)
                StartCoroutine(PerformInvulnerable());
        }

        public virtual void MakeInvincible(float duration)
        {
            Invincible = true;
            if (duration > 0)
            {
                StartCoroutine(InvincibleCoroutine(duration));
            }
            else
            {
                //TODO change shader or soemthing.
            }
        }

        protected virtual IEnumerator PerformInvulnerable()
        {
            _currentlyInvulnerable = true;
            _invulnerableTime = 0;
            Coroutine flicker = StartCoroutine(SpriteRendererUtil.Flicker(_character.SpriteRenderer, DamageTakenFlickerColor, InvulnerableDuration, 0.1F));
            while (_invulnerableTime < InvulnerableDuration)
            {
                _invulnerableTime += Time.deltaTime;
                yield return null;
            }
            _currentlyInvulnerable = false;
        }

        protected virtual IEnumerator InvincibleCoroutine(float duration)
        {
            _invulnerableTime = 0;
            Coroutine flicker = StartCoroutine(SpriteRendererUtil.Flicker(_character.SpriteRenderer, InvincibleFlickerColor, duration));
            while (_invulnerableTime < duration)
            {
                _invulnerableTime += Time.deltaTime;
                yield return null;
            }
            Invincible = false;
            StopCoroutine(flicker);
        }
        #endregion

        protected virtual void CreateHealthBar()
        {
            _healthBar = GetComponent<HealthBar>();
            if (_healthBar)
                _healthBar.Initialization();
        }

        public virtual void OnEnable()
        {
            _health = StartingHealth;
            _currentlyInvulnerable = false;
            _invulnerableTime = 0;
            if (_healthBar)
                _healthBar.SetHealthSlidersValue();
        }

        public virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        public Character Character { get => _character; }
        public float CurrentHealth { get => _health; }
    }
}
