using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl : MonoBehaviour
{
    public bool CheckBool = false;
    public int count;
    public int Target = 3;
    public int counter=0;
    //public GameObject targetobject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            counter = 1;
            if (counter == 1)
            {

                count += 1;
                Debug.Log("count: " + count);
                counterzero();
            }
            
        }
           
    }
    private void counterzero()
    {
        counter = 0;
    }
    void Update()
    {
     /*   if(count == Target)
        {
            CheckBool = true;
        }*/

    }
}
