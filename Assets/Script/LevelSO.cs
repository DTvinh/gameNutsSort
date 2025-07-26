using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int levelNumber;
    public int numberBoltCompleted;
    public List<BoltSetting> boltsSettings;



}
[System.Serializable]
public class BoltSetting
{
    public List<NutSetting> nutsSettings;
}
[System.Serializable]
public class NutSetting
{
    public NutType nutType;
    public bool IsHidden;

}
