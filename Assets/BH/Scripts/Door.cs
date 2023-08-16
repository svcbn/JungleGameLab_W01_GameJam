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
                GameManager.instance.Clear();
                Debug.Log("Ŭ����");
            }
            else
            {
                // Ű ��ƿ���� �޽���? ���ºκ�
                Debug.Log("Ű����");
            }
        }

    }
}
