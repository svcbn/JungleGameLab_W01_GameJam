
/// <summary>
/// [MJ] 쉴드 아이템에 적용될 스크립트
/// </summary>
public class BarrierItem : PlayerItem
{
    public BarrierItem()
    {
        Type = ItemType.PlayerBarrier;
    }
    public override void Execute()
    {
        Target.OnBarrier();
    }
}