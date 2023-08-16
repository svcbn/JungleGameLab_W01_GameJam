/// <summary>
/// [MJ] 적에게 스턴 효과를 부여하는 아이템에 적용될 스크립트
/// </summary>
public class StunTrap : EnemyDurationTrap
{
    public StunTrap()
    {
        Type = ItemType.EnemyStun;
        Action = () => { Target.Stop = true; };

        ExpireAction = () => { Target.Stop = false; };
    }

}