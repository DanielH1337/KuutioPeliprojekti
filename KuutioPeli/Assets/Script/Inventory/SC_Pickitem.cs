using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PickItem : MonoBehaviour
{
    public string itemName = "Cylinder"; //Unique name for all items
    public Texture itemPreview;

    void Start()
    {
        //Change item tag to Respawn to detect when we look at it
        gameObject.tag = "Respawn";
    }

    public void PickItem()
    {
        Destroy(gameObject);
    }
}
