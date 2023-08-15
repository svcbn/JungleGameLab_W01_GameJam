using System;
using UnityEngine;

/// <summary>
/// [MJ] 지속 시간을 가지는 아이템의 추상 클래스 입니다.
/// </summary>
public abstract class EnemyDurationTrap : Item<EnemyController>
{
    /// <summary>
    /// 해당 아이템의 동작을 설정
    /// </summary>
    protected Action Action { private get; set; }
    
    [Tooltip("해당 아이템의 지속 시간을 설정")]
    public int duration;
    
    protected EnemyDurationTrap() : base("Enemy")
    {
        
    }

    protected override void Execute()
    {
        Action?.Invoke();
        Invoke(nameof(ExpireDuration), duration);
    }

    protected virtual void ExpireDuration()
    {
        Action = null;
    }

}