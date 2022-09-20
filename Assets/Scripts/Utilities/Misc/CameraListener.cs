using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraListener : MonoBehaviour
{
    public static CameraListener instance;

    private CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin channelPerlin;

    public void Awake()
    {
        instance = this;

        cinemachine = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void CameraShake(float intensity, float duration)
    {
        StartCoroutine(ApplyShake(intensity, duration));
    }

    public IEnumerator ApplyShake(float intensity, float duration)
    {
        channelPerlin.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        channelPerlin.m_AmplitudeGain = 0;
    }
}
