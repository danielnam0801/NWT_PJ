using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Setting : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    List<String> resolutionStringList = new List<String>();

    private bool fullScreen; // 토글로 제어
    private int resolutionWidth;
    private int resolutionHeight;

    public GameObject Timer;

    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement root = ui.rootVisualElement;
        Toggle fullScreenToggle = root.Q<Toggle>("fullScreen");
        DropdownField dropdown = root.Q<DropdownField>();
        Slider SoundSlider1 = root.Q<Slider>("SoundSlider1");
        Toggle TimerToggle = root.Q<Toggle>("TimerToggle");

        resolutions.AddRange(Screen.resolutions);

        foreach (Resolution resolution in resolutions)
        {
            resolutionStringList.Add(resolution.width + "x" + resolution.height); // 들어감
        }
        dropdown.choices = resolutionStringList;
        dropdown.RegisterValueChangedCallback(v =>
        {
            resolutionWidth = resolutions[dropdown.index].width;
            resolutionHeight = resolutions[dropdown.index].height;
            if (resolutionWidth != Screen.width || resolutionHeight != Screen.height)
            {
                Screen.SetResolution(resolutionWidth, resolutionHeight, fullScreen);
            }
        });

        fullScreenToggle.RegisterCallback<ClickEvent>(e =>
        {
            Debug.Log("fullScreen토글클릭");
            fullScreen = fullScreenToggle.value;
            Screen.fullScreen = fullScreen;
            Debug.Log(fullScreen);
        });

        SoundSlider1.RegisterValueChangedCallback(v =>
        {
            Debug.Log(v.newValue);
        });

        TimerToggle.RegisterCallback<ClickEvent>(e =>
        {
            Timer.SetActive(!Timer.activeSelf);
        });
        //toggle.onValueChanged.AddListener(delegate
        //{
        //    ToggleValueChanged(m_Toggle);
        //});

    }
}
