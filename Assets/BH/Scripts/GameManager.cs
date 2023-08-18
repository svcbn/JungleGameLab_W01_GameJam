using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    public GameObject keyPrefab;

    public GameObject PlayerObj;
    public GateCheck GateCheck { get; set; }

    #region Tutorial Temp Data

    private int _tutorialHp;
    public int TutorialHp { get => _tutorialHp;
        set
        {
            if (value > 0)
            {
                _tutorialHp = value;
                UIManager.instance.UpdatePlayerHp(_tutorialHp);
            }
        }}
    
    
    #endregion
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
    [SerializeField] float dayTime = 15f;

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

    public PostProcessVolume postProcess;
    Vignette vignette;

    public int DefaultPlayerHp = 5;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = UIManager.instance;
        ResourceManager.Init();
        Inventory = new InventoryManager();

        vignette = postProcess.profile.GetSetting<Vignette>();
        ChangeState(GameState.Title);
    }

    // Update is called once per frame
    void Update()
    {
        #region 디버그용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("DayN");
            ChangeState(GameState.Shop);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            timeLeft = 1f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(GameState.Die);
        }

        #endregion

        if (State == GameState.Day || State == GameState.Night || State == GameState.Tutorial)
        {
            RayCheck();
            if (Input.GetMouseButtonDown(0) && canSetItem)
            {
                PlaceItem();
            }
        }
    }

    #region 게임 상태 관련
    public void ChangeState(GameState state)
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
        ChangeState(GameState.Tutorial);
        SceneManager.LoadScene("01.Scenes/Day1");
    }

    public void CompleteLoadTutorialUI()
    {
        TutorialHp = 5;
        GoalCnt = 1;
        CollectedGoalCnt = 0;
        Stage = 1;
        Inventory.Coin = 5;
        //CameraSetting(false, null, 25f);
    }
    
    void Tutorial()
    {
    }
    
    /// <summary>
    /// 튜토리얼 종료 후 호출
    /// </summary>
    public void CompleteTutorial()
    {
        // 씬 이동
        State = GameState.Shop;
        CollectedGoalCnt = 0;
        Inventory.Coin = 5;
        SceneManager.LoadScene("01.Scenes/DayN");

        // 데이터 설정
        Stage = 2;
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
        postProcess.profile.GetSetting<LensDistortion>().active = false;
        postProcess.profile.GetSetting<Vignette>().active = false;

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
        RandomBoxSetting(6); // RandomBoxSetting(GoalCnt);
        UIManager?.SetGameViewNight();

       
        // Player 랜덤 위치 지정 
        var randomPosArr = GameObject.FindGameObjectsWithTag("SpawnPosition");
        var index = Random.Range(0, randomPosArr.Length);
        PlayerObj.SetActive(true);
        PlayerObj.transform.position = randomPosArr[index].transform.position;

        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().SpawnStart();
        
        // 카메라 Player 추적하도록 설정
        CameraSetting(true, PlayerObj.transform, 10f, true);
        postProcess.profile.GetSetting<LensDistortion>().active = true;
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

    void Die()
    {
        UIManager?.SetGameViewDie();
    }

    public void B_Retry()
    {
        Inventory.Coin = _backupCoin;
        ChangeState(GameState.Shop);
        SceneManager.LoadScene("DayN");
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
        UIManager?.SetFinalClearView();
        Inventory.AddCoin(5);
        // [TODO] 플레이어 SetActive(false) 필요 할 듯
        UIManager.instance.SetClearView();
        yield return new WaitForSeconds(2f); // 대기 (여운)


        if (Stage <= 3 )
        {
            Stage++;
            
            // 상점 씬으로 이동
            // nextStageWaitFlag = false;
            ChangeState(GameState.Shop);
            SceneManager.LoadScene("DayN");
        }
        else
        {
            UIManager.instance.endingObj.SetActive(true);
            Time.timeScale = 0;
        }

    }
    #endregion


    void RandomBoxSetting(int keys)
    {

        var tempBoxs = GameObject.FindGameObjectsWithTag("Box").Select(x=> x.GetComponent<ItemBox>()).ToList();

        for (int i = 0; i < tempBoxs.Count; i++)
        {
            if (i < keys)
            {
                tempBoxs[i].item.Add(ItemType.Key);
            }
            else
            {
                //랜덤아이템로드
                int randomNum = UnityEngine.Random.Range(1, Enum.GetNames(typeof(ItemType)).Length);
                tempBoxs[i].item.Add((ItemType)randomNum);
            }
        }
            
        // 셔플하기
        boxes.Clear();
            
        while (tempBoxs.Count > 0)
        {
            var index = Random.Range(0, tempBoxs.Count);
            boxes.Add(tempBoxs[index]);
            tempBoxs.RemoveAt(index);
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
        if (gameObject != null)
        {
            gameObject.SetActive(false);

        }
    }

    void PlaceItem()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15f);

        Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        ItemType type = Inventory.GetItemBeforeInstall();

        if (type == ItemType.Ignore) return;


        GameObject item = Instantiate(ResourceManager.ItemPrefabDict[type]);

        if (type == ItemType.EnemyStun || type == ItemType.EnemySlow || type == ItemType.EnemyMoveReserve)
        {
            item.transform.SetParent(hit.transform, true);
            item.transform.position = position;
            canSetItem = false;
        }
        else
        {
            if(State == GameState.Day)
            {
                item.transform.SetParent(hit.transform, true);
                item.transform.position = position;
                canSetItem = false;
            }
            if(State == GameState.Night)
            {
                item.transform.position = GameObject.FindWithTag("Player").transform.position;
            }
        }
    }

    public void AddKey()
    {
        CollectedGoalCnt++;
        
        if (State == GameState.Tutorial)
        {
            if (CollectedGoalCnt == GoalCnt)
            {
                TutorialManager.instance.ActiveButton();
            }
        }
        else
        {
            GateCheck.CollectKey();
        }

    }

    public void Reset()
    {
        Inventory.Coin = 0;
        instance = null;
        Destroy(this.gameObject);
        State = GameState.Title;
        SceneManager.LoadScene("Title");
    }
}
