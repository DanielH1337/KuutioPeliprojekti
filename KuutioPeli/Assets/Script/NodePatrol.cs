using UnityEngine;

public class NodePatrol : MonoBehaviour
{
    public Transform ToBeMoved;
    public GameObject[] waypoints;
    public bool ChaseFunc;
    public bool Loop;
    int current = 0;
    float rotspeed;
    public float speed;
    float wPradius = 1;
    bool chase = false;
    public GameObject playerobj;

    private void TryEnableMouseLook(bool b)
    {
        MouseLook look = ToBeMoved.GetComponentInChildren<MouseLook>();
        if (look != null)
        {
            look.enabled = b;
        }
    }

    private void OnEnable()
    {
        TryEnableMouseLook(false);
    }

    private void OnDisable()
    {
        Movement mov = ToBeMoved.GetComponent<Movement>();
        if (mov != null)
        {
            mov.enabled = true;
        }
        TryEnableMouseLook(true);
    }

    private void Update()
    {
       // Debug.Log("Node patrol functions" + current);

        if (chase == false && Loop == false)
        {
           // Debug.Log("happens1");
            if (Vector3.Distance(waypoints[current].transform.position, ToBeMoved.transform.position) < wPradius)
            {
                current++;
                if (current == waypoints.Length)
                {
                    current = 0;
                    enabled = false;
                    
                }
            }
            ToBeMoved.transform.position = Vector3.MoveTowards(ToBeMoved.transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }
        if (chase == false && Loop == true)
        {
            Debug.Log("happens2");
            if (Vector3.Distance(waypoints[current].transform.position, ToBeMoved.transform.position) < wPradius)
            {
                current++;
                if (current >= waypoints.Length)
                {
                    current = 0;
                }
            }
            ToBeMoved.transform.position = Vector3.MoveTowards(ToBeMoved.transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }
        if ( chase == true && ChaseFunc == true)
        {
            Debug.Log("happens3");
            transform.position = Vector3.MoveTowards(transform.position, playerobj.transform.position, Time.deltaTime * speed);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chase = false;
        }
    }
}
