using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    private Image fadePanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        fadePanel = transform.Find("FadePanel").GetComponent<Image>();
    }

    public void Fade(float endValue, float duration)
    {
        fadePanel.gameObject.SetActive(true);

        fadePanel.DOFade(endValue, duration).OnComplete(() =>
        {
            if(endValue == 0)
                fadePanel.gameObject.SetActive(false);
        });
    }
}
