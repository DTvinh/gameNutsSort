using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiSelectLevel : MonoBehaviour
{
    [SerializeField] private List<LevelSO> levelSOs;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform levelButtonContainer;
    [SerializeField] private Button closeButton;

    void Start()
    {
        CreateLevelButtons();
        closeButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateLevelButtons()
    {
        foreach (var levelSO in levelSOs)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, levelButtonContainer);
            levelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + levelSO.levelNumber;
            Button button = levelButton.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Controller.SetLevelSelected(levelSO.levelNumber);
                SceneManager.LoadScene("GamePlay");

            });


        }
    }
}
