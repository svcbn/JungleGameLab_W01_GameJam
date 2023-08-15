using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ByeongHan
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        Past,
        Current,
        Die
    }

    public GameState state;

    int currentRound = 0;
    float pastTimer = 30f;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Past State");
            ChangeState(GameState.Past);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Current State");
            ChangeState(GameState.Current);
        }
    }

    void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Title:
                break;
            case GameState.Tutorial:
                break;
            case GameState.Past:
                PastRound();
                break;
            case GameState.Current:
                CurrentRound();
                break;
            case GameState.Die:
                break;
        }
    }

    void PastRound()
    {
        Camera.main.transform.SetParent(null);
        Camera.main.transform.position = new Vector3(0, 0, -10); //현재 스테이지의 카메라 위치
        Camera.main.orthographicSize = 10f;

    }

    void CurrentRound()
    {
        Camera.main.transform.SetParent(GameObject.FindWithTag("Player").gameObject.transform, false);
        Camera.main.orthographicSize = 5f;
    }


    public void BuyItem(ItemType item)
    {
        
    }
}
