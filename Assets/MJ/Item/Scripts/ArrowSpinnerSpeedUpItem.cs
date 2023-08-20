using UnityEngine;

/// <summary>
/// [MJ] 나침반의 속도를 높히는 아이템에 적용될 스크립트
/// </summary>
public class ArrowSpinnerSpeedUpItem : ArrowSpinnerItem
{
    [Tooltip("증가시킬 배수를 입력 (=기존 나침반 속도 * value)")]
    public float Value
    {
        get
        {
            return StatManager.Instance.ArrowSpeedUp;
        }
        set
        {
            StatManager.Instance.ArrowSpeedUp = value;
        }
    }

    public ArrowSpinnerSpeedUpItem()
    {
        Type = ItemType.CompassArrowSpeedUp;
    }
    
    public override void Execute()
    {
        Target.ChangeSpinnerSpeed(Value);
    }
}