using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static GameManager;

public class Tutorial : MonoBehaviour
{
    int currentIdx = 0;
    List<Action> actions = new List<Action>();
    bool canClick = false;

    public TextMeshProUGUI textMiddle;
    public GameObject textClick;
    public GameObject panel;
    public GameObject go2;
    public GameObject go3;
    public GameObject go4;
    public GameObject go5;

    private void Start()
    {
        textMiddle.text = "Day 1 of filming Nature's Deepest Night";
        textMiddle.enabled = true;
        StartCoroutine(DotText());
        StartCoroutine(AppearText());

        actions.Add(Idx1);
        actions.Add(Idx2);
        actions.Add(Idx3);
        actions.Add(Idx4);

        panel.SetActive(false);
    }

    IEnumerator DotText()
    {
        int dotNum = 0;
        WaitForSeconds wfs = new WaitForSeconds(0.7f);

        while (true)
        {
            if(dotNum < 3)
            {
                textMiddle.text += ".";
                dotNum++;
            }
            else
            {
                textMiddle.text = "Day 1 of filming Nature's Deepest Night";
                dotNum = 0;
            }
            yield return wfs;
        }
    }

    IEnumerator AppearText()
    {
        yield return new WaitForSeconds(3f);
        textClick.SetActive(true);
        canClick = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canClick)
        {
            if (currentIdx < actions.Count)
            {
                actions[currentIdx]();
                currentIdx++;
            }
            else
            {
                SceneManager.LoadScene("DayN");
                GameManager.Instance.ChangeState(GameState.Shop);
            }
        }
    }

    void Idx1()
    {
        StopCoroutine(DotText());
        textMiddle.enabled = false;
        panel.SetActive(true);
        go2.SetActive(true);
    }

    void Idx2()
    {
        go2.SetActive(false);
        go3.SetActive(true);
    }

    void Idx3()
    {
        go3.SetActive(false);
        go4.SetActive(true);
    }

    void Idx4()
    {
        go4.SetActive(false);
        go5.SetActive(true);
    }
}
