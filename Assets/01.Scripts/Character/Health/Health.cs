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

        public event UnityAction OnHit;
        public event UnityAction OnDeath;

        void Start()
        {
            _character = GetComponent<Character>();
            _health = StartingHealth;
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
                    OnHit?.Invoke();
                }
                else
                {
                    Kill();
                }
            }
        }
        #endregion

        #region Kill
        public virtual void Kill()
        {
            _health = 0;
            OnDeath?.Invoke();
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

        public virtual void OnEnable()
        {
            _health = StartingHealth;
            _currentlyInvulnerable = false;
            _invulnerableTime = 0;
        }

        public virtual void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}
