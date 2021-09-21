using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float MouseSensitivity = 100f;
    //public Transform playerBody;
    float xRotation = 0f;
    float yRotation = 0f;
        
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
        
    void FixedUpdate()
    {
        
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation -= -mouseX;
       // yRotation = Mathf.Clamp(yRotation, 360, 360);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        //playerBody.Rotate(Vector3.up * mouseX);      
    }
}
