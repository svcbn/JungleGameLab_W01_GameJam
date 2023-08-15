using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// [MJ] 아이템 추상 베이스 클래스 
/// </summary>
public abstract class Item<T>  : MonoBehaviour where T: MonoBehaviour
{
    private GameManager _gameManager { get; set; }
    protected T Target { get; private set; }
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
        if (_gameManager.state == GameManager.GameState.Night)
        {
            var obj = target.gameObject;
            if (obj.tag.Equals(TargetTag))
            {
                Target = obj.GetComponent<T>();
                Execute();
                
                Destroy(obj);
                Target = null;
            }
        }
    }

    protected abstract void Execute();
    
    public void OnMouseUp()
    {
        if (_gameManager.state == GameManager.GameState.Shop)
        {
            if(_gameManager.Inventory.Coin >= 1)
                {_gameManager.Inventory.BuyItem(_type);}
            else
            {
                UIManager.instance.ShowBuyFailText();
            }
        }
    }
}
