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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Player"))
        // {
        //     GameManager.instance.AddKey();
        //     Destroy(gameObject);
        //     door.CollectKey();
        // }
    }
}
