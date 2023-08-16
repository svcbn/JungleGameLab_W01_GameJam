using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isComplete = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isComplete)
            {
                // 통과가능
                Debug.Log("클리어");
            }
            else
            {
                // 키 모아오라고 메시지? 띄우는부분
                Debug.Log("키부족");
            }
        }

    }
}
