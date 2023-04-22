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
    [SerializeField]
    private bool IsLightUse = false;
    [SerializeField]
    private bool isLightOn = false;
    public bool IsLightOn
    {
        get { return isLightOn; }
        set { isLightOn = value; }
    }

    public LightTwinkleType lightTwinkleType;
    Light2D _light;

    [SerializeField] private float lightSpeed = 1f;
    private float firstIntensity;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        firstIntensity = _light.intensity;
    }

    private void Start()
    {
        if (IsLightUse)
        {
            LightOn();
            TypeCheck();
        }
        else
        {
            LightOff();
        }
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
        while(true)
        {
            float value = (firstIntensity * (Mathf.Sin(Time.time * lightSpeed) + 1) / 2);
            _light.intensity = Mathf.Clamp(value, 0, firstIntensity);
            yield return null;
        }
    }

    IEnumerator ConstantLight()
    {
        while (true)
        {
            float value = firstIntensity * (Mathf.Sin(Time.time * lightSpeed));
            _light.intensity = Mathf.Clamp(value, 0, firstIntensity);

            yield return null;
        }
    }

    public void SetLightIntensity(float lightintensity)
    {
        _light.intensity = lightintensity;
    }
    

    public void StopAll()
    {
        StopAllCoroutines();
    }

    public void LightOn()
    {
        IsLightOn = true;
        SetLightIntensity(firstIntensity);
    }

    public void LightOff()
    {
        IsLightOn = false;
        SetLightIntensity(0);
    }


}