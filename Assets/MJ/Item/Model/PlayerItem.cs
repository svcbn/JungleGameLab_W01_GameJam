/// <summary>
/// [MJ] 플레이어 아이템의 추상 클래스 입니다.
/// </summary>
public abstract class PlayerItem : Item<PlayerController>
{
    protected PlayerItem() : base("Player")
    {
        
    }
}