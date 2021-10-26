using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyCollider : MonoBehaviour
{
    
    public Animator transition;



    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "coll")
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            {
                Debug.Log("Hit");
                Player.instance.YouDie.SetActive(true);
                Player.instance.EndTimer();
                Player.instance.FreezePosition();
                StartCoroutine(loadmain());
            }
            IEnumerator loadmain()
            {
                SaveSystem.Deleteall();
                yield return new WaitForSeconds(2);
                transition.SetTrigger("Start");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(0);
            }
        }
    }


}
