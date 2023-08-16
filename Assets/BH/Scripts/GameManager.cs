using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// ByeongHan
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
    float timeLeft;
    public string timer = "";

    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.Init();
        //Inventory = new InventoryManager();
        vignette = GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();
        RandomBoxSetting(difficulty, keys);
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
                break;
        }
        Debug.Log(state + "");
    }

    void Title()
    {
        
    }

    void Tutorial()
    {

        CameraSetting(false, null, 25f);
    }

    void ShopPhase()
    {
        
    }

    void DayPhase()
    {
        CameraSetting(false, null, 40f);

        StartCoroutine(DayTimer());
    }

    IEnumerator DayTimer()
    {
        timeLeft = dayTime;

        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timer = Mathf.CeilToInt(timeLeft).ToString();
            yield return null;
        }

        ChangeState(GameState.Night);
    }

    void NightPhase()
    {
        CameraSetting(true, GameObject.FindWithTag("Player").gameObject.transform, 10f, true);

        RandomBoxSetting(difficulty, keys);

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
    public GameObject testKey;
    public ItemType Type;

    List<GameObject> testList = new List<GameObject>();

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
