using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public List<ItemType> item = new List<ItemType>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (item[0] != ItemType.Ignore)
            {
                if (item[0] == ItemType.Key)
                {
                    GameManager.Instance.AddKey();
                }
                else
                {
                    //인벤토리에 집어넣기
                    GameManager.Instance.Inventory.AddItem(item[0]);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
