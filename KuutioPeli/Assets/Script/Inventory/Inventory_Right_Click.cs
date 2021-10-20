using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Right_Click : MonoBehaviour
{

    public GameObject kello;
    public GameObject sakset;
    public GameObject avain;
    public string RaycastReturn;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("mouseeeeee");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    RaycastReturn = hit.collider.gameObject.name;
                    Debug.Log(RaycastReturn);
                }
            }
        }
    }
}
