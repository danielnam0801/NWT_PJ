using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    public Color FadeColor;

    private UIDocument document;
    private VisualElement fade;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        document = transform.Find("Fade").GetComponent<UIDocument>();
        fade = document.rootVisualElement.Q("fade");
        fade.style.display = DisplayStyle.None;
    }

    public void FadeOneShot(Action fadeOutAction = null, float time = 0, float maintainTime = 0)
    {
        Fade(0, 1, () =>
        {
            fadeOutAction?.Invoke();

            StartCoroutine(Delay(maintainTime, () =>
            {
                Fade(1, 0, () =>
                {
                    fade.style.display = DisplayStyle.None;
                }, time);
            }));
        }, time);
    }

    public void Fade(float startValue, float endValue, Action completeAction = null, float time = 1)
    {
        fade.style.display = DisplayStyle.Flex;

        fade.style.backgroundColor = new Color(FadeColor.r, FadeColor.g, FadeColor.b, startValue);

        fade.style.transitionDuration = new List<TimeValue>() { new TimeValue(time) };

        fade.style.backgroundColor = new Color(FadeColor.r, FadeColor.g, FadeColor.b, endValue);

        StartCoroutine(Delay(time, completeAction));
    }

    private IEnumerator Delay(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action?.Invoke();
    }
}
