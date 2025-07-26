using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelSO> levels;
    public LevelSO currentLevel;
    public float horizontalSpacing = 2.5f;
    public float verticalSpacing = 4.5f;

    public List<Bolt> bolts = new List<Bolt>();
    void Awake()
    {
    }

    void Start()
    {
        // CreateLevel();
        int levelNumber;
        if (Controller.levelSelected > 0)
        {
            levelNumber = Controller.levelSelected;
        }
        else
        {
            SaveData saveData = SaveSystem.LoadFileData();
            levelNumber = saveData.CurentLevel;
        }
        // SaveData saveData = SaveSystem.LoadFileData();
        LoadLevel(levelNumber);

        // ArrangeObjects();
    }

    public void CreateLevel()
    {
        // Xóa các object cũ
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        bolts.Clear();

        // Tạo các object mới
        if (currentLevel == null)
        {
            return;
        }
        for (int i = 0; i < currentLevel.boltsSettings.Count; i++)
        {

            int row = i / 3;
            int col = i % 3;

            // Tính toán tọa độ
            float x = (col - 1) * horizontalSpacing;
            float z = -row * verticalSpacing;

            Vector3 position = new Vector3(x, 0, z);
            GameObject newBotl = Instantiate(GameAsset.Instance.bolt, position, Quaternion.identity, transform);
            Bolt bolt = newBotl.GetComponent<Bolt>();
            bolt.CreateNutsInBolt(currentLevel.boltsSettings[i].nutsSettings);
            bolts.Add(bolt);
        }
        UiManager.Instance.UpdateLevelText(currentLevel.levelNumber);
        // Controller.Instance.bolts = bolts;
    }

    public bool LevelComplated()
    {
        int completedCount = 0;
        foreach (Bolt bolt in bolts)
        {
            if (bolt.IsCompleted())
            {
                completedCount++;
                continue;
            }
        }
        if (completedCount >= currentLevel.numberBoltCompleted)
        {

            return true;
        }
        else
        {

            return false;
        }
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevel.levelNumber + 1;
        foreach (LevelSO level in levels)
        {
            if (level.levelNumber == nextLevelIndex)
            {

                currentLevel = level;
                SaveSystem.Save();
                CreateLevel();
                return;
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {


        foreach (LevelSO level in levels)
        {
            if (level.levelNumber == levelNumber)
            {
                currentLevel = level;
                CreateLevel();
                return;
            }
            // AudioManager.Instance.LoadSettingVolume(saveData.settingSound);
        }
        currentLevel = levels[0];
        CreateLevel();
    }


    void OnDestroy()
    {
        // Xóa các object khi không còn cần thiết
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        bolts.Clear();
    }
}





