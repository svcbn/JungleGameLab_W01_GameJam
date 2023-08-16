using System.Collections;
using UnityEngine;

//MEGANLAB 230815
public class EnemyController : MonoBehaviour
{
    public float moveSpeed; // 이동 속도

    private GameObject player; // 플레이어 오브젝트

    public GameObject Look;
    
    public bool Stop { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Stop = false;
        moveSpeed = UnityEngine.Random.Range(2f, 5f);
        moveSpeed += GameManager.instance.Stage * 0.5f;
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

    public bool goBack = false;
    Vector3 newPosition;

    //코루틴으로 플레이어 추격
    public IEnumerator ChasePlayer(Transform enemyTransform)
    {
        while (true)
        {
            if (!Stop)
            {
                if (player != null && enemyTransform != null)
                {
                    Vector3 targetPosition = player.transform.position;

                    newPosition = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, 0);

                    if (newPosition.x < 0)
                    {
                        Look.transform.localScale = new Vector3(1,1,1);
                    }
                    else
                    {
                        Look.transform.localScale = new Vector3(-1,1,1);
                    }

                    if (goBack)
                    {
                        transform.position -= newPosition.normalized * moveSpeed * Time.deltaTime;

                    }
                    else
                    {
                        transform.position += newPosition.normalized * moveSpeed * Time.deltaTime;

                    }
                }
            }

            yield return null;
        }
    }

}


