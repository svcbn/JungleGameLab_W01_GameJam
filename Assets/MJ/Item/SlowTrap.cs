using UnityEditor;
using UnityEngine;

/// <summary>
/// [MJ] 적의 속도를 낮추는 아이템에 적용될 스크립트
/// </summary>
public class SlowTrap : EnemyDurationTrap
{
    public int speedDownValue;

    public SlowTrap()
    {
        Action = () => { _target.Speed -= speedDownValue; };
    }

    protected override void ExpireDuration()
    {
        _target.Speed += speedDownValue;
        base.ExpireDuration();
    }
}