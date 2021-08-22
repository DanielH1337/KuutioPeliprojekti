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

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data=  SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        SceneManager.LoadScene(level);
        transform.position = position;
        
    }
    
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
