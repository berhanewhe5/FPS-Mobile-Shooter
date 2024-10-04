using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverPanel;
    public bool gameOver = false;

    public GameObject pausePanel;

    public int wave = 0;
    public TMP_Text waveText;

    public EnemySpawner enemySpawner;

    public Slider waveBar;

    [Header("Enemies to Kill")]
    public int wave1EnemiesToKill;
    public int wave2EnemiesToKill;
    public int wave3EnemiesToKill;
    public int wave4EnemiesToKill;

    public int enemiesToKill;

    public bool waveComplete = false;

    public GameObject uzi;
    public GameObject assaultRifle;
    public GameObject shotgun;

    public float lerpSpeed = 0.05f;

    public GameObject UpgradeGunTrigger;

    void Start()
    {
        UpgradeGunTrigger.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        wave = 0;
        waveText.text = "Wave: " + wave;
        
        ChangeWave();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");

    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }           

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    [ContextMenu("ChangeWave")]
    public void ChangeWave()
    {
        waveText.color = Color.yellow;
        hideGuns();
        waveComplete = false;
        enemySpawner.enemiesKilled = 0;
        wave++;
        waveText.text = "Wave: " + wave;
        GetComponent<AsteroidSpanwer>().SpawningAsteroidsAttributes(wave);
        enemySpawner.SpawningAsteroidsAttributes(wave);

        switch (wave)
        {
            case 1:
                enemiesToKill = wave1EnemiesToKill;
                break;
            case 2:
                enemiesToKill = wave2EnemiesToKill;
                break;
            case 3:
                enemiesToKill = wave3EnemiesToKill;
                break;
            case 4:
                enemiesToKill = wave4EnemiesToKill;
                break;
        }

    }

    public void WaveComplete()
    {
        UpgradeGunTrigger.SetActive(true);
        waveText.text = "Wave: " + wave + " Complete!";
        waveText.color = Color.green;

        if (wave == 1)
        {
            uzi.SetActive(true);
        }
        else if (wave == 2)
        {
            assaultRifle.SetActive(true);
        }
        else if (wave == 3)
        {
            shotgun.SetActive(true);
        }

    }

    public void hideGuns()
    {
        uzi.SetActive(false);
        assaultRifle.SetActive(false);
        shotgun.SetActive(false);
    }
    private void Update()
    {
        waveBar.value = Mathf.Lerp(waveBar.value, (float)enemySpawner.enemiesKilled / enemiesToKill, lerpSpeed) ;

        if (!waveComplete && waveBar.value == 1)
        {
            waveComplete = true;
            WaveComplete();
        }
    }
}
