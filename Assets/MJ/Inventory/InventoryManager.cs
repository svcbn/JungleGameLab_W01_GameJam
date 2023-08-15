using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [MJ] 아이템 관리 매니저
/// </summary>
public class InventoryManager : MonoBehaviour
{
    #region Coin-Related Method
    private int _coin;
    public int Coin {
        get => _coin;
        private set
        {
            _coin = value;
            UIManager.instance.UpdateCoin(_coin);
        }
    }
    #endregion
    
    public void AddCoin(int count)
    {
        Coin += count;
    }

    #region Item Managing Method
    private List<
    
    /// <summary>
    /// 상점에서 호출 가능한 아이템 구매 메서드
    /// </summary>
    /// <param name="type">아이템 열거형</param>
    public void BuyItem(ItemType type)
    {
        if (Coin >= 1)
        {
            AddItem(type);
            Coin--;
        }
    }
    
    /// <summary>
    /// 맵에서 아이템 획득, 아이템 구매 후 수행되는 아이템 추가 메서드
    /// </summary>
    /// <param name="type"></param>
    public void AddItem(ItemType type)
    {
        UIManager.instance.AddItem(type);
    }
    
    /// <summary>
    /// 아이템 설치로 호출가능한 아이템 사용 메서드 
    /// </summary>
    public void InstallItem()
    {
        // [TODO] 아이템 설치 로직 필요
        
    }
    

    #endregion
}
