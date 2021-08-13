using System.Collections;
using UnityEngine;

public class RotTrig : MonoBehaviour
{
    bool trigger_safety = true;
    public Rigidbody rb;
    public float speed;
    public int rotTarget;
    float x;
    float y;
    float z;


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
                RotateCube();
                /*trigger_safety = true;
                y = y + 90;
                rb.transform.Rotate(y, x, z);*/
                for (int i = 0; i == rotTarget; i++)
                {
                    Debug.Log(i);
                    rb.transform.Rotate(Vector3.up * speed * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator RotateCube()
    {
        for (int i = 0; i == rotTarget; i++)
        {
            Debug.Log(i);
            rb.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        yield return "done";
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
