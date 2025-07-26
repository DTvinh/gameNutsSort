using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiSetting : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button closeButton;
    public Slider musicSlider;
    public Slider SFXSlider;
    void Start()
    {
        closeButton.onClick.AddListener(ResumeGame);
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(BackToMainMenu);
        AddEventSlider();

        LoadSettingSound();
        gameObject.SetActive(false);
    }


    void Update()
    {

    }
    private void AddEventSlider()
    {
        if (musicSlider != null)
        {

            musicSlider.onValueChanged.AddListener(AudioManager.Instance.MusicVolume);
        }
        if (SFXSlider != null)
        {

            SFXSlider.onValueChanged.AddListener(AudioManager.Instance.SFXVolume);
        }

    }



    private void ResumeGame()
    {

        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickBtnClip);
        SaveSystem.Save();
        gameObject.SetActive(false);
        Time.timeScale = 1f;

    }
    private void BackToMainMenu()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickBtnClip);
        SaveSystem.Save();
        SceneManager.LoadScene("MainMenu");
        // Application.Quit();
        Time.timeScale = 1f;

    }
    private void LoadSettingSound()
    {


        SettingSound settingSound = SaveSystem.LoadFileData().settingSound;
        // SettingSound settingSound = AudioManager.Instance.GetSettingVolume();
        musicSlider.value = settingSound.musicVolume;
        SFXSlider.value = settingSound.sfxVolume;
    }

}

[System.Serializable]
public class SettingSound
{
    public float musicVolume = 0f;
    public float sfxVolume = 0f;



}
