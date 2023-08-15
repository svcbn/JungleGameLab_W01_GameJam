using UnityEngine;

/// <summary>
/// [MJ] 나침반의 속도를 높히는 아이템에 적용될 스크립트
/// </summary>
public class ArrowSpinnerSpeedUpItem : ArrowSpinnerItem
{
    [Tooltip("증가시킬 배수를 입력 (=기존 나침반 속도 * value)")]
    public float value;
    protected override void Execute(ArrowSpinner target)
    {
        target.ChangeSpinnerSpeed(value);
    }
}