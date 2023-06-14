using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider musicSoundSlider;


    public static bool soundValueHasBeenSet = false;
    private const string SoundValueHasBeenSetKey = "SoundValueHasBeenSet";

    void Start()
    {
        if (!soundValueHasBeenSet)
        {
            musicSoundSlider.value = 1f;
            PlayerPrefs.SetFloat("SoundValue", 1f);
            soundValueHasBeenSet = true;
            PlayerPrefs.SetInt(SoundValueHasBeenSetKey, soundValueHasBeenSet ? 1 : 0);
        }
        else
        {
            musicSoundSlider.value = PlayerPrefs.GetFloat("SoundValue");
        }
    }

    void Awake()
    {
        soundValueHasBeenSet = PlayerPrefs.GetInt(SoundValueHasBeenSetKey, 0) == 1;
    }

    public void ToggleMusic()
    {
        AudioManager.audioInstence.ToggleMusic();
    }

    public void MusicVolume()
    {
        AudioManager.audioInstence.AllMusicVolume(musicSoundSlider.value);
        PlayerPrefs.SetFloat("SoundValue", musicSoundSlider.value);
    }

}
