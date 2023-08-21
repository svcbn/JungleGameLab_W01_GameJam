using System;
using System.Collections;
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
    protected Action ExpireAction { private get; set; }

    [Tooltip("해당 아이템의 지속 시간을 설정")]
    public float Duration
    {
        get
        {
            return StatManager.Instance.TrapDuration;
        }
        set
        {
            StatManager.Instance.TrapDuration = value;
        }
    }
    
    protected EnemyDurationTrap() : base("Enemy")
    {
        
    }

    public override void Execute()
    {
        Action?.Invoke();
        StartCoroutine(nameof(Expire));
    }

    IEnumerator Expire()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(Duration);

        ExpireAction?.Invoke();
        Destroy(gameObject);
    }
        
    

}