using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Ignore,
    Player,
    EnemyTrap,
    Util
}

public class ItemInfo : MonoBehaviour
{
    public ItemType ItemType;
    
    public ItemCategory Category;
    
    [Tooltip("이름")]
    public string ShortName;
    
    [Tooltip("설명")]
    public string Description;

    public Sprite ItemSprite;
}
