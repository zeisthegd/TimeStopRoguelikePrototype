using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Tools
{
    public class FeedbackPlaySound : Feedback
    {
        public FeedbackPlaySoundData Data;
        protected AudioSource _audioSource;

        protected override void Start()
        {
            base.Start();
            InitAudioSource();
        }

        public override void PlayFeedback()
        {
            StopAllCoroutines();
            if (_audioSource == null)
                InitAudioSource();
            _audioSource.gameObject.SetActive(true);
            StartCoroutine(PlaySoundCoroutine());
        }

        protected IEnumerator PlaySoundCoroutine()
        {
            _audioSource.transform.position = this.transform.position;
            _audioSource.Play();
            yield return _audioSource.clip.length;
            _audioSource.gameObject.SetActive(false);
        }

        public virtual void InitAudioSource()
        {
            GameObject audioObject = new GameObject(GetSourceName());
            _audioSource = audioObject.AddComponent<AudioSource>();
            _audioSource.clip = Data.Sound;
            _audioSource.playOnAwake = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public string GetSourceName()
        {
            return "AudioSource_" + Data.Name;
        }
    }
}