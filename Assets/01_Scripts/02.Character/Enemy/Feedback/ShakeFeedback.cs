using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeFeedback : Feedback
{
    [SerializeField] CinemachineVirtualCamera _cmVCam;
    
    [SerializeField]
    [Range(0, 5f)]
    private float _amplitude = 1, _intensity = 1;
    [SerializeField]
    [Range(0, 1f)]
    private float _duration = 0.1f;
    private CinemachineBasicMultiChannelPerlin _noise;

    private void OnEnable()
    {
        if (_cmVCam == null) Debug.LogError("ShakeFeedback¿¡ Vcam¾øÀ½");
        _noise = _cmVCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CreateFeedBack()
    {
        _noise.m_AmplitudeGain = _amplitude;
        _noise.m_FrequencyGain = _intensity;
        StartCoroutine(ShakeCoroutine());
    }

    public override void FinishFeedBack()
    {
        StopAllCoroutines();
        _noise.m_AmplitudeGain = 0;
    }

    IEnumerator ShakeCoroutine()
    {
        float time = _duration;
        while (time > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / _duration);
            yield return null;
            time -= Time.deltaTime;
        }
        _noise.m_AmplitudeGain = 0;
    }

}