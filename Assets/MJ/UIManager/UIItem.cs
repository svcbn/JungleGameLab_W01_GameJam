using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [MJ] 아이템 프리팹에 적용될 스크립트 
/// </summary>
public class UIItem : MonoBehaviour
{
    private Image _image;

    public void Init(ItemType type)
    {
        var sprite = ResourceManager.Instance.ItemSpriteDict[type];
        _image = GetComponent<Image>();
        _image.sprite = sprite;
    }
}