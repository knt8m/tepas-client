using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmSlider : MonoBehaviour
{
    public Slider slider;

    AudioSource audioSource;

    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = soundManager.bgmSource;
        slider.value = audioSource.volume;
        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
    }

    void Load()
    {
        slider.value = audioSource.volume;
    }
}
