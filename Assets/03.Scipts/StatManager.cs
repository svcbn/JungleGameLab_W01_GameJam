using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    [Header("GameManager")]
    public float DayTime = 15f;
    public int Stage;
    public int GoalCount;
    public int DefaultPlayerHP = 5;

    [Header("Player")]
    public int HP = 5;
    public float PlayerSpeed = 1000f;
    public float playerOriginMaxSpeed = 8f;
    public float PlayerMaxSpeed = 8f;
    public float PlayerWarpSpeed = 0.05f;
    public float PlayerKnockBackPower = 7f;
    public float PlayerKnockBackRadius = 7.5f;

    [Header("Inventory")]
    public int RoundCoin = 5;

    [Header("ArrowSpinner")]
    public int RotateDirection = 1;
    public float RotateSpeed = 200f;
    public float SpinnerSize = 1f;

    [Header("EnemySpawnManager")]
    public float SpawnPosX = 30f;
    public float SpawnPosY = 20f;

    [Header("Enemy")]
    public float EnemySpeed = 3f;

    [Header("Item")]
    public float ItemSetRadius = 5f;

    [Header("PlayerItem")]
    public int FieldOfViewEnlarger = 3;
    public int FieldOfViewMax = 20;
    public float ArrowSpeedUp = 1.5f;
    public float SpinnerSizeUp = 1.2f;
    public float SpinnerSizeMax = 2f;

    [Header("TrapItem")]
    public float TrapDuration = 2f;
    public float SpeedDown = 4f;

    [Header("GroundEffect")]
    public float SlowGround = 4f;


}
