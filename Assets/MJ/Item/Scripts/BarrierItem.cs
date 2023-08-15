
/// <summary>
/// [MJ] 쉴드 아이템에 적용될 스크립트
/// </summary>
public class BarrierItem : PlayerItem
{
    protected override void Execute(PlayerController target)
    {
        target.OnBarrier();
    }
}