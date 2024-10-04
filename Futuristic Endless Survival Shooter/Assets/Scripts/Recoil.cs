using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    //Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    //Hipfire Recoil
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    //Settings
    [SerializeField] private float snappiness;
    [SerializeField] private float resetSpeed;

    public float bulletStrength;
    public float multiplier;

    [SerializeField] PlayerShooting playerShooting;
    void Start()
    {
        
    }

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, resetSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);


    }
    public void RecoilFire() { 
        bulletStrength = playerShooting.bulletStrength;

        targetRotation += new Vector3(recoilX*bulletStrength*multiplier, Random.Range(-recoilY, recoilY) * bulletStrength * multiplier, Random.Range(-recoilZ, recoilZ) * bulletStrength * multiplier);
    }
}
