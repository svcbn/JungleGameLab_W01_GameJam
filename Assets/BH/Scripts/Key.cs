using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GateCheck door;

    void Start()
    {
        door = GameObject.Find("Gate").GetComponent<GateCheck>();
    }
}
