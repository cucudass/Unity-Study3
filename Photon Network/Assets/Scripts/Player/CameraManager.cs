using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float cameraRotationLimit = 60.0f;
    [SerializeField] float currentRotationX;
    [SerializeField] float sensitivity = 5.0f;
    [SerializeField] float scrollSpeed = 100.0f;

    void Start()
    {
        
    }

    void Update()
    {
        RotateCamera();
    }

    public void RotateCamera() {
        float xRotation = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRotation * sensitivity;

        currentRotationX -= cameraRotationX; //마우스가 가는 방향
        currentRotationX = Mathf.Clamp(currentRotationX, -cameraRotationLimit, cameraRotationLimit);//회전 범위

        transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
    }
}
