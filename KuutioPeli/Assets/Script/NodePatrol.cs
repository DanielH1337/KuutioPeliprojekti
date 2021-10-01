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
            Vector3 targetpos = waypoints[current].transform.position;
            targetpos.x = targetpos.x - ToBeMoved.transform.position.x;
            targetpos.z = targetpos.z - ToBeMoved.transform.position.z;
            float angle = Mathf.Atan2(targetpos.x, targetpos.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle - 90, 0));
            ToBeMoved.transform.position = Vector3.MoveTowards(ToBeMoved.transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        }
        if (chase == true && ChaseFunc == true) // new
        {
            Vector3 OwnPos = transform.position;
            Vector3 TargetPos = new Vector3(playerobj.transform.position.x, transform.position.y , playerobj.transform.position.z );
            //Debug.Log("happens3");            

            Vector3 ownpos = transform.position;
            Vector3 targetpos = playerobj.transform.position;
            targetpos.x = targetpos.x - ownpos.x;
            targetpos.z = targetpos.z - ownpos.z;
            float angle = Mathf.Atan2(targetpos.x, targetpos.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0,angle - 90f,0)) ;


            transform.position = Vector3.MoveTowards(OwnPos, TargetPos, Time.deltaTime * speed);
        }
        /*if ( chase == true && ChaseFunc == true) // old
        {
            Debug.Log("happens3");
            transform.position = Vector3.MoveTowards(transform.position, playerobj.transform.position, Time.deltaTime * speed);
        }*/

        /*if (chase == true && ChaseFunc == true) // avoids player
        {
            Vector3 OwnPos = transform.position;
            Vector3 TargetPos = new Vector3(playerobj.transform.position.x, playerobj.transform.position.y);
            Debug.Log("happens3");
            transform.position = Vector3.MoveTowards(OwnPos, TargetPos, Time.deltaTime * speed);
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(Physics.Linecast(transform.position, playerobj.transform.position, out RaycastHit hitInfo))
            {
                Debug.Log(hitInfo.collider);
                if (hitInfo.collider == playerobj.GetComponent<Collider>())
                {
                    Debug.Log(hitInfo);
                    chase = true;
                }
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            chase = false;
        }
    }
}
