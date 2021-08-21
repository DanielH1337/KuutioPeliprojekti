using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winbox : MonoBehaviour
{
    void Start()
    {
    
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Player.instance.EndTimer();
    }
   
}
