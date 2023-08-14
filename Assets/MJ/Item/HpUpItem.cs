/// <summary>
/// [MJ] 플레이어의 체력을 증가 시키는 아이템에 적용될 스크립트
/// </summary>
public class HpUpItem : PlayerItem
{
    public int value;
    protected override void Execute(PlayerController target)
    {
        target.Hp += value;
    }
}