using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject[] groundPiece;

    public float valueX ;
    public float valueY ;

    private void Start()
    {
        foreach (var obj in groundPiece)
        {
            obj.GetComponentInChildren<Ground>().GroundManager = this;
        }
    }
}
