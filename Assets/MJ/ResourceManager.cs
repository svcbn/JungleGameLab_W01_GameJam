
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [MJ] 프리팹을 들고 있기 위핸 리소스 매니저 스크립트
/// </summary>
public static class ResourceManager
{
    public static Dictionary<ItemType, GameObject> ItemPrefabDict { get; private set; }
    
    /// <summary>
    /// 각종 리소스들을 로드
    /// </summary>
    public static void Init()
    {
        // Load Item-Related Prefabs 
        ItemPrefabDict = new Dictionary<ItemType, GameObject>();
        for (int index = 0, cnt = Enum.GetNames(typeof(ItemType)).Length; index < cnt; index++)
        {
            var type = (ItemType)index;
            var obj = Resources.Load("Prefabs/Items/" + type.ToDescription());
            if (obj as GameObject != null)
            {
                ItemPrefabDict.Add(type, (obj as GameObject));
            }
        }   
    }
}

