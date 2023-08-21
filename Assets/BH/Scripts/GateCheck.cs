using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class GateCheck : MonoBehaviour
{
    private GameManager _gameManager;
    public List<GameObject> doors = new List<GameObject>();

    public bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.GateCheck = this;
        isComplete = false;
        ResetKey();
    }
    
    public void CollectKey()
    {
        if(!isComplete)
        {
            if (_gameManager.CollectedGoalCnt >= _gameManager.GoalCnt)
            {
                OpenDoor();
                isComplete = true;
            }
        }
    }

    void OpenDoor()
    {
        foreach (GameObject go in doors)
        {
            go.GetComponent<Door>().isComplete = true;
        }
    }

    void ResetKey()
    {
        //keys.Clear();
        // 키 추가

        foreach (GameObject go in doors)
        {
            go.SetActive(true);
        }
    }
}