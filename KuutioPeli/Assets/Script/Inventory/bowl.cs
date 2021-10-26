using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl : MonoBehaviour
{
    public bool CheckBool = false;
    public int count;
    public int Target = 3;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            count += 1;
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
