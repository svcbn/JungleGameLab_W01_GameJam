using UnityEngine;


public class ArrowSpinnerSpeedUp : ArrowSpinnerItem
{
    [Tooltip("증가시킬 배수를 입력 (=기존 나침반 속도 * value)")]
    public float value;
    protected override void Execute(ArrowSpinner target)
    {
        target.ChangeSpinnerSpeed(value);
    }
}