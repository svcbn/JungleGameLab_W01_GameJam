using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// [MJ] UI Manager
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    
    
    [Header("Top Area")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI coinText;

    [Header("Top Under Area")] 
    public Transform itemCreatePosTr;

    public GameObject itemPrefab;
    
    [Header("Bottom Area")]
    public TextMeshProUGUI goalText;

    [Header("Shop")]
    public GameObject shopObject; 
    public GameObject noMoneyObject; 
        
    #region Other Text Update Method

    public void UpdatePlayerHp(int hpValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < hpValue; i++)
            newText += "♥";
        hpText.SetText(newText);
    }

    public void UpdateStageText(int stageIdx)
    {
        stageText.SetText($"STAGE {stageIdx}");
    }

    public void UpdateCoin(int cnt)
    {
        coinText.SetText($"COIN {cnt}");
    }

    public void UpdateGoalText(int curCnt, int goalCnt)
    {
        goalText.SetText($"GOAL {curCnt} / {goalCnt}"); 
    }
    #endregion
    
    
    #region Item-Related Method
    public void AddItemOnView(ItemType type)
    {
        // 생성 후 데이타 설정
        var obj = Instantiate(itemPrefab, itemCreatePosTr);
        obj.GetComponent<ShopItem>().Init(type);
    }

    /// <summary>
    /// 해당 오브젝트는 혹시라도 아이템이 비어있는 경우에 호출되지 않음. 호출부에서 빈 값일 때 수행안하도록 처리됨
    /// </summary>
    public void RemoveItemOnView()
    {
        var obj = itemCreatePosTr.GetChild(0).gameObject;
        Destroy(obj);
    }
    #endregion

    #region Shop-Related Method
    public void ShowShop()
    {
         shopObject.SetActive(true);   
    }

    public void CompleteShopping()
    {
        
    }


    #region 구매 실패 관련 변수 및 함수
    private int noMoneyTextShowTime = 0;
    
    /// <summary>
    /// 돈이 부족해서 구매 못했다는 텍스트 출력하는 메서드
    /// </summary>
    public void ShowBuyFailText()
    {
        if (noMoneyTextShowTime == 0)
        {
            noMoneyObject.SetActive(true);
            StartCoroutine(nameof(ShowNoMoneyText));
        }
        
        noMoneyTextShowTime = 1;
    }

    IEnumerator ShowNoMoneyText()
    {
        do
        {
            yield return new WaitForSeconds(1.0f);
            noMoneyTextShowTime--;
        } while (noMoneyTextShowTime > 0);
        
        noMoneyObject.SetActive(false);
    }
    
    #endregion
    #endregion
}
