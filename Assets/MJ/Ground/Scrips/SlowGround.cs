using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [MJ] 대상의 속도를 낮추는 영역에 적용하는 스크립트
/// </summary>
public class SlowGround : MonoBehaviour
{
    [Tooltip("낮출 속도")]
    public float Value
    {
        get
        {
            return StatManager.Instance.SlowGround;
        }
        set
        {
            StatManager.Instance.SlowGround = value;
        }
    }
    public void OnTriggerEnter2D(Collider2D target)
    {
        var obj = target.gameObject;
        
        // [TODO] 두 가지 다 공동 State 정보를 가지고 동일한 클래스 정보를 읽어 속도를 낮출 수 있으면 좋을텐데...
        if (obj.tag.Equals("Player"))
        {
            var player = obj.GetComponent<PlayerController>();
            player.MaxSpeed = Value;
        }
        else if (obj.tag.Equals("Enemy"))
        {
            var enemy  = obj.GetComponent<EnemyController>(); 
            enemy.Speed -= Value / 2;
        } 
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        var obj = target.gameObject;
        if (obj.tag.Equals("Player"))
        {
            var player = obj.GetComponent<PlayerController>();
            player.MaxSpeed = StatManager.Instance.playerOriginMaxSpeed;
        }
        else if (obj.tag.Equals("Enemy"))
        {
            var enemy  = obj.GetComponent<EnemyController>(); 
            enemy.Speed += Value / 2;
        }
    }
}
