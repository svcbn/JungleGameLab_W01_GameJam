using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [MJ] 상점 아이템 프리팹에 적용될 스크립트 
/// </summary>
public class ShopItem : MonoBehaviour
{
    private Image _image;

    public void Init(ItemType type)
    {
        var sprite = ResourceManager.ItemSpriteDict[type];
        _image = GetComponent<Image>();
        _image.sprite = sprite;
    }
}