using System;
using UnityEngine;
using static GameManager;

/// <summary>
/// [MJ] 아이템 설치 영역 설정을 위한 컨트롤러 스크립트
/// </summary>
public class ItemAreaController : MonoBehaviour
{
    
    [Tooltip("아이템 설치 가능 반경 값")]
    public float Radius
    {
        get
        {
            return StatManager.Instance.ItemSetRadius;
        }
        set
        {
            StatManager.Instance.ItemSetRadius = value;
        }
    }
    
    [Tooltip("아이템 설치 가능 반경을 나타내주는 오브젝트")]
    public GameObject unbuildableAreaObj;

    public void Start()
    {
        
        // Collider 반경 설정
        GetComponent<CircleCollider2D>().radius = Radius;

        // 영역 오브젝트 크기 설정     
        unbuildableAreaObj.transform.localScale = new Vector3(Radius * 2, Radius * 2, 0);
        
        unbuildableAreaObj.SetActive(false);
    }

}
