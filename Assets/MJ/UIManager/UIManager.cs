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
    
    [Header("Bottom Area")]
    public TextMeshProUGUI goalText;

    [Header("Shop")] public GameObject shopObject; 
        
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
    public void AddItem(ItemType item)
    {
        GameObject obj = null; // [TODO] Resource에서 얻어와서 생성하는 로직
        Instantiate(obj, itemCreatePosTr);
    }

    public void RemoveItem()
    {
        var obj = itemCreatePosTr.GetChild(0).gameObject;
        if (obj != null)
        {
            Destroy(obj);
        }
        
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
    #endregion
}
