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
    public Animator transition,savedgame;
    public GameObject PauseMenu,TabKeyText;
    private int buttonclick=0;
    static AudioSource AudioSrc;
    public static AudioClip wooshSound;
    public static AudioClip clicksound;
    public GameObject worldCube;


    //Tallennus funktio
    public void SavePlayer()
    {
        AudioSrc.PlayOneShot(wooshSound);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenu.SetActive(false);
        SaveSystem.SavePlayer(this);
        Time.timeScale = 1;
        buttonclick = 0;
        savedgame.SetTrigger("SavedGame");
    }
    //Funktio joka pys�ytt�� pelaajan, kun osutaan winboxiin
    public void FreezePosition()
    {
        control.enabled = (false);
        
    }

    //Load funktio
    public void LoadPlayer()
    {
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            control.enabled = false;
            PlayerData data = SaveSystem.LoadPlayer();
            level = data.level;
            health = data.health;
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

            Vector3 CubePos;
            CubePos.x = data.CubePos[0];
            CubePos.y = data.CubePos[1];
            CubePos.z = data.CubePos[2];
            worldCube.transform.position = CubePos;
        }
        else
        {
            Debug.Log("File not found");
        }

    }
    
    
    //Tehd��n t�st� objektista singleton
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        //Pausemenu n�kyviin ja piiloon painamalla tab n�pp�int�
        if (buttonclick == 2)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            TabKeyText.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            buttonclick = 0;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AudioSrc.PlayOneShot(clicksound);
            buttonclick += 1;
            PauseMenu.SetActive(true);
            TabKeyText.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        clicksound = Resources.Load<AudioClip>("click");
        wooshSound = Resources.Load<AudioClip>("woosh");
        AudioSrc = GetComponent<AudioSource>();
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
    //Ajastimen Funktio
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayinghStr = "Time: " + timePlaying.ToString("mm':'ss");
            Timertext.text = timePlayinghStr;

            yield return null;
        }
    }
    public void LoadMain()
    {
        AudioSrc.PlayOneShot(wooshSound);
        timerGoing = false;
        Time.timeScale = 1;
        StartCoroutine(loadMain());
    }
   IEnumerator loadMain()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
    //Load funktio jos valitaan load pausemenusta
   public void LoadButtonFade()
    {
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            AudioSrc.PlayOneShot(wooshSound);
            PauseMenu.SetActive(false);
            StartCoroutine(Loadfade());
            buttonclick = 0;
        }
        else
        {
            Debug.Log("Save not found");
        }
    }
    IEnumerator Loadfade()
    {
        control.enabled = false;
        Time.timeScale = 1;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;
        timerGoing = false;
        elapsedTime = data.elapsedTime;
        timerGoing = true;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        Vector3 CubePos;
        CubePos.x = data.CubePos[0];
        CubePos.y = data.CubePos[1];
        CubePos.z = data.CubePos[2];
        worldCube.transform.position = CubePos;
        control.enabled = true;

        transition.SetTrigger("End");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    
 
}
