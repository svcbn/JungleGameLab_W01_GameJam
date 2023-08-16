using System;
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
    [HideInInspector] public GameObject knockBackArea;
    [SerializeField] int _hp = 5;

    bool isImmune = false;
    SpriteRenderer playerSprite;

    float inputX;
    float inputY;
    Vector2 direction;
    Rigidbody2D playerRb;
    [SerializeField] float speed = 1000f;
    public float maxSpeed = 8f;
    [SerializeField] float warpSpeed = 0.05f;
    [SerializeField] float knockBackPower = 7f;
    [SerializeField] float knockBackRadius = 7.5f;
    [SerializeField] Collider2D[] enemyInRange;
    [HideInInspector] public GameObject barrier;

    GameManager gameManager;

    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            UIManager.instance.UpdatePlayerHp(_hp);
        }
    }

    public enum PlayerState
    {
        Ignore = 0,
        Tutorial,
        Play,
        Die
    }

    public PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        knockBackArea.transform.localScale = Vector3.one * knockBackRadius * 2;
        knockBackArea.SetActive(false);
        playerSprite = GetComponent<SpriteRenderer>();
        gameManager = GameManager.instance;
        gameManager.PlayerObj = this.gameObject;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        LimitMoveSpeed();

        InRangeCheck();

        if (Input.GetMouseButtonDown(1))
        {
            Warp();
        }

    }

    void Move()
    {
        //if (gameManager.State != GameManager.GameState.Night) return;

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        direction = new Vector2(inputX, inputY);
        playerRb.AddForce(direction.normalized * speed * Time.deltaTime, ForceMode2D.Force);
    }

    void LimitMoveSpeed()
    {
        if(playerRb.velocity.x > maxSpeed) playerRb.velocity = new Vector2(maxSpeed, playerRb.velocity.y);
        if (playerRb.velocity.x < -maxSpeed) playerRb.velocity = new Vector2(-maxSpeed, playerRb.velocity.y);
        if (playerRb.velocity.y > maxSpeed) playerRb.velocity = new Vector2(playerRb.velocity.x, maxSpeed);
        if (playerRb.velocity.y < -maxSpeed) playerRb.velocity = new Vector2(playerRb.velocity.x, -maxSpeed);
    }

    void InRangeCheck()
    {
        enemyInRange = Physics2D.OverlapCircleAll(transform.position, knockBackRadius, LayerMask.GetMask("Enemy"));
    }

    void Warp()
    {
        //if (gameManager.State != GameManager.GameState.Night) return;

        transform.position = warpPos.transform.position;
        playerRb.velocity *= warpSpeed;
    }

    /// <summary>
    /// When Get Item:Barrier
    /// </summary>
    public void OnBarrier()
    {
        StartCoroutine(Immune(3f));
        barrier.SetActive(true);
    }

    /// <summary>
    /// When Player Get Damage
    /// </summary>
    public void GetDamage()
    {
        if (isImmune) return;
        HP--;
        StartCoroutine(Immune(1.5f));
        KnockBack();
    }

    IEnumerator Immune(float immuneTime)
    {        isImmune = true;
        float currentTime = 0f;

        while (currentTime < immuneTime)
        {
            currentTime += Time.deltaTime;
            if(playerSprite.isVisible) playerSprite.enabled = false;
            else playerSprite.enabled = true;

            yield return null;
        }
        playerSprite.enabled = true;
        isImmune = false;
        barrier.SetActive(false);
    }

    
    void KnockBack()
    {
        StartCoroutine(KnockBackAreaFeedback());
        foreach(Collider2D collider in enemyInRange)
        {
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce((collider.gameObject.transform.position - transform.position).normalized * knockBackPower , ForceMode2D.Impulse);
        }
    }

    IEnumerator KnockBackAreaFeedback()
    {
        knockBackArea.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        knockBackArea.SetActive(false);
    }
    
}
