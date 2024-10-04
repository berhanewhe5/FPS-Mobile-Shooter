using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidMovement : MonoBehaviour
{
    public float minAsteroidForce;
    public float maxAsteroidForce;
    Vector3 asteroidDirection;

    public GameObject explosionPrefab;
    public GameObject trailPrefab;
    public GameObject sparksPrefab;
    public GameObject pointsPrefab;
    public GameObject[] asteroidMonsters;


    public GameObject explosion;
    public GameObject trail;
    public GameObject sparks;
    

    public Transform player;

    public int waveAsteroidMonsters;

    [Header("Wave 1 Asteroid Monsters")]
    public int wave1MinAsteroidMonsters;
    public int wave1MaxAsteroidMonsters;

    [Header("Wave 2 Asteroid Monsters")]
    public int wave2MinAsteroidMonsters;
    public int wave2MaxAsteroidMonsters;

    [Header("Wave 3 Asteroid Monsters")]
    public int wave3MinAsteroidMonsters;
    public int wave3MaxAsteroidMonsters;

    [Header("Wave 4 Asteroid Monsters")]
    public int wave4MinAsteroidMonsters;
    public int wave4MaxAsteroidMonsters;

    public void SpawningAsteroidsMonsterAttributes(int wave)
    {
        switch (wave)
        {
            case 1:
                waveAsteroidMonsters = Random.Range(wave1MinAsteroidMonsters, wave1MaxAsteroidMonsters+1);
                break;
            case 2:
                waveAsteroidMonsters = Random.Range(wave2MinAsteroidMonsters, wave2MaxAsteroidMonsters + 1);
                break;
            case 3:
                waveAsteroidMonsters = Random.Range(wave3MinAsteroidMonsters, wave3MaxAsteroidMonsters + 1);
                break;
            case 4:
                waveAsteroidMonsters = Random.Range(wave4MinAsteroidMonsters, wave4MaxAsteroidMonsters + 1);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {



        //generate code to make the explosion a child of this game object

        
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 asteroidDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, -0.1f), Random.Range(-1f, 1f)).normalized;
       // Debug.DrawRay(transform.position, asteroidDirection * 300000f, Color.red); // The ray will be visible for 5 seconds

    }

    public void getAsteroidDirection()
    {
        explosion = Instantiate(explosionPrefab, this.transform);
        trail = Instantiate(trailPrefab, this.transform);
        sparks = Instantiate(sparksPrefab, this.transform);

        float forceMagnitude = Random.Range(minAsteroidForce, maxAsteroidForce);

        // Generate a random direction vector with a negative y-component
        bool touchedIsland = false;
        while (touchedIsland == false)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, -0.1f), Random.Range(-1f, 1f)).normalized;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, randomDirection);

            if (Physics.Raycast(ray, out hit, 300000f))
            {
                if (hit.collider.tag == "Island")
                {
                    touchedIsland = true;
                    asteroidDirection = randomDirection;
                }
            }

        }

        // Draw the ray for debugging purposes
        Debug.DrawRay(transform.position, asteroidDirection * 300000f, Color.red, 5.0f); // The ray will be visible for 5 seconds

        // Get the Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();

        // Apply force to the Rigidbody in the random downward direction
        rb.AddForce(asteroidDirection * forceMagnitude, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Island")
        {
            StartCoroutine("DestroyAsteroid");
           
        }
    }

    IEnumerator DestroyAsteroid()
    {
        GetComponent<Animator>().enabled = false;
        this.transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject points = Instantiate(pointsPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),Quaternion.identity);
        points.transform.eulerAngles = new Vector3(-90, 0, 0);
        points.GetComponent<ParticleSystem>().trigger.SetCollider(0, player.GetComponentInChildren<CapsuleCollider>());
        points.GetComponent<ParticleCollector>().pointsBar = player.GetComponent<PlayerManager>().pointsBar;
        points.GetComponent<ParticleCollector>().player = player;
        explosion.SetActive(true);
        trail.SetActive(false);
        sparks.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        Debug.Log(waveAsteroidMonsters);
        for (int i = 0; i < waveAsteroidMonsters; i++)
        {
            asteroidMonsters[i].SetActive(true); 
            asteroidMonsters[i].GetComponent<EnemyController>().player = player;
            asteroidMonsters[i].transform.position = new Vector3(transform.position.x, 1.783F, transform.position.z);
        }

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
