using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpanwer : MonoBehaviour
{
    public GameObject asteroid;

    public float minSpawnTime;
    public float maxSpawnTime;

    public float minAsteroidDistance;
    public float maxAsteroidDistance;


    public float xDistance;
    public float zDistance;

    public Transform player;

    [Header("Wave 1 Spawn Time")]
    public float wave1MinSpawnTime;
    public float wave1MaxSpawnTime;

    [Header("Wave 2 Spawn Time")]
    public float wave2MinSpawnTime;
    public float wave2MaxSpawnTime;

    [Header("Wave 3 Spawn Time")]
    public float wave3MinSpawnTime;
    public float wave3MaxSpawnTime;

    [Header("Wave 4 Spawn Time")]
    public float wave4MinSpawnTime;
    public float wave4MaxSpawnTime;

    public void SpawningAsteroidsAttributes(int wave)
    {
        switch (wave)
        {
            case 1:
                minSpawnTime = wave1MinSpawnTime;
                maxSpawnTime = wave1MaxSpawnTime;
                break;
            case 2:
                minSpawnTime = wave2MinSpawnTime;
                maxSpawnTime = wave2MaxSpawnTime;
                break;
            case 3:
                minSpawnTime = wave3MinSpawnTime;
                maxSpawnTime = wave3MaxSpawnTime;
                break;
            case 4:
                minSpawnTime = wave4MinSpawnTime;
                maxSpawnTime = wave4MaxSpawnTime;
                break;
        }

        StartCoroutine("spawnAsteroids");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnAsteroids()
    {
        while (!GetComponent<GameManagerScript>().waveComplete)
        {

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            GameObject asteroidPrefab = Instantiate(asteroid, new Vector3(Random.Range(-xDistance, xDistance), Random.Range(minAsteroidDistance, maxAsteroidDistance), Random.Range(-zDistance, zDistance)), Quaternion.identity);
            asteroidMovement[] asteroidsScript = asteroidPrefab.GetComponentsInChildren<asteroidMovement>();

            foreach (asteroidMovement asteroidScript in asteroidsScript)
            {
                asteroidScript.player = player;
                asteroidScript.SpawningAsteroidsMonsterAttributes(GetComponent<GameManagerScript>().wave);
            }
        }
    }
    
}
