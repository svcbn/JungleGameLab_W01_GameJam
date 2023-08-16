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
    [SerializeField] float originSpinnerSize = 1f;

    int rotateDirection;
    float rotateSpeed;
    float spinnerSize;

    public GameObject arrow;

    
    void Start()
    {
        OriginSetting();
    }

    
    void Update()
    {
        arrow.transform.RotateAround(transform.position, Vector3.forward, rotateDirection * rotateSpeed * Time.deltaTime);

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
        spinnerSize *= size;
        transform.localScale = Vector3.one * spinnerSize;
    }

    /// <summary>
    /// Change Spinner's Speed 'X float'
    /// </summary>
    /// <param name="speed"></param>
    public void ChangeSpinnerSpeed(float speed)
    {
        rotateSpeed *= speed;
    }
}
