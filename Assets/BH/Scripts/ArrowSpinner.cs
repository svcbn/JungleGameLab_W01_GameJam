using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ByeongHan
/// </summary>
/// 

public class ArrowSpinner : MonoBehaviour
{

    int RotateDirection
    {
        get
        {
            return StatManager.Instance.RotateDirection;
        }
        set
        {
            StatManager.Instance.RotateDirection = value;
        }
    }
    float RotateSpeed
    {
        get
        {
            return StatManager.Instance.RotateSpeed;
        }
        set
        {
            StatManager.Instance.RotateSpeed = value;
        }
    }
    float SpinnerSize
    {
        get
        {
            return StatManager.Instance.SpinnerSize;
        }
        set
        {
            StatManager.Instance.SpinnerSize = value;
        }
    }

    public GameObject arrow;

    
    void Update()
    {
        arrow.transform.RotateAround(transform.position, Vector3.forward, RotateDirection * RotateSpeed * Time.deltaTime);
    }


    /// <summary>
    /// Change Spinner's Rotation Direction with -1 or +1
    /// </summary>
    public void ChangeRotateDirection()
    {
        RotateDirection *= -1;
    }

    /// <summary>
    /// Change Spinner's Scale 'X float'
    /// </summary>
    public void ChangeSpinnerSize(float size)
    {
        SpinnerSize *= size;
        transform.localScale = Vector3.one * SpinnerSize;
    }

    /// <summary>
    /// Change Spinner's Speed 'X float'
    /// </summary>
    /// <param name="speed"></param>
    public void ChangeSpinnerSpeed(float speed)
    {
        RotateSpeed *= speed;
    }
}
