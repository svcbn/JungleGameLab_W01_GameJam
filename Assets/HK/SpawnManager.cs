using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MEGANLAB 230815

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public int NumberOfEnemies = 2; // 스폰할 적의 수
    public float SpawnPosX
    {
        get
        {
            return StatManager.Instance.SpawnPosX;
        }
        set
        {
            StatManager.Instance.SpawnPosX = value;
        }
    }
    public float SpawnPosY
    {
        get
        {
            return StatManager.Instance.SpawnPosY;
        }
        set
        {
            StatManager.Instance.SpawnPosY = value;
        }
    }

    public void SpawnStart()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;
        NumberOfEnemies = GameManager.Instance.Stage + 1;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                StartCoroutine(enemyController.ChasePlayer(enemy.transform));
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randX;
        float randY;

        while (true)
        {
            randX = Random.Range(-SpawnPosX, SpawnPosX);
            if(randX < 10 && randX > -10)
            { 
                continue;
            }
            randY = Random.Range(-SpawnPosY, SpawnPosY);
            if(randY < 8 && randY > -8)
            {
                continue;
            }
            break;
        }
       
        Vector3 spawnPosition = new Vector3(transform.position.x + randX, transform.position.y + randY, 0f);
        return spawnPosition;
    }

}
