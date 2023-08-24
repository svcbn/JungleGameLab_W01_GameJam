using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// [MJ] 아이템 관리 매니저
/// </summary>
public class InventoryManager
{
    public InventoryManager()
    {
        Coin = 5;
        Items = new List<ItemType>();
    }
    
    #region Coin-Related Method
    private int _coin;
    public int Coin {
        get => _coin;
        set
        {
            _coin = value;
            UIManager.instance?.UpdateCoin(_coin);
        }
    }
    #endregion
    
    public void AddCoin(int count)
    {
        Coin += count;
    }

    #region Item Managing Method
    public List<ItemType> Items { get; private set; }
    
    /// <summary>
    /// 상점에서 호출 가능한 아이템 구매 메서드
    /// </summary>
    /// <param name="type">아이템 열거형</param>
    public void BuyItem(ItemType type)
    {
        AddItem(type);
        Coin--;
    }
    
    /// <summary>
    /// 맵에서 아이템 획득, 아이템 구매 후 수행되는 아이템 추가 메서드
    /// </summary>
    /// <param name="type"></param>
    public void AddItem(ItemType type)
    {
        Items.Add(type);
        UIManager.instance?.AddItemOnView(type);
        if (Items.Count > 0)
        {
            UIManager.instance.nextItemGrid.SetActive(true);
        }
        if(GameManager.Instance.State == GameManager.GameState.Night)
        {
            UIManager.instance.ShowItemInfoText(ResourceManager.Instance.ItemPrefabDict[type].GetComponent<ItemInfo>().ShortName);
            
        }
    }

    /// <summary>
    /// UI 및 리스트에서 각 첫번째 아이템 제거
    /// </summary>
    private void RemoveItem()
    {
        UIManager.instance?.RemoveItemOnView();
        Items.RemoveAt(0);
        if (Items.Count < 1)
        {
            UIManager.instance.nextItemGrid.SetActive(false);
        }
    }
    
    /// <summary>
    /// 아이템 설치 행동 시, 해당 메서드를 호출하여 아이템 정보 얻기 
    /// </summary>
    public ItemType GetItemBeforeInstall()
    {   
        var item = Items.FirstOrDefault();

        if (item != ItemType.Ignore)
        {
            RemoveItem();
        }
        return item;
    }

    /// <summary>
    /// 아이템 초기화
    /// </summary>
    public void ResetItems()
    {
        Items.Clear();
        UIManager.instance.ResetItem();
    }

    #endregion
}
