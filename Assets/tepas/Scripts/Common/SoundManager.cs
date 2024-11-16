using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public GameManager gameManager;

    [SerializeField]
    public AudioSource bgmSource;

    [SerializeField]
    public AudioClip[] bgmClips;

    [SerializeField]
    public AudioSource seSource;

    [SerializeField]
    public AudioClip[] seClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlayBgm("bgm04");
            LoadBgmVolume();
            LoadBgmMute();
            LoadSeVolume();
            LoadSeMute();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
    }

    public void LoadBgmVolume()
    {
        bgmSource.volume = gameManager.gameData.bgmVolume;
    }

    public void SaveBgmVolume()
    {
        gameManager.gameData.bgmVolume = bgmSource.volume;
        gameManager.SaveGameData();
    }

    public void LoadBgmMute()
    {
        Debug.Log("gameData: " + gameManager.gameData.bgmMute);
        Debug.Log("bgmSource: " + bgmSource.mute);
        bgmSource.mute = gameManager.gameData.bgmMute;
    }

    public void SaveBgmMute()
    {
        gameManager.gameData.bgmMute = bgmSource.mute;
        gameManager.SaveGameData();
    }

    public void LoadSeVolume()
    {
        seSource.volume = gameManager.gameData.seVolume;
    }

    public void SaveSeVolume()
    {
        gameManager.gameData.seVolume = seSource.volume;
        gameManager.SaveGameData();
    }

    public void LoadSeMute()
    {
        seSource.mute = gameManager.gameData.seMute;
    }

    public void SaveSeMute()
    {
        gameManager.gameData.seMute = seSource.mute;
        gameManager.SaveGameData();
    }

    public void PlayBgm(string name)
    {
        AudioClip clip = FindAudioClip(bgmClips, name);
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySe(string name)
    {
        AudioClip clip = FindAudioClip(seClips, name);
        if (clip != null)
        {
            seSource.PlayOneShot(clip);
        }
    }

    private AudioClip FindAudioClip(AudioClip[] clips, string name)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
                return clip;
        }
        Debug.LogWarning($"AudioClip '{name}' not found!");
        return null;
    }




}
