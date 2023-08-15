using System;
using UnityEngine;

/// <summary>
/// [MJ] 아이템 설치 영역 설정을 위한 컨트롤러 스크립트
/// </summary>
public class ItemAreaController : MonoBehaviour
{
    private GameManager _gameManager;
    
    [Tooltip("아이템 설치 가능 반경 값")]
    public int radius;
    
    [Tooltip("아이템 설치 가능 반경을 나타내주는 오브젝트")]
    public GameObject unbuildableAreaObj;
    
    private CircleCollider2D unbuildableAreaCol; 

    public void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        // Collider 반경 설정
        unbuildableAreaCol = GetComponent<CircleCollider2D>();
        GetComponent<CircleCollider2D>().radius = radius;

        // 영역 오브젝트 크기 설정     
        unbuildableAreaObj.transform.localScale = new Vector3(radius * 2, radius * 2, 0);
        
        unbuildableAreaCol.enabled = false;
        unbuildableAreaObj.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag.Equals("Player"))
            unbuildableAreaObj.SetActive(true);
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        if(target.gameObject.tag.Equals("Player"))
            unbuildableAreaObj.SetActive(false);
    }

    /// <summary>
    /// GameController에 설정 필요
    /// </summary>
    /// <param name="gameState"></param>
    public void ChangeMode(GameState gameState)
    {
        unbuildableAreaObj.SetActive(false);
        unbuildableAreaCol.enabled =  gameState == GameState.Past;
    }
    
}
