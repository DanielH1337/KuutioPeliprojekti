using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PickItem : MonoBehaviour
{
    public string itemName = "Cube"; //Unique name for all items
    public Texture itemPreview;

    void Start()
    {
        gameObject.tag = "Respawn";
    }

    public void PickItem()
    {
        Destroy(gameObject);
    }
}
