using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    public static int levelSelected;
    public Bolt botlClicked;
    public Nut nutClicked;
    // public List<Bolt> bolts = new List<Bolt>();
    public LevelManager levelManager;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // levelManager.LoadLevel();

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SetNutClicked(Nut nut)
    {
        nutClicked = nut;
    }
    public void SetBoltClicked(Bolt bolt)
    {
        botlClicked = bolt;
    }
    public void AddNutforBolt(Bolt bolt)
    {
        if (bolt != null)
        {
            if (bolt.AddNut(nutClicked))
            {
                if (botlClicked != null && botlClicked != bolt)
                {
                    // botlClicked = bolt;
                    botlClicked.RemoveNut(bolt);
                }
                botlClicked = null;
                nutClicked = null;

            }
            else
            {
                botlClicked.ReturnNutToOldPosition();
                bolt.GetTopNut();

            }
            // bolt.AddNut(nutClicked);
        }
    }


    public void CheckWinLevel()
    {

        if (levelManager.LevelComplated())
        {
            Debug.Log("You Win!");
            levelManager.LoadNextLevel();
        }
        else
        {
            Debug.Log("Not completed ");
        }


    }

    public static void SetLevelSelected(int level)
    {
        levelSelected = level;
        SaveSystem.IsSaveData = levelSelected <= 0 ? true : false;
    }



}
