using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ByeongHan
/// </summary>
/// 

public class ArrowSpinner : MonoBehaviour
{
    [SerializeField] int originRotateDirection = 1;
    [SerializeField] float originRotateSpeed = 200f;
    [SerializeField] int originSpinnerSize = 5;

    int rotateDirection;
    float rotateSpeed;
    int spinnerSize;

    [HideInInspector] public GameObject Spinner;
    [HideInInspector] public GameObject Arrow;

    
    void Start()
    {
        OriginSetting();
    }

    
    void Update()
    {
        Arrow.transform.RotateAround(transform.position, Vector3.forward, rotateDirection * rotateSpeed * Time.deltaTime);
    }

    void OriginSetting()
    {
        rotateDirection = originRotateDirection;
        rotateSpeed = originRotateSpeed;
        spinnerSize = originSpinnerSize;
    }

    /// <summary>
    /// Change Spinner's Rotation Direction with -1 or +1
    /// </summary>
    public void ChangeRotateDirection()
    {
        rotateDirection *= -1;

    }

    /// <summary>
    /// Change Spinner's Scale 'X float'
    /// </summary>
    public void ChangeSpinnerSize(float size)
    {
        Spinner.transform.localScale = Vector3.one * size;
    }

    /// <summary>
    /// Change Spinner's Speed 'X float'
    /// </summary>
    /// <param name="speed"></param>
    public void ChangeSpinnerSpeed(float speed)
    {
        rotateSpeed = speed;
    }
}
