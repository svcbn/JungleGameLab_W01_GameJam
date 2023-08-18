
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// [MJ] 프리팹을 들고 있기 위핸 리소스 매니저 스크립트
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<ItemType> BoxItemList { get; private set; } = new();
    public List<ItemType> ShopItemList { get; private set; } = new();
    
    public Dictionary<ItemType, GameObject> ItemPrefabDict { get; private set; }
    
    public Dictionary<ItemType, Sprite> ItemSpriteDict { get; private set; }
    
    /// <summary>
    /// 각종 리소스들을 로드
    /// </summary>
    public void Init()
    {
        // Load Item Prefabs 
        ItemPrefabDict = new();
        for (int index = 0, cnt = Enum.GetNames(typeof(ItemType)).Length; index < cnt; index++)
        {
            var type = (ItemType)index;
            var obj = Resources.Load("Prefabs/Items/" + type.ToDescription());
            if (obj as GameObject != null)
            {
                ItemPrefabDict.Add(type, (obj as GameObject));
                
                BoxItemList.Add(type);
                
                // 상점 아이템에 추가
                if ((obj as GameObject).tag == "Item")
                {
                    ShopItemList.Add(type);
                }
            }
        }
        
        // Load Item Sprites
        ItemSpriteDict = new();
        var sprites = Resources.LoadAll<Sprite>($"Sprites/mapIcons").ToList();
        for(int index = 1, cnt = sprites.Count ; index <= cnt; index++ )
        {
            ItemSpriteDict.Add((ItemType)index, sprites[index - 1]);
        }
    }
}

