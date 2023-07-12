using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    private Image fadeImage;

    [SerializeField]
    private float fadeTime;
    [SerializeField]
    private Color fadeColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        fadeImage = transform.Find("Image").GetComponent<Image>();
    }

    public void StartFade(float startValue, float endValue, Action completeAction = null, float duration = -1)
    {
        if (duration == -1)
            duration = fadeTime;

        StartCoroutine(Fade(startValue, endValue, completeAction, duration));
    }

    private IEnumerator Fade(float startValue, float endValue, Action completeAction = null, float duration = -1)
    {
        fadeImage.gameObject.SetActive(true);

        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, startValue);

        float current = 0;
        float percent = 0;
        float value = endValue - startValue;
        while(percent <= 1)
        {
            current += Time.deltaTime;
            percent = current / duration;

            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, Mathf.Lerp(startValue, endValue, percent));

            yield return null;  
        }

        if(completeAction == null)
            fadeImage.gameObject.SetActive(false);

        completeAction?.Invoke();
    }
}
