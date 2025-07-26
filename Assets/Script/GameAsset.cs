using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    public GameObject nutBlue;
    public GameObject nutRed;
    public GameObject nutGreen;
    public GameObject nutYellow;
    public GameObject nutPurple;
    public GameObject nutOrange;
    public GameObject nutCyan;

    public GameObject bolt;

    public GameObject boltEffect;
    public static GameAsset Instance;

    public Material materialHighlighted;

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


    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetNutPrefab(NutType nutType)
    {
        switch (nutType)
        {
            case NutType.Blue:
                return nutBlue;
            case NutType.Red:
                return nutRed;
            case NutType.Green:
                return nutGreen;
            case NutType.Yellow:
                return nutYellow;
            case NutType.Purple:
                return nutPurple;
            case NutType.Orange:
                return nutOrange;
            case NutType.Cyan:
                return nutCyan;
            default:
                return null;
        }
    }
}
