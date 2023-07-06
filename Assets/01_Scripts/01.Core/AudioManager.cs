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

    [SerializeField] Sound[] sfx = null; // ȿ����
    [SerializeField] Sound[] bgm = null; // �������

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    Dictionary<string, Sound> bgmSounds = new Dictionary<string, Sound>();
    Dictionary<string, Sound> sfxSounds = new Dictionary<string, Sound>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateAudioDic();
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
        if (sfxSounds.TryGetValue(p_bgmName, out Sound sound) == false)
        {
            Debug.Log(p_bgmName + " �̸��� ��������� �����ϴ�.");
            return;
        }

        bgmPlayer.clip = bgmSounds[p_bgmName].clip;
        bgmPlayer.Play();
    }

    public void PlaySFX(string p_sfxName)
    {
        if(sfxSounds.TryGetValue(p_sfxName, out Sound sound) == false)
        {
            Debug.Log(p_sfxName + " �̸��� ȿ������ �����ϴ�.");
            return;
        }

        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            // SFXPlayer���� ��� ������ ���� Audio Source�� �߰��ߴٸ� 
            if (!sfxPlayer[i].isPlaying)
            {
                sfxPlayer[i].clip = sfxSounds[p_sfxName].clip;
                sfxPlayer[i].Play();
                return;
            }
        }
        Debug.Log("��� ����� �÷��̾ ������Դϴ�.");
        return;
    }
}
