using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// ByeongHan
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UIManager _uiManager;
    public InventoryManager Inventory { get; private set; }

    [SerializeField] int difficulty = 1;
    [SerializeField] int keys = 10;
    public int maxKeys;
    public int currentStage = 0;

    public List<GameObject> boxes = new List<GameObject>();

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

    public enum GameState
    {
        Title,
        Tutorial,
        Shop,
        Day,
        Night,
        Die
    }

    public GameState State { get; private set; }
    [SerializeField] float dayTime = 30f;

    private int _stage;
    public int Stage
    {
        get => _stage;
        private set
        {
            _stage = value;
            _uiManager.UpdateStageText(_stage);
        }
    }

    public int GoalCnt { get; private set; }
    private int _collectedGoalCnt;

    public int CollectedGoalCnt
    {
        get => _collectedGoalCnt;
        set
        {
            _collectedGoalCnt = value;
            _uiManager.UpdateGoalText(_collectedGoalCnt, GoalCnt);
        }
    }

    public float _timer;

    public float Timer
    {
        get => _timer;
        set
        {
            _timer = value;
            _uiManager.UpdateTimerText(_timer);
        }
    }
    private int _backupCoin;
    
    float timeLeft;

    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = UIManager.instance;
        ResourceManager.Init();
        Inventory = new InventoryManager();
        // vignette = GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();

        ChangeState(GameState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(GameState.Title);
            Debug.Log("Title");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(GameState.Tutorial);
            Debug.Log("Tutorial");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(GameState.Shop);
            Debug.Log("Shop");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeState(GameState.Day);
            Debug.Log("Day");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeState(GameState.Night);
            Debug.Log("Night");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeState(GameState.Die);
            Debug.Log("Die");
        }
    }

    #region 게임 상태 관련
    void ChangeState(GameState state)
    {
        State = state;
        switch (state)
        {
            case GameState.Title:
                Title();
                break;
            case GameState.Tutorial:
                Tutorial();
                break;
            case GameState.Shop:
                ShopPhase();
                break;
            case GameState.Day:
                DayPhase();
                break;
            case GameState.Night:
                NightPhase();
                break;
            case GameState.Die:
                Die();
                break;
        }
    }

    void Title()
    {
        _uiManager.SetTitleView();
    }

    public void B_Tutorial()
    {
        Stage = 0;
        ChangeState(GameState.Tutorial);
    }
    public void B_GameStart()
    {
        Stage = 1;
        ChangeState(GameState.Shop);
    }
    void Tutorial()
    {
        CameraSetting(false, null, 25f);
    }

    void ShopPhase()
    {
        Inventory.ResetItems();
        _uiManager.SetGameViewShop();
    }

    public void B_ShopConfirm() => ChangeState(GameState.Day);
    void DayPhase()
    {
        CollectedGoalCnt = 0;
        GoalCnt = Stage;
        _uiManager.SetGameViewDay(GoalCnt);
        
        CameraSetting(false, null, 40f);

        StartCoroutine(DayTimer());
    }

    IEnumerator DayTimer()
    {
        timeLeft = dayTime;

        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Timer = timeLeft;
            yield return null;
        }

        ChangeState(GameState.Night);
    }

    void NightPhase()
    {
        Inventory.ResetItems();
        RandomBoxSetting(difficulty, keys);
        _uiManager.SetGameViewNight();
        
        CameraSetting(true, GameObject.FindWithTag("Player").gameObject.transform, 10f, true);

        // [TODO] 캐릭터 랜덤 위치에 지정 필요!
    }
    
    void CameraSetting(bool isVignette, Transform transform, float size, bool isAttached = false)
    {
        //vignette.active = isVignette;
        Camera.main.transform.SetParent(transform, false);
        if (!isAttached)
        {
            Camera.main.transform.position = new Vector3(0, 0, -10); // 현재 스테이지 카메라 위치로 변경할것
        }
        
        Camera.main.orthographicSize = size;
    }

    void Die()
    {
        _uiManager.SetGameViewDie();
    }

    public void B_Retry()
    {
        
    }

    public void B_Main()
    {
        // Reload Scene
    }

    /// <summary>
    /// 목표 대상을 수집 후, 탈출구로 이동할 때 수행되는 클리어
    /// </summary>
    public void Clear() => StartCoroutine(nameof(ClearNextEventBeforeDelay));

    private bool nextStageWaitFlag;
    IEnumerator ClearNextEventBeforeDelay()
    {
        // 보상 처리 및 초기화
        nextStageWaitFlag = true;
        _uiManager.SetClearView();
        Inventory.AddCoin(5);
        // [TODO] 플레이어 SetActive(false) 필요 할 듯
         
        yield return new WaitForSeconds(2f); // 대기 (여운) 
        
        // 상점 씬으로 이동
        Stage++;
        nextStageWaitFlag = false;
        ChangeState(GameState.Shop);
    }
    #endregion


    void RandomBoxSetting(int difficulty, int keys)
    {
        maxKeys = difficulty * keys;
       
        foreach (var box in boxes)
        {
            //GameObject itemPrefab = 

            //box.GetComponent<ItemBox>().item.Append();
        }
    }
}
