using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField]
    private float lerpTime = 0.3f;

    private float defaultTimeScale;
    public float DefaultTimeScale { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        defaultTimeScale = Time.timeScale;
    }

    public void SetTimeScale(float value, bool isLerp = false)
    {
        if(!isLerp)
        {
            Time.timeScale = value;
        }
        else
        {
            StartCoroutine(SetTimeScaleToLerp(value));
        }
    }

    public void ResetTimeScale()
    {
        Time.timeScale = defaultTimeScale;
    }

    private IEnumerator SetTimeScaleToLerp(float value)
    {
        float currentTime = 0;
        float percent = 0;
        float originScale = Time.timeScale;

        while(percent < 1)
        {
            currentTime += Time.unscaledDeltaTime;
            percent = currentTime / lerpTime;

            Time.timeScale = Mathf.Lerp(originScale, value, percent);

            yield return null;
        }
    }
}
