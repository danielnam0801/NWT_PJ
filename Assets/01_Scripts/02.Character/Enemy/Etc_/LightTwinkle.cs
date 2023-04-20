using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum LightTwinkleType
{
    constant, twinkle, shake
}

public class LightTwinkle : MonoBehaviour
{
    public bool IsLightUse { get; set; }
    public bool IsLightOn { get; set; }
    
    public LightTwinkleType lightTwinkleType;
    Light2D _light;

    [SerializeField] private float lightSpeed = 1f;
    private float firstIntensity;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    private void Start()
    {
        if (IsLightUse)
        {
            LightOn();
            TypeCheck();
        }
        else LightOff();

    }

    private void TypeCheck()
    {
        switch (lightTwinkleType)
        {
            case LightTwinkleType.constant:
                StartCoroutine("ConstantLight");
                break;
            case LightTwinkleType.twinkle:
                StartCoroutine("TwinkleLight");
                break;
            case LightTwinkleType.shake:
                StartCoroutine("ShakeLight");
                break;
            default:
                Debug.LogError("Error");
                break;
        }
    }

    IEnumerator ShakeLight()
    {
        while (true) { 

            yield return null;
        }
    }

    IEnumerator TwinkleLight()
    {
        while (true) {
            _light.intensity = firstIntensity * (Mathf.Sin(Time.time) + 1) / 2;
            yield return null;
        }
    }

    IEnumerator ConstantLight()
    {
        while (true)
        {
            float value = firstIntensity * (Mathf.Sin(Time.time));
            _light.intensity = Mathf.Clamp(value, 0, firstIntensity);

            yield return null;
        }
    }

    public void LightOn()
    {
        IsLightOn = true;
        _light.enabled = true;
    }

    public void LightOff()
    {
        IsLightOn = false;
        _light.enabled = false;
    }


}