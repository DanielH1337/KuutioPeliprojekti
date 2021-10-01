using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]

public class SC_CharacterController : MonoBehaviour
{
    public Camera playerCamera;
    //public float lookspeed = 3.0f;
    //public float lookXLimit = 55.0f;



    Vector2 rotation = Vector2.zero;
    //public bool canMove = true;

    void Update()
    {
       /* if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookspeed;
            rotation.x += Input.GetAxis("Mouse Y") * lookspeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
          
        }*/
    }
}
