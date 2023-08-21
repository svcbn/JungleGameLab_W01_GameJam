using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialBox : MonoBehaviour
{
    public ItemType itemType;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
            if (itemType == ItemType.Key)
            {
                GameManager.Instance.AddKey();
            }
            else
            {
                GameManager.Instance.Inventory.AddItem(itemType);
            }
        }
    }
}
