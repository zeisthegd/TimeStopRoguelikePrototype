using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using NaughtyAttributes;

using Penwyn.Tools;


namespace Penwyn.Game
{
    public class Slowable : MonoBehaviour
    {
        [InfoBox("This components will slow animations speed, rigidbody2D's velocity when the gameObject enter a SlowTimeZone.", EInfoBoxType.Normal)]
        protected Character _character;
        protected bool _isSlowed;
        protected float _currentScale;
        protected Vector2 _velocityJustAfterSlowed;
        protected CharacterController _controller;


        protected virtual void Start()
        {
            _character = gameObject.FindComponent<Character>();
            _controller = gameObject.FindComponent<CharacterController>();

            CharacterTimeZoneControl playerTimeZoneControlAbility = Characters.Player?.FindAbility<CharacterTimeZoneControl>();
            if (playerTimeZoneControlAbility)
                playerTimeZoneControlAbility.TimeScaleChanged += OnTimeScaleChanged;

        }

        protected virtual void Update()
        {
            //_controller?.SetVelocity(_character.Controller.Body2D.velocity * _currentScale);
        }

        protected virtual void UpdateSlowState()
        {

        }

        protected virtual void UpdateNormalState()
        {

        }

        /// <summary>
        /// Slow this gameObject.
        /// </summary>
        public virtual void Slow(float scale)
        {
            this.DOComplete();
            if (_character != null && !_isSlowed)
            {
                _isSlowed = true;
                ChangeTimeScale(scale);
                _velocityJustAfterSlowed = _character.Controller.Body2D.velocity;
            }
        }

        public virtual void Normalize(float duration)
        {
            StartCoroutine(NormalizeCoroutine(duration));
        }

        /// <summary>
        /// Slowly move recover the timescale.
        /// </summary>
        public IEnumerator NormalizeCoroutine(float duration)
        {
            float time = 0;

            while (time < duration && this.gameObject.activeInHierarchy)
            {
                _currentScale = time / duration;
                _character.Model.GetComponent<Animator>().speed = time / duration;
                yield return null;
            }

            _isSlowed = false;
            _currentScale = 1;
            _character.Model.GetComponent<Animator>().speed = 1;
        }

        protected virtual void OnTimeScaleChanged(float scale)
        {
            ChangeTimeScale(scale);
        }

        protected virtual void ChangeTimeScale(float scale)
        {
            _currentScale = scale;
            _character.Model.GetComponent<Animator>().speed = scale;
        }

        protected virtual void OnDisable()
        {
            StopAllCoroutines();
        }

        public bool IsSlowed { get => _isSlowed; }
        public float CurrentScale { get => _currentScale; }
    }
}
