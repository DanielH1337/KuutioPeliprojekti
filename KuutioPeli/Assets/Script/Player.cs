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

    public float gravity;
    public CharacterController control;
    public static Player instance;
    public TimeSpan timePlaying;
    public int health=100;
    public TMP_Text Timertext;
    public float elapsedTime;
    private bool timerGoing;
    public string playerName;
    public TMP_Text pName;
    public Animator transition,savedgame;
    public GameObject PauseMenu,TabKeyText,YouDie;
    private int buttonclick=0;
    static AudioSource AudioSrc;
    public static AudioClip wooshSound;
    public static AudioClip clicksound;
    public GameObject worldCube;
    public float timer;
    public float intensity;
    public int YouDieTrig;
   


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
    //Funktio joka pys‰ytt‰‰ pelaajan, kun osutaan winboxiin
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
            gravity = data.gravity;
            timerGoing = false;
            elapsedTime = data.elapsedTime;
            timerGoing = true;
            Debug.Log(elapsedTime);

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
            Debug.Log(transform.position);

            Quaternion CharPos;
            CharPos.x = data.CharPos[0];
            CharPos.y = data.CharPos[1];
            CharPos.z = data.CharPos[2];
            CharPos.w = data.CharPos[3];
            transform.rotation = CharPos;
           

            Quaternion CubePos;
            CubePos.x = data.CubePos[0];
            CubePos.y = data.CubePos[1];
            CubePos.z = data.CubePos[2];
            CubePos.w = data.CubePos[3];
            worldCube.transform.rotation= CubePos;
            control.enabled = true;
        }
        else
        {
            Debug.Log("File not found");
        }

    }
    
    
    //We Make this object a singleton
    private void Awake()
    {
        instance = this;
        
        
    }
    IEnumerator YouDieDelay()
    {
        control.enabled = false;
        yield return new WaitForSeconds(2);
        LoadMain();

    }
    private void Update()
    {
        //When time reaches zero we launch YouDie view
        if (elapsedTime <= 0)
        {
            
            if (YouDieTrig == 0)
            {
                YouDieTrig += 1;
                EndTimer();
                YouDie.SetActive(true);
                StartCoroutine(YouDieDelay());
                
            }
        }
        
        //At the last minute the timer flashes red and white
        if (elapsedTime< 60f)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                Timertext.color = Color.red;
            }
            if (timer >= 2)
            {
                Timertext.color = Color.white;
                timer = 0;
            }

        }
        gravity = GetComponent<Movement>().gravity;

        //Show and hide pause menu
        if (buttonclick == 2)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            TabKeyText.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            buttonclick = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
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
        YouDieTrig = 0;
        GameObject.FindWithTag("Player");
        Debug.Log(gravity);
        Debug.Log(worldCube.transform.position);
        clicksound = Resources.Load<AudioClip>("click");
        wooshSound = Resources.Load<AudioClip>("woosh");
        AudioSrc = GetComponent<AudioSource>();
        YouDie.SetActive(false);
        PauseMenu.SetActive(false);
        Timertext.text = "Time:00:00.00";
        BeginTimer();
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
        elapsedTime = 180f;

        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timerGoing = false;
        Timertext.color = Color.red;
    }
   
    //Timer function for updating timer
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayinghStr = "Time: " + timePlaying.ToString("mm':'ss");
            Timertext.text = timePlayinghStr;
            //GlitchEffect.flipIntensity = 1f / elapsedTime;
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
        gravity = data.gravity;
        timerGoing = false;
        elapsedTime = data.elapsedTime;
        timerGoing = true;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        control.enabled = true;
        Quaternion CubePos;
        CubePos.x = data.CubePos[0];
        CubePos.y = data.CubePos[1];
        CubePos.z = data.CubePos[2];
        CubePos.w = data.CubePos[3];
        Quaternion CharPos;
        CharPos.x = data.CharPos[0];
        CharPos.y = data.CharPos[1];
        CharPos.z = data.CharPos[2];
        CharPos.w = data.CharPos[3];
        transform.rotation = CharPos;
        worldCube.transform.rotation = CubePos;
       
        transition.SetTrigger("End");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

}
