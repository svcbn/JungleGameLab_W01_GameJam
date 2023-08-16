using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MEGANLAB 230815

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public int numberOfEnemies = 2; // 스폰할 적의 수

    public void SpawnStart()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;
        numberOfEnemies = GameManager.instance.Stage + 1;
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
        float minX = -30f;
        float maxX = 30f;
        float minY = -20f;
        float maxY = 20f;

        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(minX, maxX), transform.position.y + Random.Range(minY, maxY), 0f);
        
       
        return spawnPosition;
    }

}
