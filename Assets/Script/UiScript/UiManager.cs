using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Button restartButton;
    [SerializeField] Button pauseButton;
    [SerializeField] UiSetting uiSetting;

    public static UiManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        pauseButton.onClick.AddListener(PauseGame);
        restartButton.onClick.AddListener(RestartGame);
    }


    public void UpdateLevelText(int level)
    {
        levelText.text = "Level: " + level.ToString();
    }


    public void PauseGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickBtnClip);
        uiSetting.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickBtnClip);
        Controller.Instance.levelManager.CreateLevel();
        uiSetting.gameObject.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
