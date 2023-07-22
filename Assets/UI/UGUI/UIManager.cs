using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider HealthBar;
    public Slider ManaBar;
    public GameObject SettingUI;
    public TextMeshProUGUI TimerText;
    public AudioSource BGM;
    public AudioSource SFX;
    public GameObject HUD;

    public bool isTimerTick;
    public bool settingActive;
    //public bool SettingUIActive = true;

    int minute = 0;
    float second = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    private void Start()
    {
        isTimerTick = true;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SettingEnable();
        //}
        if (isTimerTick)
        {
            second += Time.deltaTime;
            if (second >= 60)
            {
                second = 0;
                minute++;
            }
            //if (minute > 0)
            //    TimerText.text = "Time: " + minute.ToString() + "." + second.ToString("F0");
            //else
            //    TimerText.text = "Time: " + "0" + "." + second.ToString("F0");
        }
    }

    public void SetHealth(int health)
    {
        HealthBar.value = health;
    }
    public void SettingEnable(bool value)
    {
        //SettingUIActive = !SettingUIActive;
        SettingUI.SetActive(value);
        isTimerTick = value;
    }
    public void SetBGMVolume(float volume)
    {
        BGM.volume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        SFX.volume = volume;
    }
}
