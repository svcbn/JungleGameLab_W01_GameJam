using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ByeongHan
/// </summary>
/// 

public class PlayerController : MonoBehaviour
{
    public GameObject warpPos;
    int _hp = 5;

    int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }

    public enum PlayerState
    {
        Tutorial,
        Play,
        Die
    }

    public PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.J))
        {
            transform.position = warpPos.transform.position;
        }
    }

    /// <summary>
    /// When Get Item:Barrier
    /// </summary>
    public void OnBarrier()
    {

    }

    /// <summary>
    /// When Player Get Damage
    /// </summary>
    public void GetDamage()
    {
        HP--;
    }
}
