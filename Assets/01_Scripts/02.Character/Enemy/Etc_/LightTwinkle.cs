using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public enum LightTwinkleType
{
    Fixed, Constant, Twinkle, Shake
}

public class LightTwinkle : MonoBehaviour
{
    [Header("사용여부 체크")]
    [SerializeField]
    private bool IsLightUse = false;
    [SerializeField]
    private bool ColorChangeEffect = true, IntensityChangeEffect = true, RadiusChangeEffect = true;

    [Header("속성값")]
    public LightTwinkleType lightTwinkleType;
    [SerializeField] private float lightSpeed = 1f;
    [SerializeField] private float impactIntensityValue = 3f;
    [SerializeField] private float impactRadiusValue = 3f;
    [SerializeField] private Color attackColor = Color.red;

    [Space(30)]
    [SerializeField]
    private bool isLightOn = false;
    [SerializeField] private float valueChangeSpeed = 0.2f;

    public bool IsLightOn
    {
        get { return isLightOn; }
        private set { isLightOn = value; }
    }

    
    Light2D _light;
    private float firstIntensity;
    private float firstRadius;
    private Color firstInColor;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        firstIntensity = _light.intensity;
        firstRadius = _light.pointLightOuterRadius;
        firstInColor = _light.color;
    }

    private void Start()
    {
        if (IsLightUse)
        {
            TypeCheck();
        }
    }

    private void TypeCheck()
    {
        switch (lightTwinkleType)
        {
            case LightTwinkleType.Fixed:
                FixedTypeSetIntensity(firstIntensity, firstInColor, firstRadius); // 그냥 켜놓는거
                break;
            case LightTwinkleType.Constant:
                StartCoroutine("ConstantLight");
                break;
            case LightTwinkleType.Twinkle:
                StartCoroutine("TwinkleLight");
                break;
            case LightTwinkleType.Shake:
                Debug.LogError("아직 미구현");
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

    public void FixedTypeSetIntensity(float lightintensity, Color color, float radius)
    {
        if(ColorChangeEffect)
            DOVirtual.Color(_light.color, color, valueChangeSpeed, v => _light.color = v);
        else 
            _light.color = color;
      
        if(IntensityChangeEffect)
            DOVirtual.Float(_light.intensity, lightintensity, valueChangeSpeed, v => _light.intensity = v);
        else 
            _light.intensity = lightintensity;

        if (RadiusChangeEffect)
            DOVirtual.Float(_light.pointLightOuterRadius, radius, valueChangeSpeed, v=> _light.pointLightOuterRadius = v);
    }

    public void StopAll()
    {
        StopAllCoroutines();
    }

    public void AttackLightOn()
    {
        StopAll();
        IsLightOn = true;
        FixedTypeSetIntensity(impactIntensityValue, attackColor, impactRadiusValue);
    }

    public void LightInit()
    {
        IsLightOn = false;
        FixedTypeSetIntensity(firstIntensity, firstInColor, firstRadius);
        TypeCheck();
    }

    public void EnemyDieLightOff()
    {
        DOVirtual.Float(_light.intensity, 0, 1.5f, v => _light.intensity = v);
    }


}