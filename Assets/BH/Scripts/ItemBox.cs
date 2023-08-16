using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public List<GameObject> item = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (item[0] != null)
            {
                Instantiate(item[0], transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}
