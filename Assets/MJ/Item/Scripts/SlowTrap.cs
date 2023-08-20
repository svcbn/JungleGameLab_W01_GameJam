using UnityEditor;
using UnityEngine;

/// <summary>
/// [MJ] 적의 속도를 낮추는 아이템에 적용될 스크립트
/// </summary>
public class SlowTrap : EnemyDurationTrap
{
    [Tooltip("감소 시킬 값을 입력")]
    public float Value
    {
        get
        {
            return StatManager.Instance.SpeedDown;
        }
        set
        {
            StatManager.Instance.SpeedDown = value;
        }
    }

    public SlowTrap()
    {
        Type = ItemType.EnemySlow;
        Action = () => { Target.Speed -= Value; };

        ExpireAction = () => { Target.Speed += Value; };
    }

    
}