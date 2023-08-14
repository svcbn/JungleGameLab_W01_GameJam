
/// <summary>
/// [MJ] 대상의 일정 시간 반대로 움직이게 하는 아이템에 적용될 스크립트 
/// </summary>
public class MoveReverseDurationTrap : EnemyDurationTrap
{
    public MoveReverseDurationTrap()
    {
        Action = () => 
        { 
            // 대상을 반대로 움직이게 
        };
    }

    protected override void ExpireDuration()
    {
        // 대상의 이동 초기화
        base.ExpireDuration();
    }
}