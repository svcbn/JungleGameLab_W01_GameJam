using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

/// <summary>
/// ByeongHan
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UIManager { get; set; }
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
            UIManager?.UpdateStageText(_stage);
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
            UIManager?.UpdateGoalText(_collectedGoalCnt, GoalCnt);
        }
    }

    public float _timer;

    public float Timer
    {
        get => _timer;
        set
        {
            _timer = value;
            UIManager?.UpdateTimerText(_timer);
        }
    }
    private int _backupCoin;
    
    float timeLeft;

    Vignette vignette;

    public int DefaultPlayerHp = 5;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = UIManager.instance;
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

        if (State == GameState.Day || State == GameState.Night)
        {
            RayCheck();
            if (Input.GetMouseButtonDown(0) && canSetItem)
            {
                PlaceItem();
            }
        }

    }

    #region 게임 상태 관련
    void ChangeState(GameState state)
    {
        State = state;
        Debug.Log(state + "");
        switch (state)
        {
            case GameState.Title:
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

    public void B_GameStart()
    {
        Stage = 1;
        SceneManager.LoadScene("01.Scenes/Day1");
        ChangeState(GameState.Tutorial);
    }
    void Tutorial()
    {
        CameraSetting(false, null, 25f);
    }
    
    /// <summary>
    /// 튜토리얼 종료 후 호출
    /// </summary>
    public void CompleteTutorial()
    {
        // 씬 이동
        SceneManager.LoadScene("01.Scenes/DayN");
        
        // 데이터 설정
        Stage = 2;
        Inventory.AddCoin(5);
        _backupCoin = Inventory.Coin;
    }

    public void CompleteLoadShop() => ChangeState(GameState.Shop);
    void ShopPhase()
    {
        Inventory.ResetItems();
        UIManager.SetGameViewShop();
        
        // size9 orthographic
        Camera.main.transform.position = new Vector3(1000, 1000, -10);
        Camera.main.orthographicSize = 9;
    }

    public void B_ShopConfirm() => ChangeState(GameState.Day);
    void DayPhase()
    {
        CollectedGoalCnt = 0;
        GoalCnt = Stage;
        UIManager?.SetGameViewDay(GoalCnt);
        
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
        UIManager?.SetGameViewNight();
        
        CameraSetting(true, GameObject.FindWithTag("Player").gameObject.transform, 10f, true);

        // [TODO] 캐릭터 랜덤 위치에 지정 필요!
    }

    void CameraSetting(bool isVignette, Transform transform, float size, bool isAttached = false)
    {
        vignette.active = isVignette;
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
        UIManager?.SetGameViewDie();
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
        UIManager?.SetClearView();
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
            Debug.Log(hit.collider.tag);
            
            if (hit.collider.CompareTag("ItemArea"))
            {
                canSetItem = false;
                GameObject go = hit.collider.transform.parent.GetChild(1).gameObject;
                if(go.gameObject.activeSelf == false)
                {
                    go.SetActive(true);
                    StartCoroutine(ActiveFalse(go));
                }
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
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15f);

        Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        GameObject currentItem = Inventory.GetItemBeforeInstall();
        if (currentItem != null)
        {
            GameObject item = Instantiate(currentItem);
            item.transform.SetParent(hit.transform, true);
            item.transform.position = position;
            canSetItem = false;
        }
    }
}
