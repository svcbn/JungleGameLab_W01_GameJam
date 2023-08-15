/// <summary>
/// [MJ] 나침반의 방향 전환에 사용되는 아이템에 적용될 스크립트
/// </summary>
public class ArrowSpinnerChangeDirectionItem : ArrowSpinnerItem
{
    protected override void Execute(ArrowSpinner target)
    {
        target.ChangeRotateDirection();
    }
}