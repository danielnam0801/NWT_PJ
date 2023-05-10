using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance;
    private Light2D globalLight;

    [SerializeField]
    private List<Light2D> focusLigths;
    [SerializeField]
    private float focusIntensity = 0.75f;
    [SerializeField]
    private float focusGlobalIntensity = 0.25f;
    [SerializeField]
    private float focusTime = 1f;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        focusLigths = new List<Light2D>();
        globalLight = GetComponent<Light2D>();
    }

    public void SetFocus(bool turnOn)
    {
        StopAllCoroutines();
        StartCoroutine(TurnOnFocusObjectLight(turnOn));
    }

    public void SetGlobalLightIntensity(float intensity)
    {
        globalLight.intensity = intensity;
    }

    public void SetFocusObjectLightIntensity(float value)
    {
        for(int i = 0; i < focusLigths.Count; i++)
        {
            focusLigths[i].intensity = value;
        }
    }

    public void AddFocusObject(GameObject obj)
    {
        focusLigths.Add(obj.GetComponent<Light2D>());
    }

    public void RemoveFocusObject(GameObject obj)
    {
        focusLigths.Remove(obj.GetComponent<Light2D>());
    }

    private IEnumerator TurnOnFocusObjectLight(bool turnOn)
    {
        float current = 0;
        float percent = 0;
        float initGlobalIntensity = globalLight.intensity;
        float initFocusIntensity = focusLigths[0].intensity;
        float endGlobalValue, endFocusValue;

        if(turnOn)
        {
            endGlobalValue = focusGlobalIntensity;
            endFocusValue = focusIntensity;
        }
        else
        {
            endGlobalValue = 1;
            endFocusValue = 0;
        }

        while(current < focusTime)
        {
            current += Time.unscaledDeltaTime;
            percent = current / focusTime;

            SetFocusObjectLightIntensity(Mathf.Lerp(initFocusIntensity, endFocusValue, percent));

            SetGlobalLightIntensity(Mathf.Lerp(initGlobalIntensity, endGlobalValue, percent));

            yield return null;
        }
    }
}
