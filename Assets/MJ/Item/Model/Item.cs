using UnityEngine;

/// <summary>
/// [MJ] 아이템 추상 베이스 클래스 
/// </summary>
public abstract class Item<T> : MonoBehaviour
{
    private GameManager _gameManager { get; }
    private string TargetTag { get; }
    
    public Item(string tag)
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        TargetTag = tag;
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        if (_gameManager.Mode == Mode.Current)
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
}
