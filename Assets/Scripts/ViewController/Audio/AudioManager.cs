using System;
using System.Collections;
using System.Collections.Generic;
using Elvenwood;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : AbstractController
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    
    public AudioSource fxSource;
    public AudioSource bgmSource;
    
    
    [Header("声音资源")]
    public AudioClip confirmAudio;

    public AudioClip bgmAudio;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayBGM(bgmAudio);
    }

    public void OnPlayConfirmAudio()
    {
        fxSource.clip = confirmAudio;
        fxSource.Play();
    }

    public void OnFXVolumeChange(float amount)
    {
        //
        audioMixer.SetFloat("FXVolume", -10 / amount + 10);
    }
    public void OnBGMVolumeChange(float amount)
    {
        audioMixer.SetFloat("BGMVolume", -10 / amount + 10);
    }

    public void PlayFXSound(AudioClip newAudioClip)
    {
        fxSource.clip = newAudioClip;
        fxSource.Play();
    }

    public void PlayFX(AudioClip newFX)
    {
        fxSource.clip = newFX;
        fxSource.Play();
    }

    public void PlayBGM(AudioClip newBgm)
    {
        bgmSource.clip = newBgm;
        bgmSource.Play();
    }
    
}
