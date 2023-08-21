using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

/// <summary>
/// [MJ] 아이템 추상 베이스 클래스 
/// </summary>
public abstract class Item<T>  : MonoBehaviour where T: MonoBehaviour
{
    private GameManager _gameManager { get; set; }
    protected T Target { get; private set; }
    
    /// <summary>
    /// 충돌 처리할 대상 태그
    /// </summary>
    private string TargetTag { get; }
    
    public ItemType Type { get; protected set; }

    public Item(string tag)
    {
        TargetTag = tag;
    }

    public void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (_gameManager.State == GameManager.GameState.Night)
        {
            var obj = target.gameObject;
            if (obj.tag.Equals(TargetTag))
            {
                Target = obj.GetComponent<T>();
                if (Target == null)
                {
                    Target = obj.GetComponentInChildren<T>();
                }

                Execute();

                if (!target.CompareTag("Enemy"))
                {
                    Destroy(gameObject);
                }
                
                
            }
        }
    }

    public abstract void Execute();
}
