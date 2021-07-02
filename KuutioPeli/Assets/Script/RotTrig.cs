using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotTrig : MonoBehaviour
{
    bool trigger_safety = true;
    public Rigidbody rb;
    float x;
    float y;
    float z;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && trigger_safety == false )
        {
            if (Input.GetKey("e"))
            {
                Debug.Log("success");
                trigger_safety = true;
                y = y + 90;
                rb.transform.Rotate(y, x, z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        trigger_safety = false;
    }

    private void OnTriggerExit(Collider other)
    {
        trigger_safety = true;
    }

}
