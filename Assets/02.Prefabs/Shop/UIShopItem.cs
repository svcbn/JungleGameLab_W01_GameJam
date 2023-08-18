using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// [MJ] 상점 아이템 프리팹에 적용될 스크립트 
/// </summary>
public class UIShopItem : MonoBehaviour
{
    private ShopManager _shopManager;
    public ItemInfo BaseInfo { get; private set; } 
    
    public void Init(ShopManager shopManager, ItemInfo info)
    {
        _shopManager = shopManager;
        
        BaseInfo = info;
        
        GetComponentInChildren<TextMeshProUGUI>().SetText(info.ShortName);
        GetComponentInChildren<Image>().sprite = info.ItemSprite;
        GetComponentInChildren<Button>().onClick.AddListener(Click);
    }

    public void B_OnMouseEnter()
    {
        _shopManager.ActiveItemInfo(this);
    }

    public void B_OnMouseExit()
    {
        _shopManager.DisableItemInfo();
    }

    private void Click()
    {
        var gm = GameManager.instance; 
        if (gm.State == GameManager.GameState.Shop)
        {
            if (gm.Inventory.Coin >= 1)
            {
                gm.Inventory.BuyItem(BaseInfo.ItemType);
            }
            else
            {
                UIManager.instance.ShowBuyFailText();
            }
        }
    }
}
