using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=_QajrabyTJc


    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float JumpS = 3;
    public float maxpeed;
    Vector3 velocity;
    public int health = 100;
    public int level = 1;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        gravity = data.gravity;
    }

    private void OnDisable()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpS * -1 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        if (velocity.y <= maxpeed)
        {
           // Debug.Log("maxpeed reached");
            velocity.y = maxpeed;
        }
        else if (velocity.y >= -maxpeed)
        {
            //Debug.Log("maxpeed reached");
            velocity.y = -maxpeed;
        }        
        controller.Move(velocity * Time.deltaTime);

    }

    public void ReverseGravity()
    {
        Debug.Log("gravity Reversed");
        gameObject.transform.Rotate(180, 0, 0);
        /*
        for (float i = 0; i <= 179; i++)
        {
            Debug.Log("loop");
            gameObject.transform.Rotate(1, 0, 0);
        }
        */
        gravity = -gravity;
        JumpS = -JumpS;

    }
    public void loadGravity()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        gravity = data.gravity;
    }
}
