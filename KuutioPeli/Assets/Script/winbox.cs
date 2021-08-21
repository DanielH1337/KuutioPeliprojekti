using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winbox : MonoBehaviour
{
    void Start()
    {
        BeginTime();
    }
    public void BeginTime()
    {
        Player.instance.BeginTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.instance.EndTimer();
    }
   
}
