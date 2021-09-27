using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Pickitem : MonoBehaviour
{
    public string itemName = "Some Item"; //Unique name for all items
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
