
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using System.Security.Cryptography;

using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    public static SaveSystem Instance;
    public static bool IsSaveData = true;

    private static string nameSave = "dataGame";

    // private static string nameSave
    // {
    //     string filePath = Application.persistentDataPath + "/DataGame.json";
    //     return filePath;
    // }
    public static void Save()
    {
        SaveData dataSave = LoadFileData();
        if (IsSaveData)
        {
            dataSave.CurentLevel = Controller.Instance.levelManager.currentLevel.levelNumber;
        }
        dataSave.settingSound = AudioManager.Instance.GetSettingVolume();
        string saveDataJson = JsonUtility.ToJson(dataSave, true);
        PlayerPrefs.SetString(nameSave, saveDataJson);
        PlayerPrefs.Save();

    }

    public static SaveData LoadFileData()
    {
        SaveData saveData = new SaveData();

        if (PlayerPrefs.HasKey(nameSave))
        {
            string saveContent = PlayerPrefs.GetString(nameSave);
            saveData = JsonUtility.FromJson<SaveData>(saveContent);
        }

        return saveData;
    }

    public static void NewGameData()
    {

        // File.Delete(nameSave);
        SaveData data = LoadFileData();
        data.CurentLevel = 1;
        string saveDataJson = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(nameSave, saveDataJson);
        PlayerPrefs.Save();

    }

}

[System.Serializable]
public class SaveData
{
    public SettingSound settingSound;
    public int CurentLevel;

    public SaveData()
    {
        settingSound = new SettingSound();
        CurentLevel = 1;
    }
}