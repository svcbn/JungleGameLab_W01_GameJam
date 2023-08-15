/// <summary>
/// [MJ] 적에게 스턴 효과를 부여하는 아이템에 적용될 스크립트
/// </summary>
public class StunTrap : EnemyDurationTrap
{
    public StunTrap()
    {
        Action = () => { _target.Stop = true; };
    }

    protected override void ExpireDuration()
    {
        _target.Stop = false;
        base.ExpireDuration();
    }
}