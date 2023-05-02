using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera playerCam;

    public void Awake()
    {
        playerCam = GameObject.Find("Cam/PlayerCam").GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        ShakePlayerCam(6, 1);
    }

    public void ShakePlayerCam(float amplitude = 1, float frequency = 1, float time = 1)
    {
        StartCoroutine(Shake(playerCam, amplitude, frequency, time));
    }

    public void ShakeCam(CinemachineVirtualCamera cam, float amplitude = 1, float frequency = 1, float time = 1)
    {
        StartCoroutine(Shake(cam, amplitude, frequency, time));
    }

    private IEnumerator Shake(CinemachineVirtualCamera cam = null, float amplitude = 1, float frequency = 1, float time = 1)
    {
        CinemachineBasicMultiChannelPerlin camPerlin;

        camPerlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        camPerlin.m_AmplitudeGain = 0;
        camPerlin.m_FrequencyGain = frequency;
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            camPerlin.m_AmplitudeGain = Mathf.Lerp(amplitude, 0, percent);

            yield return null;
        }

        camPerlin.m_AmplitudeGain = 0;
    }
}
