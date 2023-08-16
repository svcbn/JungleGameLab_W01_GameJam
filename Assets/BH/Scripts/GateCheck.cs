using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCheck : MonoBehaviour
{
    public List<GameObject> keys = new List<GameObject>();
    public List<GameObject> doors = new List<GameObject>();

    public bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetKey();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.instance.State == GameManager.GameState.Die)
        //{
        //    ResetKey();
        //}
    }

    public void CollectKey(GameObject obj)
    {
        keys.Remove(obj);
        obj.SetActive(false);
        
        if (keys.Count == 0)
        {
            OpenDoor();
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
        // Å° Ãß°¡

        foreach (GameObject go in doors)
        {
            go.SetActive(true);
        }
    }
}