using UnityEngine;

/// <summary>
/// [MJ] 시야를 더 넓혀주는 아이템에 적용될 스크립트
/// </summary>
public class FieldOfViewEnlargerItem : PlayerItem
{
    [Tooltip("증가시킬 배수를 입력 (기존 나침반 속도 * value)")]
    public int Value
    {
        get
        {
            return StatManager.Instance.FieldOfViewEnlarger;
        }
        set
        {
            StatManager.Instance.FieldOfViewEnlarger = value;
        }
    }

    public int MaxValue
    {
        get
        {
            return StatManager.Instance.FieldOfViewMax;
        }
        set
        {
            StatManager.Instance.FieldOfViewMax = value;
        }
    }

    public FieldOfViewEnlargerItem()
    {
        Type = ItemType.CameraZoomOut;
    }
    public override void Execute()
    {
        if(Camera.main.orthographicSize < MaxValue)
        {
            Camera.main.orthographicSize += Value;
        }
    }
}