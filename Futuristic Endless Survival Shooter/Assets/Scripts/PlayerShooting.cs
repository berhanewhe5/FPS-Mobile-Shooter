using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update


    bool isShooting = false;
    [SerializeField]FixedTouchField touchField;

    public Recoil recoilScript;

    public Animator gunAnimator;

    public int gunType = 1; // 1 = Pistol, 2 = SMG, 3 = AR, 4 = Shotgun

    public int pistolBulletStrength;
    public float pistolFireRate;
    public VisualEffect pistolMuzzleFlash;
    public int pistolBulletCount;

    public int smgBulletStrength;
    public float smgFireRate;
    public VisualEffect smgMuzzleFlash;
    public int smgBulletCount;

    public int arBulletStrength;
    public float arFireRate;
    public VisualEffect arMuzzleFlash;
    public int arBulletCount;

    public int shotgunBulletStrength;
    public float shotgunFireRate;
    public VisualEffect shotgunMuzzleFlash;
    public int shotgunBulletCount;

    public int bulletStrength;
    public int bulletCount;
    public float fireRate;

    public float nextTimeToFire = 0f;

    public VisualEffect muzzleFlash;

    public GameObject pistol;
    public GameObject smg;
    public GameObject assaultRifle;
    public GameObject shotgun;

    public float ammoAvailable;
    public pointsBar pointsBar;
    void Start()
    {
        ammoAvailable = pointsBar.points;
    }

    public void ShootButtonDown()
    {
        
        touchField.isFirstMove = true;
        isShooting = true;
    }

    public void ShootButtonUp()
    {
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            Shoot();
            touchField.isShooting = true;
        }
        else { 
            touchField.isShooting = false;
        }

        if(pistol.activeSelf)
        {
            ChangeGun(1);
        }
        else if(smg.activeSelf)
        {
            ChangeGun(2);
        }
        else if(assaultRifle.activeSelf)
        {
            ChangeGun(3);
        }
        else if(shotgun.activeSelf)
        {
            ChangeGun(4);
        }
    }

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            if(ammoAvailable > bulletCount)
            {
                ammoAvailable -= bulletCount;
                pointsBar.points = ammoAvailable;

                // Create a ray from the camera to the mouse cursor
                gunAnimator.SetTrigger("GunShoot");

                muzzleFlash.Play();
                Debug.Log("Shooting");
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, this.transform.position.z));
                RaycastHit hit;
                // Check if the ray hits something
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the object hit is an enemy
                    if (hit.collider.tag == "Enemy")
                    {
                        // Destroy the enemy
                        hit.collider.GetComponent<EnemyController>().TakeDamage(bulletStrength);
                    }
                }
                nextTimeToFire = Time.time + 1f / fireRate;
                recoilScript.RecoilFire();
            }
        }
    }

    public void ChangeGun(int gun)
    {
        gunType = gun;
        if (gunType == 1)
        {
            bulletStrength = pistolBulletStrength;
            fireRate = pistolFireRate;
            muzzleFlash = pistolMuzzleFlash;
            bulletCount = pistolBulletCount;
        }
        else if (gunType == 2)
        {
            bulletStrength = smgBulletStrength;
            fireRate = smgFireRate;
            muzzleFlash = smgMuzzleFlash;
            bulletCount = smgBulletCount;
        }
        else if (gunType == 3)
        {
            bulletStrength = arBulletStrength;
            fireRate = arFireRate;
            muzzleFlash = arMuzzleFlash;
            bulletCount = arBulletCount;
        }
        else if (gunType == 4)
        {
            bulletStrength = shotgunBulletStrength;
            fireRate = shotgunFireRate;
            muzzleFlash = shotgunMuzzleFlash;
            bulletCount = shotgunBulletCount;
        }
    }

}
