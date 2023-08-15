using UnityEditor;
using UnityEngine;

/// <summary>
/// [MJ] 적의 속도를 낮추는 아이템에 적용될 스크립트
/// </summary>
public class SlowTrap : EnemyDurationTrap
{
    [Tooltip("감소 시킬 값을 입력")]
    public int speedDownValue;

    public SlowTrap()
    {
        Action = () => { Target.moveSpeed -= speedDownValue; };
    }

    protected override void ExpireDuration()
    {
        Target.moveSpeed += speedDownValue;
        base.ExpireDuration();
    }
}