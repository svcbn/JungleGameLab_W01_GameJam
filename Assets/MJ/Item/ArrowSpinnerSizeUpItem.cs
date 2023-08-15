using UnityEngine;

public class ArrowSpinnerSizeUpItem : ArrowSpinnerItem
{
    [Tooltip("증가시킬 배수를 입력 (=기존 나침반 반경 * value)")]
    public int value;
    protected override void Execute(ArrowSpinner target)
    {
        target.ChangeSpinnerSize(value);
    }
}