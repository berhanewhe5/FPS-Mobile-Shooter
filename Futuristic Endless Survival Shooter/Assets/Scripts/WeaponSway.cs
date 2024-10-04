using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;

    [SerializeField] CameraLook cameraLook;

    // Update is called once per frame
    void Update()
    {
        float lookX = cameraLook.XMove * swayMultiplier;
        float lookY = cameraLook.YMove * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-lookY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(lookX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
