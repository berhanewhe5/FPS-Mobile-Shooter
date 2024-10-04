using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeGun : MonoBehaviour
{
    public GameObject pistol;
    public GameObject uzi;
    public GameObject assaultRifle;
    public GameObject shotgun;

    public GameManagerScript gameManager;
    public pointsBar pointsBar;
    public void UpgradeToUzi()
    {
        pistol.SetActive(false);
        uzi.SetActive(true);
    }

    public void UpgradeToAssaultRifle()
    {
        uzi.SetActive(false);
        assaultRifle.SetActive(true);
    }

    public void UpgradeToShotgun()
    {
        assaultRifle.SetActive(false);
        shotgun.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && pointsBar.pointsSlider.value <= 100f)
        {
            if (gameManager.wave == 1)
            {
                UpgradeToUzi();
            }
            else if (gameManager.wave == 2)
            {
                UpgradeToAssaultRifle();
            }
            else if (gameManager.wave == 3)
            {
                UpgradeToShotgun();
            }

            pointsBar.points = 0;
            gameManager.ChangeWave();

        }
    }
}
