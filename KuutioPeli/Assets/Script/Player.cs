using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using System.IO;
public class Player : MonoBehaviour
{
    public CharacterController control;
    public static Player instance;
    public TimeSpan timePlaying;
    public int health=100;
    public int level;
    public TMP_Text Timertext;
    public float elapsedTime;
    private bool timerGoing;
    public string playerName;
    public TMP_Text pName;
    public Animator transition;
    public GameObject PauseMenu,TabKeyText;
    private int buttonclick=0;
    
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            control.enabled = false;
            PlayerData data = SaveSystem.LoadPlayer();
            level = data.level;
            health = data.health;
           /* if (timerGoing == false)
            {
                Timertext.color = Color.white;
                timerGoing = true;
                StartCoroutine(UpdateTimer());
            }*/
            timerGoing = false;
            elapsedTime = data.elapsedTime;
            timerGoing = true;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
            Debug.Log(transform.position);
            control.enabled = true;
            
           
        }
        else
        {
            Debug.Log("File not found");
        }
        

    }
    
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (buttonclick == 2)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            TabKeyText.SetActive(true);
            buttonclick = 0;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            buttonclick += 1;
            PauseMenu.SetActive(true);
            TabKeyText.SetActive(false);
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        PauseMenu.SetActive(false);
        Timertext.text = "Time:00:00.00";
        timerGoing =false;
        level = SceneManager.GetActiveScene().buildIndex;
        playerName = PlayerPrefs.GetString("name");
    
        pName.text = playerName;
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            LoadPlayer();
        }
        else
        {
            return;
        }
           
       

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
    public void LoadMain()
    {
        StartCoroutine(loadMain());
    }
   IEnumerator loadMain()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
    
   
}
