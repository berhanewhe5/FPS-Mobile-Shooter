using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHolderComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] asteroids = new GameObject [8];

        int i = 0;
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            asteroids[i] = rb.gameObject;
            rb.gameObject.SetActive(false);
            i++;
        }
        int asteroidNum = Random.Range(0, asteroids.Length);
        asteroids[asteroidNum].SetActive(true);

        asteroids[asteroidNum].GetComponent<asteroidMovement>().getAsteroidDirection();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
