using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class Player : MonoBehaviour
{
    public static Player instance;
    private TimeSpan timePlaying;
    public int health=100;
    
    public int level;
    public Text Timertext;

    private float elapsedTime;
    private bool timerGoing;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        Timertext.text = "Time:00:00.00";
        timerGoing =false;
        level = SceneManager.GetActiveScene().buildIndex;
  
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timerGoing = false;
        Timertext.color = Color.red;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayinghStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            Timertext.text = timePlayinghStr;

            yield return null;
        }
    }
    
}
