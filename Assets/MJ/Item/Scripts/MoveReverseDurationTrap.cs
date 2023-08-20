using UnityEngine;

/// <summary>
/// [MJ] 대상의 일정 시간 반대로 움직이게 하는 아이템에 적용될 스크립트 
/// </summary>
public class MoveReverseDurationTrap : EnemyDurationTrap
{

    public MoveReverseDurationTrap()
    {
        Type = ItemType.EnemyMoveReserve;
        Action = () =>
        {
            Target.goBack = true;
        };

        ExpireAction = () => { Target.goBack = false; };
    }
}