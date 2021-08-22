using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{

    public CinemachineBrain mainCamera;
    public CinemachineVirtualCamera frame0_cam;
    public CinemachineVirtualCamera frame1_cam;
    //public CinemachineVirtualCamera frame2_cam;

    public GameObject[] frame;
    public GameObject startButton;
    public EventSystem ES;
    public Animator transition;
    public static AudioClip wooshSound;

    static AudioSource AudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        wooshSound = Resources.Load<AudioClip>("woosh");
        AudioSrc = GetComponent<AudioSource>();
        frame[0].SetActive(true);
        frame[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && frame[0].activeInHierarchy)
        {
            Frame1();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !frame[0].activeInHierarchy)
        {
            Frame0();
        }
    }
    public void Frame1()
    {
        AudioSrc.PlayOneShot(wooshSound);
        StartCoroutine(Framedelay1());
        ES.SetSelectedGameObject(startButton);
        frame0_cam.gameObject.SetActive(false);
        frame1_cam.gameObject.SetActive(true);
    }
    public void Frame0()
    {
        StartCoroutine(Framedelay0());
        frame1_cam.gameObject.SetActive(false);
        frame0_cam.gameObject.SetActive(true);
    }

    IEnumerator Framedelay1()
    {
        frame[0].SetActive(false);
        yield return new WaitForSeconds(2);
        frame[1].SetActive(true);
    }
    IEnumerator Framedelay0()
    {
        frame[1].SetActive(false);
        yield return new WaitForSeconds(2);
        frame[0].SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadFirstScene()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        AudioSrc.PlayOneShot(wooshSound);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
    public void loadSavedGame()
    {
        Player.instance.LoadPlayer();
    }
}