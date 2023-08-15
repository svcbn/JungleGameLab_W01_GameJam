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

        ResourceManager.Init();
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
        vignette = GetComponent<PostProcessVolume>().profile.GetSetting<Vignette>();
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

    void RandomBoxSetting(int difficulty, int keys)
    {
        maxKeys = difficulty * keys;
       
        foreach (var box in boxes)
        {
            //GameObject itemPrefab = 

            //box.GetComponent<ItemBox>().item.Append();
        }
    }

    void AddItem()
    {

    }


    public void BuyItem(ItemType item)
    {
        
    }
}
