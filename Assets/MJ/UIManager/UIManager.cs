using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// [MJ] UI Manager
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _gameManager.UIManager = this;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        if(_gameManager.State == GameManager.GameState.Tutorial)
        {
            SetViewObject();
            _gameManager.CompleteLoadTutorialUI();
        }
        else
        {
            _gameManager.CompleteLoadShop();
            UpdateAllText();
            shopConfirmButton.onClick.AddListener(_gameManager.B_ShopConfirm);
            shopConfirmButton.onClick.AddListener(CompleteShopping);
        }
        
    }
    #endregion

    private GameManager _gameManager;
    
    [Header("View by Game State")]
    public GameObject gameObj;
    public GameObject shopObj;  
    public GameObject clearObj;
    public GameObject gameOverObj;
    public GameObject endingObj;
    
    [Header("Default Game Info Area")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI coinText;
    
    [Header("Item Info")]
    public Transform itemCreatePosTr;
    public GameObject itemPrefab;
    public GameObject nextItemGrid;
    
    [Header("Day or Night Text")]
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI timerText;

    [Header("Shop")]
    public GameObject noMoneyObject;

    public Button shopConfirmButton;
    public Button readyButton;

    [Header("Clear")]
    public TextMeshProUGUI stageClearText;


    public GameObject itemInfoText;

    #region Method by GameState
    public void SetGameViewShop()
    {
        SetViewObject(game:true, shop:true);
        goalText.SetText(string.Empty);
    }
    
    public void SetGameViewDay(int goalCnt)
    {
        SetViewObject(game:true, timerTextObj:true);
        goalText.SetText($"The number of keys you need to collect is {goalCnt}."); 
    }

    public void SetGameViewNight()
    {
        SetViewObject(game:true);
        UpdateGoalText(_gameManager.CollectedGoalCnt, _gameManager.GoalCnt);
    }

    public void SetClearView()
    {
        stageClearText.SetText($"I endure for {_gameManager.Stage} days!");
    }

    public void SetFinalClearView()
    {
        SetViewObject(clear:true);
    }

    public void SetGameViewDie()
    {
        SetViewObject(gameOver:true);
    }

    /// <summary>
    /// 설정되지 않은 항목들은 다 비활성화 시킴
    /// </summary>
    private void SetViewObject(bool game = false, bool shop= false, bool gameOver = false, bool timerTextObj = false, bool clear = false)
    {
        gameObj.SetActive(game);
        shopObj.SetActive(shop);
        gameOverObj.SetActive(gameOver);
        clearObj.SetActive(clear);
        timerText.gameObject.SetActive(timerTextObj);   
    }
    #endregion 
    
    #region Other Text Update Method

    private void UpdateAllText()
    {
        UpdatePlayerHp(_gameManager.DefaultPlayerHp);
        UpdateStageText(_gameManager.Stage);
        UpdateCoin(_gameManager.Inventory.Coin);
    }

    public void UpdatePlayerHp(int hpValue)
    {
        string newText = string.Empty;
        for (int i = 0; i < hpValue; i++)
            newText += "♥";
        hpText.SetText(newText);
    }

    public void UpdateStageText(int stageIdx)
    {
        stageText.SetText($"DAY {stageIdx}");
    }

    public void UpdateCoin(int cnt)
    {
        coinText.SetText($"COIN {cnt}");
    }

    public void UpdateGoalText(int curCnt, int goalCnt)
    {
        goalText.SetText($"GOAL {curCnt} / {goalCnt}"); 
    }

    public void UpdateTimerText(float time)
    {
        timerText.SetText($"{(int)time}");
    }
    #endregion
    
    
    #region Item-Related Method
    public void AddItemOnView(ItemType type)
    {
        // 생성 후 데이타 설정
        var obj = Instantiate(itemPrefab, itemCreatePosTr);
        obj.GetComponent<UIItem>().Init(type);
    }

    /// <summary>
    /// 해당 오브젝트는 혹시라도 아이템이 비어있는 경우에 호출되지 않 음. 호출부에서 빈 값일 때 수행안하도록 처리됨
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
         shopObj.SetActive(true);   
    }

    public void CompleteShopping()
    {
        readyButton.gameObject.SetActive(true);
    }

    public void B_Ready()
    {
        _gameManager.timeLeft = 0.3f;
        readyButton.gameObject.SetActive(false);
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

    #region Tutorial

    public void Tutorial_Game()
    {
        SetViewObject(game: true);
    }
    public void Tutorial_Shop()
    {
        SetViewObject(shop: true, game: true);
    }

    public void Tutorial_Reset()
    {
        _gameManager.UIManager = null;
        instance = null;
    }

    #endregion

    public void B_Quit()
    {
        Application.Quit();
    }

    public void B_Retry()
    {
        _gameManager.B_Retry();
    }

    public void ResetItem()
    {
        for (int i = itemCreatePosTr.childCount - 1; i >= 0; i--)
        {
            var tr = itemCreatePosTr.GetChild(i);
            Destroy(tr.gameObject);
        }
    }

    public void ShowItemInfoText(string desc)
    {
        GameObject go = GameObject.Instantiate(itemInfoText, this.transform.parent);
        go.GetComponent<TextMeshProUGUI>().SetText(desc + " 획득");
    }
}
