using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeftUpOfGround : MonoBehaviour
{
    private Ground _ground;

    void Start()
    {
        _ground = transform.parent.GetComponent<Ground>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player")) _ground.MovePlayerOnLeftUp();
    }
}
