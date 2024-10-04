using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerGFX;
    public GameObject gameOverPanel;
    public GameObject weaponCamera;
    public pointsBar pointsBar;
    public GameManagerScript gameManager;
    public float health;
    public healthBar healthBar;

    public float intensity;
    public Volume _volume;
    //Vignette _vignette;


    // Start is called before the first frame update
    void Start()
    {
        /**
        _vignette = _volume.profile.components[0] as Vignette;
        if (!_vignette)
        {
            Debug.LogError("Missing Vignette");
        }
        else
        {
            _vignette.active = false;
        }
        **/
        Time.timeScale = 1;
        health = healthBar.maxHealth;
    }

    public void KillPlayer()
    {
        playerGFX.GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerManager>().weaponCamera.SetActive(false);
        GetComponent<PlayerShooting>().enabled = false;
        gameManager.gameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
       // StartCoroutine(TakeDamageEffect());
        health -= damage;
        healthBar.health = health;

        if (health <= 0)
        {
            health = 0;
            KillPlayer();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Asteroid")
        {
            KillPlayer();
        }   
    }
    /**
    private IEnumerator TakeDamageEffect()
    {
        intensity = 0.4f;


        _vignette.active = true;
        _vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while (intensity > 0)
        {
            intensity -= 0.1f;

            if(intensity < 0) intensity = 0;

            _vignette.intensity.Override(intensity);
            yield return new WaitForSeconds(0.1f);
        }

        _vignette.active = false;
        yield break;

    }
    **/
    // Update is called once per frame
    void Update()
    {

    }
}
