using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioInstence;
    public Sound[] musicSound,sfxMusic;
    public AudioSource musicSource ,SfxSource;
   

    void Awake()
    {
        if (audioInstence == null)
        {
            audioInstence = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");

        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySfxMusic(string name)
    {
        Sound s = Array.Find(sfxMusic, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");

        }
        else
        {
            SfxSource.clip = s.clip;
            SfxSource.Play();
        }
    }
    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");

        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Stop();
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void AllMusicVolume(float volume)
    {
        musicSource.volume = volume;
        SfxSource.volume = volume;
  
    }
}
