using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float XMove;
    public float YMove;
    private float XRotation;
    [SerializeField] private Transform PlayerBody;
    public Vector2 LockAxis;
    public float Sensivity = 40f;
    void Start()
    {
        
    }

    
    void Update()
    {
        XMove = LockAxis.x * Sensivity * Time.deltaTime;
        YMove = LockAxis.y * Sensivity * Time.deltaTime;
        XRotation -= YMove;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation,0,0);
        PlayerBody.Rotate(Vector3.up * XMove);
    }
}
