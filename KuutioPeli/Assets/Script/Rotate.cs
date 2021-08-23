using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public Vector3 Trotation;
    private Vector3 own;
    public float RotSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
