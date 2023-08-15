using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ByeongHan
/// </summary>
/// 

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]

public class PlayerController : MonoBehaviour
{
    public GameObject warpPos;
    [SerializeField] int _hp = 5;

    float inputX;
    float inputY;
    Vector2 direction;
    Rigidbody2D playerRb;

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
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        direction = new Vector2(inputX, inputY);

        playerRb.
        

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
