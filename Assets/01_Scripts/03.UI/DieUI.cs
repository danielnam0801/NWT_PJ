using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class DieUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    private VisualElement group;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        document.enabled = false;
        root = document.rootVisualElement;
        group = root.Q<VisualElement>("group");
    }

    private void Start()
    {
        VisualElement restartBtn = root.Q<VisualElement>("restartBtn");
        VisualElement quitBtn = root.Q<VisualElement>("quitBtn");

        restartBtn.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            FadeManager.Instance.FadeOneShot(() => SceneManager.LoadScene("MergeScene 2"), 2, 1);
        });

        quitBtn.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            FadeManager.Instance.FadeOneShot(() => SceneManager.LoadScene("Intro"), 2, 1);
        });
    }

    public void Active()
    {
        group.style.display = DisplayStyle.Flex;
    }
}
