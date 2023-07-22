using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class IntroSetting : MonoBehaviour
{
    List<Resolution> resolutions = new List<Resolution>();
    List<String> resolutionStringList = new List<String>();

    private bool fullScreen; // ��۷� ����
    private int resolutionWidth;
    private int resolutionHeight;

    private UIDocument document;
    private VisualElement root;
    private VisualElement group;

    [SerializeField]
    private bool isActive = false;

    public UnityEvent EnableEvent;
    public UnityEvent DisableEvent;

    //public GameObject Timer;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        group = root.Q<VisualElement>("group");
    }

    private void Start()
    {
        group.style.display = DisplayStyle.None;
        DisableEvent?.Invoke();
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        TimeManager.Instance.SetTimeScale(1);
    }

    private void OnEnable()
    {
        Toggle fullScreenToggle = root.Q<Toggle>("fullScreen");
        DropdownField ResolutionDropdown = root.Q<DropdownField>("ResolutionDropdown");
        //Toggle UIToggle = root.Q<Toggle>("UIToggle");
        Slider BGMSlider = root.Q<Slider>("BGM");
        Slider EffectSlider = root.Q<Slider>("Effect");
        Toggle TimerToggle = root.Q<Toggle>("TimerToggle");
        Button ReturnBtn = root.Q<Button>("Return");
        Button NewGameBtn = root.Q<Button>("NewGame");
        Button ExitBtn = root.Q<Button>("Exit");

        EnableEvent?.Invoke();
        //UnityEngine.Cursor.lockState = CursorLockMode.None;

        resolutions.AddRange(Screen.resolutions);
        foreach (Resolution resolution in resolutions)
        {
            resolutionStringList.Add(resolution.width + "x" + resolution.height); // ��
        }

        ResolutionDropdown.choices = resolutionStringList;
        ResolutionDropdown.RegisterValueChangedCallback(v =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            resolutionWidth = resolutions[ResolutionDropdown.index].width;
            resolutionHeight = resolutions[ResolutionDropdown.index].height;
            if (resolutionWidth != Screen.width || resolutionHeight != Screen.height)
            {
                Screen.SetResolution(resolutionWidth, resolutionHeight, fullScreen);
            }
        });

        fullScreenToggle.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            fullScreen = fullScreenToggle.value;
            Screen.fullScreen = fullScreen;
        });

        BGMSlider.RegisterValueChangedCallback(v =>
        {
            //UIManager.Instance.SetBGMVolume(v.newValue);
            AudioManager.Instance.SetBGMVolume(v.newValue);
            Debug.Log(v.newValue);
        });

        EffectSlider.RegisterValueChangedCallback(v =>
        {
            //UIManager.Instance.SetSFXVolume(v.newValue);
            AudioManager.Instance.SetSFXVlume(v.newValue);
            Debug.Log(v.newValue);
        });

        TimerToggle.RegisterCallback<ClickEvent>(e =>
        {
            //Timer.SetActive(!Timer.activeSelf);
            AudioManager.Instance.PlaySFX("BtnClickSound");
        });

        //UIToggle.RegisterValueChangedCallback(v =>
        //{
        //    AudioManager.Instance.PlaySFX("BtnClickSound");
        //    UIManager.Instance.HUD.SetActive(!UIManager.Instance.HUD.activeSelf);
        //    UIManager.Instance.isTimerTick = !UIManager.Instance.isTimerTick;
        //});

        ReturnBtn.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            Debug.Log(2);
            SetActive();

            //UIManager.Instance.SettingEnable(false);
        });

        NewGameBtn.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
        });

        ExitBtn.RegisterCallback<ClickEvent>(e =>
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            Application.Quit();
        });


        TimeManager.Instance.SetTimeScale(0);
    }

    public void SetActive()
    {
        if (FadeManager.Instance.isFade)
        {
            return;
        }

        isActive = !isActive;
        //UIManager.Instance.settingActive = isActive;

        if (isActive)
        {
            AudioManager.Instance.PlaySFX("BtnClickSound");
            group.style.display = DisplayStyle.Flex;
            EnableEvent?.Invoke();
            //UnityEngine.Cursor.lockState = CursorLockMode.None;
            //TimeManager.Instance.SetTimeScale(0);
        }
        else
        {
            group.style.display = DisplayStyle.None;
            DisableEvent?.Invoke();
            //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            //TimeManager.Instance.SetTimeScale(1);
        }
    }
}