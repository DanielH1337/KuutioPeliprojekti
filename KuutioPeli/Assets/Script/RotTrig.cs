using System.Collections;
using UnityEngine;

public class RotTrig : MonoBehaviour
{
    bool trigger_safety = true;
    public Rigidbody rb;
    [Header("for 90 turn in 2s = speed=.5 rT=90 multiply=2")]
    public float speed;
    public float rotTarget;
    public float multiplyBy;
    float x;
    float y;
    float z;   
    private bool b;
    public Vector3 CubeVects;

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
        if (other.gameObject.tag == "Player" && trigger_safety == false)
        {
            if (Input.GetKey("e"))
            {
                Debug.Log("success");
                if (b == false)
                {
                    StartCoroutine(RotateCube());
                }                                
            }
        }
    }

    IEnumerator RotateCube()
    {
        Debug.Log("Rotate ON");
        Debug.Log(rotTarget);
        b = true;        
        for (float i = 0; i <= multiplyBy*rotTarget-1; i++)
        {
            Debug.Log("i");
            rb.transform.Rotate(CubeVects * speed);
            Debug.Log(rb.position.x);
            yield return null;
        }
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
