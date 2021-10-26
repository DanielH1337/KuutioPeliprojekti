using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl : MonoBehaviour
{
    public bool CheckBool = false;
    public int count;
    public int Target = 3;
    public GameObject targetobject;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Avain" || other.gameObject.tag == "Sakset" || other.gameObject.tag == "Kello")
        {
            targetobject = other.gameObject;
            count += 1;
            Debug.Log("count: " + count);
            Destroy(targetobject);
        }
           
    }

    void Update()
    {
        if(count == Target)
        {
            CheckBool = true;
        }

    }
}
