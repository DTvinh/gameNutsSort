using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button selectLevelButton;
    [SerializeField] Button quitGameButton;
    [SerializeField] UiSelectLevel uiSelectLevel;
    void Start()
    {
        newGameButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        selectLevelButton.onClick.AddListener(OpenSelectLevel);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NewGame()
    {
        SaveSystem.NewGameData();
        Controller.SetLevelSelected(0);
        SceneManager.LoadScene("GamePlay");
    }
    void ContinueGame()
    {
        // SaveSystem.LoadFile();
        Controller.SetLevelSelected(0);
        SceneManager.LoadScene("GamePlay");
    }

    void OpenSelectLevel()
    {
        uiSelectLevel.gameObject.SetActive(true);
    }
    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Dừng chế độ Play trong Editor
#else
    Application.Quit(); // Thoát game khi đã build
#endif
    }

}
