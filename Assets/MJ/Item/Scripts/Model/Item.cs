using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// [MJ] 아이템 추상 베이스 클래스 
/// </summary>
public abstract class Item<T>  : MonoBehaviour where T: MonoBehaviour
{
    private GameManager _gameManager { get; set; }
    private string TargetTag { get; }

    [Tooltip("반드시 선언된 열거자 중 일치하는 타입을 설정해주세요 ")]
    public ItemType _type;
    
    public Item(string tag)
    {
        TargetTag = tag;
    }

    public void Start()
    {
        _gameManager = GameManager.instance;
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (_gameManager.state == GameManager.GameState.Current)
        {
            var obj = target.gameObject;
            if (obj.tag.Equals(TargetTag))
            {
                Execute(obj.GetComponent<T>());
                Destroy(obj);
            }
        }
    }

    protected abstract void Execute(T target);

    public void OnMouseUp()
    {
        if (_gameManager.state == GameManager.GameState.Shop)
        {
            _gameManager.BuyItem(_type);
        }
    }
}
