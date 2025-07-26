using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource SFXSource;


    public AudioClip musicClip;
    public AudioClip clickNutClip;
    public AudioClip clickBtnClip;
    public AudioClip sortNutClip;
    public AudioClip complateBoltClip;


    // SettingSound settingSound;

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadSettingVolume();

    }
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
        // Debug.Log(SaveSystem.Data.settingSound.)
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlaySFX(AudioClip audioSFX)
    {
        SFXSource.PlayOneShot(audioSFX);
    }
    public void MusicVolume(float volume)
    {
        if (volume <= 0)
        {
            musicAudioSource.mute = false;
        }
        musicAudioSource.volume = volume;

    }
    public void SFXVolume(float volume)
    {
        if (volume <= 0)
        {
            SFXSource.mute = false;
        }
        SFXSource.volume = volume;

    }


    public void LoadSettingVolume()
    {
        SettingSound setting = SaveSystem.LoadFileData().settingSound;
        // Debug.Log("Load : " + setting.musicVolume + " " + setting.sfxVolume);

        musicAudioSource.volume = setting.musicVolume;
        SFXSource.volume = setting.sfxVolume;

    }
    public SettingSound GetSettingVolume()
    {
        SettingSound setting = new SettingSound();
        setting.musicVolume = musicAudioSource.volume;
        setting.sfxVolume = SFXSource.volume;
        return setting;
    }
}
