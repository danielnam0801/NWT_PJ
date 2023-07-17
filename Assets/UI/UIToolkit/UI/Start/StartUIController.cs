using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartUIController : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        VisualElement background = root.Q("Background");
        Button startBtn = root.Q<Button>("StartBtn");
        Button settingBtn = root.Q<Button>("SettingBtn");
        Button exitBtn = root.Q<Button>("ExitBtn");

        startBtn.RegisterCallback<ClickEvent>(e =>
        {
            FadeManager.Instance.FadeOneShot(() =>
            {
                SceneManager.LoadScene("MergeScene 2");
            }, 2, 1);
        });
        settingBtn.RegisterCallback<ClickEvent>(e =>
        {
            //세팅만들면
        });
        exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            Application.Quit();
        });
    }
}
