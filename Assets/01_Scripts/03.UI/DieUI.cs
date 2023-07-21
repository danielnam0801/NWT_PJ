using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class DieUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        document.enabled = false;
    }
    
    public void Active()
    {
        document.enabled = true;

        root = document.rootVisualElement;
        VisualElement restartBtn = root.Q<VisualElement>("restartBtn");
        VisualElement quitBtn = root.Q<VisualElement>("quitBtn");

        restartBtn.RegisterCallback<ClickEvent>(e =>
        {
            FadeManager.Instance.FadeOneShot(() => SceneManager.LoadScene("MergeScene 2"), 2, 1);
        });

        quitBtn.RegisterCallback<ClickEvent>(e =>
        {
            FadeManager.Instance.FadeOneShot(() => SceneManager.LoadScene("Intro"), 2, 1);
        });
    }
}
