using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeMuteToggle : MonoBehaviour
{
    AudioSource audioSource;

    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = soundManager.seSource;
    }

    public void Mute()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
        }
    }
}
