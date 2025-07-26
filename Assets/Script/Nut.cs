using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour
{
    public NutType nutType;

    private Vector3 movePosition;
    private bool isMoving = false;
    public bool isHidden = false;
    private Material material;
    private MeshRenderer renderer;
    void Awake()
    {


        renderer = GetComponent<MeshRenderer>();
        material = renderer.material;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime * 10f);
            if (Vector3.Distance(transform.position, movePosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
    public void SetMovePosition(Vector3 position)
    {
        // Debug.Log(movePosition);
        movePosition = position;
        isMoving = true;
    }
    public void SetHidden(bool hidden)
    {
        isHidden = hidden;
        if (hidden)
        {
            renderer.material = GameAsset.Instance.materialHighlighted;
            // gameObject.SetActive(false);
        }
        else
        {
            renderer.material = material;
            // gameObject.SetActive(true);
        }
    }

    public NutType GetNutType()
    {
        if (!isHidden)
        {
            return nutType;
        }
        return NutType.Black;
    }
}



public enum NutType
{
    Blue,
    Yellow,
    Red,
    Purple,
    Black,
    Green,
    Orange,
    Cyan
}

