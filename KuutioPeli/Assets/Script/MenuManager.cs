using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuManager : MonoBehaviour
{

    public CinemachineBrain mainCamera;
    public CinemachineVirtualCamera frame0_cam;
    public CinemachineVirtualCamera frame1_cam;
    public CinemachineVirtualCamera frame2_cam;
    //public CinemachineVirtualCamera frame2_cam;

    public GameObject[] frame;
    public GameObject startButton;
    public EventSystem ES;
    public Animator transition,BadName;
    public AudioClip wooshSound;
    public AudioClip clickSound;
    public float time;
    public string nimi;


    static AudioSource AudioSrc;

    //Ensimmäinen frame asetetaan cam0 mukaisesti
    void Start()
    {
        clickSound = Resources.Load<AudioClip>("click");
        wooshSound = Resources.Load<AudioClip>("woosh");
        AudioSrc = GetComponent<AudioSource>();
        frame[0].SetActive(true);
        frame[1].SetActive(false);
        frame[2].SetActive(false);
        frame[3].SetActive(false);
        frame[4].SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && frame[0].activeInHierarchy)
        {
            Frame1();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !frame[0].activeInHierarchy)
        {
            Frame0();
        }

        
    }
    //Main menu paneelit on jaettu frameihin, niitä aktivoidaan eri kohdissa main menua.

    //Menu valikko näkymä
    public void Frame1()
    {
        AudioSrc.PlayOneShot(wooshSound);
        StartCoroutine(Framedelay1());
        ES.SetSelectedGameObject(startButton);
        frame0_cam.gameObject.SetActive(false);
        frame1_cam.gameObject.SetActive(true);
        frame2_cam.gameObject.SetActive(false);
    }
   //Press any key näkymä
    public void Frame0()
    {
        
        StartCoroutine(Framedelay0());
        frame1_cam.gameObject.SetActive(false);
        frame0_cam.gameObject.SetActive(true);
        frame2_cam.gameObject.SetActive(false);
    }
    //HighScore Näkymä
    public void Frame2()
    {
        AudioSrc.PlayOneShot(wooshSound);
        frame0_cam.gameObject.SetActive(false);
        frame1_cam.gameObject.SetActive(false);
        frame2_cam.gameObject.SetActive(true);
        frame[1].SetActive(false);
    }
    public void Frame3()
    {
        AudioSrc.PlayOneShot(wooshSound);
        StartCoroutine(Framedelay2());
        frame1_cam.gameObject.SetActive(false);
        frame0_cam.gameObject.SetActive(false);
        frame2_cam.gameObject.SetActive(true);
    }
    IEnumerator Framedelay2()
    {
        frame[1].SetActive(false);
        frame[2].SetActive(false);
        frame[3].SetActive(false);
        yield return new WaitForSeconds(2);
        frame[4].SetActive(true);
    }
    //Menu valikko viivästys
    IEnumerator Framedelay1()
    {
        frame[1].SetActive(false);
        frame[2].SetActive(false);
        frame[0].SetActive(false);
        yield return new WaitForSeconds(2);
        frame[3].SetActive(true);
        
    }
    //Press any key näkymä viivästys
    IEnumerator Framedelay0()
    {
        frame[3].SetActive(false);
        frame[2].SetActive(false);
        frame[1].SetActive(false);
        yield return new WaitForSeconds(2);
        frame[0].SetActive(true);
        
    }
    //Exit näppäin
    public void Quit()
    {
        Application.Quit();
    }
    
    //Aloitetaan ensimmäinen taso eli start nappi.
    public void LoadFirstScene()
    {
        
        nimi=PlayerPrefs.GetString("name");

        if(nimi.Length!=1&& nimi.Length < 10)
        {
            StartCoroutine(LoadLevel());
        }

        else
        {
            BadName.SetTrigger("BadName");
            Debug.Log("Nimi ei kelpaa");
        }
    }
    IEnumerator LoadLevel()
    {
        AudioSrc.PlayOneShot(wooshSound);
        transition.SetTrigger("Start");
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            SaveSystem.Deleteall();
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    //Highscore näkymä
    public void highscoreView()
    {
        frame[3].SetActive(false);
        StartCoroutine(highScoreViewDelayed());
    }
    IEnumerator highScoreViewDelayed()
    {
        yield return new WaitForSeconds(2);
        frame[2].SetActive(true);
    }
    
    //Aloitetaan tallennettu peli.
    public void loadGame()
    {
        StartCoroutine(LoadGame());
    }
    IEnumerator LoadGame()
    {
        string Path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(Path))
        {
            AudioSrc.PlayOneShot(wooshSound);
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("File not found");
        }
    }
    //Enter name näkymä, kun ollaan painettu start button
    public void StartButton()
    {
        AudioSrc.PlayOneShot(clickSound);
        frame[3].SetActive(false);
        frame[2].SetActive(false);
        frame[0].SetActive(false);
        frame[1].SetActive(true);
    }
    public void backButton()
    {
        //AudioSrc.PlayOneShot(wooshSound);
        frame[2].SetActive(false);
        Frame1();

    }
    public void settingsBackButton()
    {
        frame[4].SetActive(false);
        Frame1();
    }
    
}