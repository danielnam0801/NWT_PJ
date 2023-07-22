using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] sfx = null; // 효과음
    [SerializeField] Sound[] bgm = null; // 배경음악

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    Dictionary<string, Sound> bgmSounds = new Dictionary<string, Sound>();
    Dictionary<string, Sound> sfxSounds = new Dictionary<string, Sound>();

    public int sfxPlayerCount = 50;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        CreateAudioDic();

        sfxPlayer = new AudioSource[sfxPlayerCount];

        for(int i = 0; i < sfxPlayerCount; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            sfxPlayer[i] = source;
        }
    }

    private void CreateAudioDic()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgmSounds[bgm[i].name] = bgm[i];
        }

        for (int i = 0; i < sfx.Length; i++)
        {
            sfxSounds[sfx[i].name] = sfx[i];
        }
    }

    public void PlayBGM(string p_bgmName)
    {
        if (bgmSounds.TryGetValue(p_bgmName, out Sound sound) == false)
        {
            Debug.Log(p_bgmName + " 이름의 배경음악이 없습니다.");
            return;
        }

        bgmPlayer.clip = bgmSounds[p_bgmName].clip;
        bgmPlayer.Play();
    }

    public AudioSource PlaySFX(string p_sfxName)
    {
        if(sfxSounds.TryGetValue(p_sfxName, out Sound sound) == false)
        {
            Debug.Log(p_sfxName + " 이름의 효과음이 없습니다.");
            return null;
        }

        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            // SFXPlayer에서 재생 중이지 않은 Audio Source를 발견했다면 
            if (!sfxPlayer[i].isPlaying)
            {
                sfxPlayer[i].clip = sfxSounds[p_sfxName].clip;
                sfxPlayer[i].Play();
                return sfxPlayer[i];
            }
        }

        GameObject obj = new GameObject();
        obj.transform.SetParent(transform);
        AudioSource source = obj.AddComponent<AudioSource>();
        sfxPlayer[sfxPlayer.Length] = source;
        source.clip = sfxSounds[p_sfxName].clip;
        source.Play();

        Debug.Log("모든 오디오 플레이어가 재생중입니다.");

        return source;
    }

    public void SetBGMVolume(float startValue, float endValue, float time)
    {
        StartCoroutine(SetBGMVolume_Coroutine(startValue, endValue, time));
    }

    private IEnumerator SetBGMVolume_Coroutine(float startValue, float endValue, float time)
    {
        Debug.Log(1);
        bgmPlayer.volume = startValue;

        if(time <= 0)
        {
            bgmPlayer.volume = endValue;
            yield break;    
        }

        float current = 0;
        float percent = 0;


        while(percent <= 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            bgmPlayer.volume = Mathf.Lerp(startValue, endValue, percent);

            yield return null;
        }
    }
}
