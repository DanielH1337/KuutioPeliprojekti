using System.Collections;
using UnityEngine;

public class RotTrig : MonoBehaviour
{
    bool trigger_safety = true;
    public Rigidbody rb;
    public GameObject player;
    [Header("for 90 turn in 2s = speed=.5 rT=90 multiply=2")]
    public float speed;
    public float rotTarget;
    public float multiplyBy;
    float x;
    float y;
    float z;   
    private bool b;
    public Vector3 CubeVects;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && trigger_safety == false)
        {
            if (Input.GetKey("e"))
            {
                Debug.Log("success");
                if (b == false)
                {
                    player.GetComponent<Movement>().ReverseGravity();
                    //player.GetComponent<Movement>().enabled = false;
                    //gameObject.GetComponent<NodePatrol>().enabled = true;
                    StartCoroutine(RotateCube());
                }                                
            }
        }
    }

    IEnumerator RotateCube()
    {
        //Debug.Log("Rotate ON");
        //Debug.Log(rotTarget);
        b = true;
        for (float i = 0; i <= multiplyBy * rotTarget - 1; i++)
        {
            //Debug.Log("i");
            rb.transform.Rotate(CubeVects * speed);
            //Debug.Log(rb.position.x);
            yield return null;
        }
        //gameObject.GetComponent<NodePatrol>().enabled = false;
        b = false;
        
        
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
