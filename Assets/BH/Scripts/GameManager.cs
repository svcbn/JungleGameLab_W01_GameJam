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

    public List<ItemBox> boxes = new List<ItemBox>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        vignette = GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();
        ChangeState(GameState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        #region 디버그용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(GameState.Title);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(GameState.Tutorial);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(GameState.Shop);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeState(GameState.Day);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeState(GameState.Night);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeState(GameState.Die);
        }
        #endregion

        if (State == GameState.Day)
        {
            RayCheck();
            if(Input.GetMouseButtonDown(0) && canSetItem)
            {
                PlaceItem();
            }
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
        Debug.Log(state + "");
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

    public GameObject testItem;
    List<GameObject> testList = new List<GameObject>();
    public GameObject testKey;

    public ItemType Type;
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
        boxes.Clear();
        maxKeys = difficulty * keys;
        ItemBox[] tempArr;
        tempArr = GameObject.Find("RandomBox").GetComponentsInChildren<ItemBox>();

        testList.Add(testItem);
        testList.Add(testKey);

        foreach (ItemBox ib in tempArr)
        {
            boxes.Add(ib);
        }

        foreach (ItemBox box in boxes)
        {
            //랜덤아이템로드
            int randomNum = UnityEngine.Random.Range(0, testList.Count);
            
            GameObject itemPrefab = testList[randomNum];

            Type = (ItemType)1;

            box.item.Add(itemPrefab);
        }
    }

    public bool canSetItem = false;

    void RayCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15f);

        if(hit)
        {
            Debug.Log(hit.collider.gameObject);
            
            if (hit.collider.CompareTag("ItemArea"))
            {
                canSetItem = false;
                GameObject go = hit.collider.transform.parent.GetChild(1).gameObject;
                go.SetActive(true);
                StartCoroutine(ActiveFalse(go));
            }
            if (hit.collider.CompareTag("Ground"))
            {
                canSetItem = true;
            }
        }
    }

    IEnumerator ActiveFalse(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void PlaceItem()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 이자리에 설치
        // 인벤토리랑 연결
        //Instantiate(testItem, position, Quaternion.identity);
        canSetItem = false;
    }
}
