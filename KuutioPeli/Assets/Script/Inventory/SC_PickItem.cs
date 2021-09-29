using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PickItem : MonoBehaviour
{
    public string itemName = "Cylinder"; //Unique name for all items
    public Texture itemPreview;
    Rigidbody rb;
    public bool flip;
   

    void Start()
    {
        //Change item tag to Respawn to detect when we look at it
        gameObject.tag = "Respawn";
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

    }
  

    public void PickItem()
    {
        Destroy(gameObject);
    }
    //Flip gravity from gameobjects

    private void Update()
    {
        if (flip==true)
        {
          
            rb.AddForce(Vector3.up * 9.81f, ForceMode.Acceleration);
           
        }
       if (flip==false)
        {
            
            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
        }

    }
}
