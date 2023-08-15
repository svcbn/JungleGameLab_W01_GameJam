using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public int numberOfEnemies = 2; // 스폰할 적의 수

  
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
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
        float minX = -15f;
        float maxX = 15f;
        float minY = -10f;
        float maxY = 10f;

        Vector3 spawnPosition = Vector3.zero;

        do
        {
            spawnPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        } 
        while ((spawnPosition.x >= -10f && spawnPosition.x <= 10f) || (spawnPosition.y >= -7f && spawnPosition.y <= 7f));

        return spawnPosition;
    }

}
