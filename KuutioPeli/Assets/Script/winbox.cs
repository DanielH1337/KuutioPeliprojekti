using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winbox : MonoBehaviour
{
    public Animator transition;
    public float time;
    void Start()
    {
    
    }
    

    private void OnTriggerEnter(Collider other)
    {
        time = Player.instance.elapsedTime;
        PlayerPrefs.SetFloat("Time",time);
        StartCoroutine(loadMain());
        Player.instance.EndTimer();
        
    }
    IEnumerator loadMain()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
        SaveSystem.Deleteall();
    }
   
}
