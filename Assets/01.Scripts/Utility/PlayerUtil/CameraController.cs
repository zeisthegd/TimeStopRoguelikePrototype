using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

using DG.Tweening;

public class CameraController : MonoBehaviour
{
    // public float amplitudeGain;
    // public float frequencyGain;
    // private CinemachineVirtualCamera cineCamBase;

    // void Awake()
    // {
    //     cineCamBase = GetComponent<CinemachineVirtualCamera>();
    // }
    // void Start()
    // {
    //     if (Player.Instance != null)
    //     {
    //         FollowLocalPlayer();
    //     }
    // }
    
    // void Update()
    // {
    //     if (cineCamBase.m_Lens.OrthographicSize != Camera.main.orthographicSize)
    //         cineCamBase.m_Lens.OrthographicSize = Camera.main.orthographicSize;
    // }

    // public void FollowLocalPlayer()
    // {
    //     FollowPlayer(Player.Instance);
    //     ConnectEvents();
    // }

    // public void FollowPlayer(Player player)
    // {
    //     if (player != null)
    //     {
    //         Transform lookAtTarget = Player.Instance.transform;
    //         SetLookAtAndFollow(Player.Instance.transform, lookAtTarget);
    //     }
    //     else
    //         Debug.Log("Player is null!");
    // }

    // public void SetLookAtAndFollow(Transform target)
    // {
    //     if (target != null)
    //     {
    //         cineCamBase.Follow = target;
    //         cineCamBase.LookAt = target;
    //     }
    // }

    // public void SetLookAtAndFollow(Transform followTarger, Transform lookAtTarget)
    // {
    //     if (followTarger != null && lookAtTarget != null)
    //     {
    //         cineCamBase.Follow = followTarger;
    //         cineCamBase.LookAt = lookAtTarget;
    //     }
    // }

    // public void ConfineToTilemap()
    // {
    //     PolygonCollider2D polygonCollider2D = FindObjectOfType<PolygonCollider2D>();
    //     CinemachineConfiner cinemachineConfiner = FindObjectOfType<CinemachineConfiner>();
    //     cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;
    // }

    // public void StartShaking()
    // {
    //     StartCoroutine(ProcessShake(amplitudeGain, frequencyGain));
    // }

    // IEnumerator ProcessShake(float amplitudeGain, float frequencyGain)
    // {
    //     Shake(amplitudeGain, frequencyGain);
    //     yield return new WaitForSeconds(frequencyGain);
    //     Shake(0, 0);
    // }

    // void Shake(float amplitudeGain, float frequencyGain)
    // {
    //     CinemachineBasicMultiChannelPerlin noise = cineCamBase.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    //     noise.m_AmplitudeGain = amplitudeGain;
    //     noise.m_FrequencyGain = frequencyGain;
    // }

    // void ConnectEvents()
    // {
    // }

    // void DisconnectEvents()
    // {
    // }

    // void OnDisable()
    // {
    //     DisconnectEvents();
    // }
}

