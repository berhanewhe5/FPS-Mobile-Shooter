using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int health;
    public NavMeshAgent agent;
    public Transform player;
    public float attackDistance;
    public float attackCooldown = 2f; // Cooldown time between attacks
    public bool touchingIsland;
    public bool touchingWater;
    public EnemySpawner enemySpawner;
    public int attackDamage = 10; // Damage dealt to the player
    private Animator anim;
    private bool isAttacking;


    public int enemyType; // 1 = Sea Monster, 2 = Mini Asteroid Monster, 3 =  Ranged Sea Monster
    public float seaMonsterSpeed;
    public float miniAsteroidMonsterSpeed;

    public float enemyDamage;

    public Rigidbody rb;

    bool dying = false;
    bool onGround = false;


    public float fadeDuration = 2.0f; // Duration of the fade effect
    private Material material;
    private Color originalColor;
    private float fadeTime;

    public Material normalMaterial;
    public Material deathMaterial;

    SkinnedMeshRenderer meshRenderer;

    public GameObject bloodPrefab;

    void Start()
    {

        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        rb = GetComponent<Rigidbody>();
        

        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
            meshRenderer.material = normalMaterial;
        }

        anim = GetComponentInChildren<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator not found on child object.");
        }

        if (enemyType == 1)
        {
            StartCoroutine("RepositionTimer");
            agent.speed = seaMonsterSpeed;
        }
        else if (enemyType == 2)
        {
            transform.parent = null;
            StartCoroutine("RepositionTimer");
            agent.speed = miniAsteroidMonsterSpeed;
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            else
            {
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true;
                }
            }
        }

        if (meshRenderer != null)
        {
            // Ensure each clone has its own material instance
            Material originalMaterial = meshRenderer.sharedMaterial;
            material = new Material(originalMaterial); // Create a new instance
            meshRenderer.material = material; // Assign the new material

            originalColor = material.color;
            fadeTime = fadeDuration;
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (enemyType == 2)
            {
                // Calculate the direction from the object to the target
                Vector3 direction = player.position - transform.position;

                // Only keep the Y component
                direction.y = 0;

                // Ensure the direction is not zero to avoid NaN rotation
                if (direction != Vector3.zero)
                {
                    // Calculate the new rotation
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    // Apply the rotation
                    transform.rotation = rotation;
                }
            }

            if (enemyType == 1)
            {
                if (touchingWater)
                {
                    rb.isKinematic = false;
                }
                else
                {
                    rb.isKinematic = true;
                }
            }

            if (onGround)
            {
                agent.SetDestination(player.position);
            }

            if (enemyType == 2)
            {
                agent.enabled = true;
                agent.SetDestination(player.position);
            }

            if (Vector3.Distance(transform.position, player.position) < attackDistance && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }

        Debug.DrawRay(transform.position, Vector3.down * 300f, Color.red);
    }


    IEnumerator Attack()
    {
        isAttacking = true;

        if (enemyType == 1)
        {
            int attackType = Random.Range(0, 2);
            if (attackType == 0)
            {
                anim.SetTrigger("Attack1");
                agent.speed = 0;
            }
            else
            {
                anim.SetTrigger("Attack2");
                agent.speed = miniAsteroidMonsterSpeed;
            }


        }

        

        yield return new WaitForSeconds(1.25f); // Wait for the middle of the attack animation

        if ((Vector3.Distance(transform.position, player.position) < attackDistance) && !dying)
        {
            ApplyDamageToPlayer();
        }

        yield return new WaitForSeconds(attackCooldown - 0.5f);
        agent.speed = seaMonsterSpeed;
        isAttacking = false;
    }

    void ApplyDamageToPlayer()
    {
        var playerManager = player.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            playerManager.TakeDamage(enemyDamage); // Assuming TakeDamage method exists in PlayerManager
            Debug.Log("Player took damage"+enemyDamage);    
        }
    }

    public void KillPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            var playerManager = player.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.KillPlayer();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            StartCoroutine("TakeDamageEffect");
        }
        if (health <= 0 && !dying)
        {
            dying = true;
            Die();
        }
    }

    IEnumerator TakeDamageEffect()
    {
        Debug.Log("Took damage effect played");
        var meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.05f);

        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.white;
        }
    }
    void Die()
    {
        seaMonsterSpeed = 0;
        miniAsteroidMonsterSpeed = 0;
        agent.speed = 0;

        if (enemyType == 1)
        {
            int deathType = Random.Range(0, 2);
            if (deathType == 0)
            {
                anim.SetTrigger("Death1");
            }
            else if (deathType == 1)
            {
                anim.SetTrigger("Death2");
            }
            StartCoroutine(DeathTimer());
        }
        else if (enemyType == 2)
        {
            Instantiate(bloodPrefab, transform.position + new Vector3 (0, -0.792f, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(3);


        Debug.Log("Fading out");
        if (meshRenderer.material != deathMaterial)
        {
            material = new Material(deathMaterial); // Create a unique instance of the death material
            meshRenderer.material = material; // Assign it to the renderer
            originalColor = material.color;
            fadeTime = fadeDuration; // Reset fade time when switching materials
        }

        for (float fadeAmount = 1; fadeAmount > 0; fadeAmount-=0.02f)
        {
            yield return new WaitForSeconds(0.01f);
            float alpha = Mathf.Clamp01(fadeAmount/1);
            Debug.Log("Alpha: " + alpha);
            Color color = originalColor;
            color.a = alpha;
            material.color = color;
        }
        enemySpawner.enemiesKilled++;
        Destroy(gameObject);
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 300f))
        {
            touchingIsland = hit.collider.CompareTag("Island");
        }
    }

    IEnumerator RepositionTimer()
    {
        yield return new WaitForSeconds(3);
        CheckGround();

        while (touchingIsland)
        {
            if (enemySpawner != null)
            {
                enemySpawner.RecalculatePosition();
                transform.position = new Vector3(Random.Range(enemySpawner.spawnXMin, enemySpawner.spawnXMax), 1.76f, Random.Range(enemySpawner.spawnZMin, enemySpawner.spawnZMax));
            }

            yield return new WaitForSeconds(0.1f);

            CheckGround();
        }

        Debug.Log("Repositioned");
        var meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }
        yield return new WaitForSeconds(1.5f);
        onGround = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            Destroy(gameObject);
        }

        if (collision.collider.tag == "IslandExtension")
        {
            touchingWater = true;
        }
    }

}