using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
/// [MJ] 상점 아이템 관리
/// </summary>
public class ShopManager : MonoBehaviour
{
    [Header("아이템 생성 정보")]
    public Transform playerItemListTr;
    public Transform trapItemListTr;
    public Transform utilItemListTr;
    public GameObject shopItemPrefab;

    [Header("아이템 정보 표시")] 
    public GameObject itemInfoObject;
    public Image itemSprite;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;


    public void Start()
    {
        itemInfoObject.SetActive(false);

        var rm = ResourceManager.Instance;

        foreach (var itemType in rm.ShopItemList)
        {
            var obj = rm.ItemPrefabDict[itemType];
            var info = obj.GetComponent<ItemInfo>();
            if (info != null)
            {
                Transform tr = null;
                switch (info.Category)
                {
                    case  ItemCategory.Player : 
                        tr = playerItemListTr;
                        break;
                    case  ItemCategory.EnemyTrap : 
                        tr = trapItemListTr;
                        break; 
                    case  ItemCategory.Util : 
                        tr = utilItemListTr;
                        break;
                    default: continue;
                }

                var instantIObj = Instantiate(shopItemPrefab, tr);
                var shopItem = instantIObj.GetComponent<UIShopItem>();
                shopItem.Init(this, info);
            }
        }
        
    }

    public void ActiveItemInfo(UIShopItem uiItem)
    {
        itemInfoObject.SetActive(true);
        itemNameText.SetText(uiItem.BaseInfo.ShortName);
        itemDescriptionText.SetText(uiItem.BaseInfo.Description);
        itemSprite.sprite = uiItem.BaseInfo.ItemSprite;
    }

    public void DisableItemInfo()
    {
        itemInfoObject.SetActive(false);
    }
    
}
