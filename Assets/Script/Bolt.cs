using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bolt : MonoBehaviour
{
    private int maxNuts = 5;
    public Stack<Nut> nutsInColumn = new Stack<Nut>();
    [SerializeField] Transform nutClickPosition;
    public Nut nutClicked;
    public bool canClick = true;


    void Start()
    {

    }

    public void CreateNutsInBolt(List<NutSetting> nutsSettings)
    {
        for (int i = 0; i < nutsSettings.Count; i++)
        {
            GameObject nutObject = Instantiate(GameAsset.Instance.GetNutPrefab(nutsSettings[i].nutType), transform);
            nutObject.transform.localPosition = new Vector3(0, i * 0.5f, 0);
            Nut nut = nutObject.GetComponent<Nut>();
            nutsInColumn.Push(nut);
            if (nutsSettings[i].IsHidden)
            {
                nut.SetHidden(true);
            }


        }

    }

    void Update()
    {

    }
    void OnMouseDown()
    {
        if (!canClick)
        {
            return;
        }
        if (nutClicked != null)
        {
            ReturnNutToOldPosition();
            return;
        }
        if (nutClicked == null && Controller.Instance.nutClicked != null)
        {
            // AddNut(Controller.Instance.nutClicked);
            // Controller.Instance.SetBoltClicked(this);
            Controller.Instance.AddNutforBolt(this);
            return;
        }
        if (nutClicked == null && Controller.Instance.nutClicked == null)
        {
            GetTopNut();
            return;

        }

    }
    public void ReturnNutToOldPosition()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.clickNutClip);
        nutClicked.SetMovePosition(new Vector3(transform.position.x, (nutsInColumn.Count - 1) * 0.5f, transform.position.z));
        Controller.Instance.SetNutClicked(null);
        Controller.Instance.SetBoltClicked(null);
        nutClicked = null;

    }

    public void GetTopNut()
    {

        if (nutsInColumn.Count > 0 && !IsCompleted())
        {

            AudioManager.Instance.PlaySFX(AudioManager.Instance.clickNutClip);
            Nut topNut = nutsInColumn.Peek();
            // oldNutPosition = topNut.transform.position;
            Controller.Instance.SetNutClicked(topNut);
            Controller.Instance.SetBoltClicked(this);
            topNut.SetMovePosition(nutClickPosition.position);
            nutClicked = topNut;


        }
        else
        {

            Debug.Log("Bolt này không có nut nào Hoặc đã hoàn thành .");
        }
    }


    public bool AddNut(Nut newNut)
    {

        if (nutsInColumn.Count == 0 || (nutsInColumn.Count < maxNuts && nutsInColumn.Peek().nutType == newNut.nutType))
        {
            newNut.transform.SetParent(transform);
            newNut.SetMovePosition(GetPositionNut());
            // Controller.Instance.SetNutClicked(null);
            nutsInColumn.Push(newNut);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.sortNutClip);
            if (IsCompleted())
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.complateBoltClip);
                GameObject effect = Instantiate(GameAsset.Instance.boltEffect, transform.position, Quaternion.identity);
                effect.transform.SetParent(transform);
                Controller.Instance.CheckWinLevel();

            }

            return true;

        }
        else
        {

            return false;
        }

    }

    public void RemoveNut(Bolt bolt)
    {
        StartCoroutine(RemoveNutCoroutine(bolt));
    }

    private IEnumerator RemoveNutCoroutine(Bolt bolt)
    {
        if (nutsInColumn.Count == 0) yield break;

        Nut nutRemove = nutsInColumn.Pop();
        canClick = false;
        while (nutsInColumn.Count > 0)
        {
            Nut nutf = nutsInColumn.Peek();
            yield return new WaitForSeconds(0.2f);
            if (nutRemove.GetNutType() == nutf.GetNutType())
            {
                if (bolt.AddNut(nutf))
                {
                    nutRemove = nutsInColumn.Pop();
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        if (nutsInColumn.Count > 0 && nutsInColumn.Peek().isHidden)
        {
            nutsInColumn.Peek().SetHidden(false);
        }
        canClick = true;
        nutClicked = null;
    }

    Vector3 GetPositionNut()
    {

        int numberOfNuts = nutsInColumn.Count;
        return new Vector3(transform.position.x, numberOfNuts * 0.5f, transform.position.z);

    }

    public bool IsCompleted()
    {
        if (nutsInColumn.Count != maxNuts) return false;

        Nut firstNut = null;
        foreach (Nut nut in nutsInColumn)
        {
            if (firstNut == null)
                firstNut = nut;
            else if (nut.GetNutType() != firstNut.GetNutType())
                return false;
        }
        return true;
    }
}
