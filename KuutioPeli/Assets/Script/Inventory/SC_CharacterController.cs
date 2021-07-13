using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_CharacterController : MonoBehaviour
{
    public float speed = 6.5f;
    public float jump = 7.0f;
    public float gravity = 19.0f;
    public Camera playerCamera;
    public float lookspeed = 3.0f;
    public float lookXLimit = 55.0f;

    CharacterController characterController;

    Vector3 moveDirection = Vector3.zero;

    Vector2 rotation = Vector2.zero;


    [HideInInspector]

    public bool canMove = true;



    void Start()
    {
        characterController = GetComponent<CharacterController>();

        rotation.y = transform.eulerAngles.y;

    }

    
    void Update()
    {
        if (characterController.isGrounded)
        {
            //Grounded
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = speed * Input.GetAxis("Vertical");
            float curSpeedY = speed * Input.GetAxis("Horizontal");

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
    if (Input.GetButton("Jump"))
            {

                moveDirection.y = jumpSpeed;

            }
        }

        //Gravity, moveDirection multiplied by deltaTime

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        //Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookspeed;
            rotation.x += Input.GetAxis("Mouse Y") * lookspeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }
}
