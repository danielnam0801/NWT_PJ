using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    private UIDocument document;
    private VisualElement fadeImage;

    [SerializeField]
    private float fadeTime;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        fadeImage = document.rootVisualElement.Q<VisualElement>("FadeImage");

        //fadeTime = FadeManager
    }

    public void Fade(bool on)
    {
        if (on)
            fadeImage.AddToClassList("on");
        else
            fadeImage.RemoveFromClassList("on");
    }

    //private IEnumerator Delay()
    //{

    //}
}
