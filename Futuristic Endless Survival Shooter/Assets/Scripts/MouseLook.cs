using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public Transform player;
    float xRotation = 0f;

    public float xMove;
    public float yMove;

    void Start()
    {
#if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }
    void Update()
    {
#if UNITY_EDITOR
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
#elif UNITY_iOS || UNITY_ANDROID
        float mouseX = joystick.Horizontal * mouseSensitivity * Time.deltaTime;
        float mouseY = joystick.Vertical * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
#endif
    }
}
