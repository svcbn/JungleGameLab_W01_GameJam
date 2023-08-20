using UnityEngine;

/// <summary>
/// [MJ] 나침반의 영역을 넓히는 아이템에 적용될 스크립트
/// </summary>
public class ArrowSpinnerSizeUpItem : ArrowSpinnerItem
{
    [Tooltip("증가시킬 배수를 입력 (=기존 나침반 반경 * value)")]
    public float Value
    {
        get
        {
            return StatManager.Instance.SpinnerSizeUp;
        }
        set
        {
            StatManager.Instance.SpinnerSizeUp = value;
        }
    }

    public float MaxValue
    {
        get
        {
            return StatManager.Instance.SpinnerSizeMax;
        }
        set
        {
            StatManager.Instance.SpinnerSizeMax = value;
        }
    }

    public ArrowSpinnerSizeUpItem()
    {
        Type = ItemType.CompassArrowSizeUp;
    }
    
    public override void Execute()
    {
        if (StatManager.Instance.SpinnerSize < MaxValue)
        {
            Target.ChangeSpinnerSize(Value);
        }
    }
}