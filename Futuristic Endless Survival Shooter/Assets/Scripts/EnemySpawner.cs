using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int wave1InitialEnemies;
    public int wave2InitialEnemies;
    public int wave3InitialEnemies;
    public int wave4InitialEnemies;

    public float wave1SpawnRate;
    public float wave2SpawnRate;
    public float wave3SpawnRate;
    public float wave4SpawnRate;

    public float spawnRate;
    public float initialEnemies;

    public float spawnXMin;
    public float spawnXMax;

    public float spawnY;

    public float spawnZMin;
    public float spawnZMax;

    public bool shouldSpawnEnemies = true;

    public Transform player;

    public int enemiesKilled = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    public void SpawningAsteroidsAttributes(int wave)
    {
        switch (wave)
        {
            case 1:
                spawnRate = wave1SpawnRate;
                initialEnemies = wave1InitialEnemies;
                break;
            case 2:
                spawnRate = wave2SpawnRate;
                initialEnemies = wave2InitialEnemies;
                break;
            case 3:
                spawnRate = wave3SpawnRate;
                initialEnemies = wave3InitialEnemies;
                break;
            case 4:
                spawnRate = wave4SpawnRate;
                initialEnemies = wave4InitialEnemies;
                break;
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (shouldSpawnEnemies)
        {
            int posType = Random.Range(0, 4);

            if (posType == 0)
            {
                spawnXMin = -spawnXMax;
                spawnXMax = spawnXMax;
                spawnZMin = spawnZMin;
                spawnZMax = spawnZMax;
            }
            else if (posType == 1)
            {
                spawnXMin = -spawnXMin;
                spawnXMax = -spawnXMax;
                spawnZMin = -spawnZMax;
                spawnZMax = spawnZMax;
            }
            else if (posType == 2)
            {
                spawnXMin = -spawnXMax;
                spawnXMax = spawnXMax;
                spawnZMin = -spawnZMin;
                spawnZMax = -spawnZMax;
            }
            else if (posType == 3)
            {
                spawnXMin = spawnXMin;
                spawnXMax = spawnXMax;
                spawnZMin = -spawnZMax;
                spawnZMax = spawnZMax;
            }   

            Vector3 spawnPosition = new Vector3(Random.Range(spawnXMin, spawnXMax) , spawnY, Random.Range(spawnZMin, spawnZMax));
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().enemySpawner = this;
            enemy.GetComponent<EnemyController>().player = player;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public void RecalculatePosition()
    {
        int posType = Random.Range(0, 4);

        if (posType == 0)
        {
            spawnXMin = -spawnXMax;
            spawnXMax = spawnXMax;
            spawnZMin = spawnZMin;
            spawnZMax = spawnZMax;
        }
        else if (posType == 1)
        {
            spawnXMin = -spawnXMin;
            spawnXMax = -spawnXMax;
            spawnZMin = -spawnZMax;
            spawnZMax = spawnZMax;
        }
        else if (posType == 2)
        {
            spawnXMin = -spawnXMax;
            spawnXMax = spawnXMax;
            spawnZMin = -spawnZMin;
            spawnZMax = -spawnZMax;
        }
        else if (posType == 3)
        {
            spawnXMin = spawnXMin;
            spawnXMax = spawnXMax;
            spawnZMin = -spawnZMax;
            spawnZMax = spawnZMax;
        }
    }
}
