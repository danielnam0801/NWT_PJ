using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum Display
{
    모두표시 = 0,
    HUD숨기기,
    모두숨기기,
}
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
        DropdownField ResolutionDropdown = root.Q<DropdownField>("ResolutionDropdown");
        Toggle UIToggle = root.Q<Toggle>("UIToggle");
        Slider BGMSlider = root.Q<Slider>("BGM");
        Slider EffectSlider = root.Q<Slider>("Effect");
        Toggle TimerToggle = root.Q<Toggle>("TimerToggle");
        Button ReturnBtn = root.Q<Button>("Return");
        Button NewGameBtn = root.Q<Button>("NewGame");
        Button ExitBtn = root.Q<Button>("Exit");


        resolutions.AddRange(Screen.resolutions);
        foreach (Resolution resolution in resolutions)
        {
            resolutionStringList.Add(resolution.width + "x" + resolution.height); // 들어감
        }

        ResolutionDropdown.choices = resolutionStringList;
        ResolutionDropdown.RegisterValueChangedCallback(v =>
        {
            resolutionWidth = resolutions[ResolutionDropdown.index].width;
            resolutionHeight = resolutions[ResolutionDropdown.index].height;
            if (resolutionWidth != Screen.width || resolutionHeight != Screen.height)
            {
                Screen.SetResolution(resolutionWidth, resolutionHeight, fullScreen);
            }
        });

        fullScreenToggle.RegisterCallback<ClickEvent>(e =>
        {
            fullScreen = fullScreenToggle.value;
            Screen.fullScreen = fullScreen;
        });

        BGMSlider.RegisterValueChangedCallback(v =>
        {
            UIManager.Instance.SetBGMVolume(v.newValue);
            Debug.Log(v.newValue);
        });

        EffectSlider.RegisterValueChangedCallback(v =>
        {
            UIManager.Instance.SetSFXVolume(v.newValue);
            Debug.Log(v.newValue);
        });

        TimerToggle.RegisterCallback<ClickEvent>(e =>
        {
            Timer.SetActive(!Timer.activeSelf);
        });

        UIToggle.RegisterValueChangedCallback(v =>
        {
            UIManager.Instance.HUD.SetActive(!UIManager.Instance.HUD.activeSelf);
            UIManager.Instance.isTimerTick = !UIManager.Instance.isTimerTick;
        });

        ReturnBtn.RegisterCallback<ClickEvent>(e =>
        {
            UIManager.Instance.SettingEnable();
        });

        NewGameBtn.RegisterCallback<ClickEvent>(e =>
        {

        });

        ExitBtn.RegisterCallback<ClickEvent>(e =>
        {
            Application.Quit();
        });
    }
}
