using System.Collections;
using UnityEngine;

//MEGANLAB 230815
public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.0f; // 이동 속도

    private GameObject player; // 플레이어 오브젝트
    
    public bool Stop { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Stop = false;
    }


    //플레이어와의 충돌시, 데미지 메서드 호출. 재 확인 필요
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.GetDamage(); 
            }           
        }
    }

    //코루틴으로 플레이어 추격
    public IEnumerator ChasePlayer(Transform enemyTransform)
    {
        while (true)
        {
            if (!Stop)
            {
                if (player != null && enemyTransform != null)
                {
                    Vector2 targetPosition = player.transform.position;
                    Vector2 newPosition = Vector2.MoveTowards(enemyTransform.position, targetPosition, moveSpeed * Time.deltaTime);
                    enemyTransform.position = newPosition;
                }
            }

            yield return null;
        }
    }

}


