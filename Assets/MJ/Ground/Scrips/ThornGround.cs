using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// [MJ] 데미지를 주는 영역에 적용하는 스크립트
/// </summary>
public class ThornGround : MonoBehaviour
{
    [Tooltip("데미지 주기")]
    public float interval;
    
    [Tooltip("가시가 사용자게에 입히는 데미지")]
    public int damage;
    
    private PlayerController _player;
    public void OnTriggerEnter2D(Collider2D target)
    {
        var obj = target.gameObject;
        if (obj.tag.Equals("Player"))
        {
            _player = obj.GetComponent<PlayerController>();
            StartCoroutine(nameof(OnDamageToTarget));
        }
    }

    public void OnTriggerExit2D(Collider2D target)
    {
        var obj = target.gameObject;
        if (obj.tag.Equals("Player"))
        {
            StopCoroutine(nameof(OnDamageToTarget));
            _player = null;
        }
    }
    
    IEnumerator OnDamageToTarget()
    {
        while (true)
        {
            _player.GetDamage();
            yield return new WaitForSeconds(interval);
        }
    }
    
    
    
}
