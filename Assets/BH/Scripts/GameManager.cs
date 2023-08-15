using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// ByeongHan
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentStage = 0;

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

        //ResourceManager.Init();
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

    public GameState state;


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
            ChangeState(GameState.Day);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(GameState.Night);
        }
    }

    void ChangeState(GameState state)
    {
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
    }

    void NightPhase()
    {
        CameraSetting(true, GameObject.FindWithTag("Player").gameObject.transform, 10f, true);
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

    void AddItem()
    {

    }
}
