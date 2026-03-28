using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera AimCamera;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    void Start()
    {
        perlinNoise = AimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void PulseShake(float intensity, float duration)
    {
        StartCoroutine(PulseCoroutine(intensity, duration));
    }

    private IEnumerator PulseCoroutine(float intensity, float duration)
    {
        perlinNoise.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        perlinNoise.m_AmplitudeGain = 0f;
    }
}
