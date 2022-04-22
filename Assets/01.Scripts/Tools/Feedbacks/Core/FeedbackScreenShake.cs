using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Penwyn.Tools
{
    public class FeedbackScreenShake : Feedback
    {
        public float ShakeDuration = 1;
        public float AmplitudeGain = 1;
        public float FrequencyGain = 1;

        protected static float _shakeElapsedTime = 0;
        protected CinemachineVirtualCamera _virtualCamera;
        protected CinemachineBasicMultiChannelPerlin _virtualCameraNoise;


        protected override void Start()
        {
            base.Start();
            _virtualCamera = FindObjectOfType<CameraController>().GetComponent<CinemachineVirtualCamera>();
            if (_virtualCamera)
                _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        protected override void Update()
        {
            base.Update();
            if (_shakeElapsedTime >= 0)
            {
                _shakeElapsedTime -= Time.deltaTime;
                if (_shakeElapsedTime < 0)
                    StopFeedback();
            }
        }

        public override void PlayFeedback()
        {
            base.PlayFeedback();
            if (_virtualCamera != null && _virtualCameraNoise != null)
            {
                _virtualCameraNoise.m_AmplitudeGain = AmplitudeGain;
                _virtualCameraNoise.m_FrequencyGain = FrequencyGain;
                _shakeElapsedTime = ShakeDuration;
            }
        }

        public override void StopFeedback()
        {
            base.StopFeedback();
            if (_virtualCamera != null && _virtualCameraNoise != null)
            {
                _virtualCameraNoise.m_AmplitudeGain = 0;
                _virtualCameraNoise.m_FrequencyGain = 0;
                _shakeElapsedTime = -1;
            }
        }
    }
}
