using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public static AudioClip wooshSound;
    public GameObject[] frames;
    static AudioSource AudioSrc;
    public Animator transition;
    public static AudioClip clickSound;




    // Adding tutorial to frames, where every frame shown and hidden after button press.
    void Start()
    {
        
        AudioSrc = GetComponent<AudioSource>();
        wooshSound = Resources.Load<AudioClip>("woosh");
        clickSound = Resources.Load<AudioClip>("click");
        frames[0].SetActive(true);
        frames[1].SetActive(false);
        frames[2].SetActive(false);
        frames[3].SetActive(false);
    }

    public void firstSwitch()
    {
        AudioSrc.PlayOneShot(clickSound);
        frames[0].SetActive(false);
        frames[1].SetActive(true);
        frames[2].SetActive(false);
        frames[3].SetActive(false);
    }
    public void secondSwitch()
    {
        AudioSrc.PlayOneShot(clickSound);
        frames[0].SetActive(false);
        frames[1].SetActive(false);
        frames[2].SetActive(true);
        frames[3].SetActive(false);
    }
    public void thirdSwitch()
    {
        AudioSrc.PlayOneShot(clickSound);
        frames[0].SetActive(false);
        frames[1].SetActive(false);
        frames[2].SetActive(false);
        frames[3].SetActive(true);
    }
    public void startGameSwitch()
    {
        //Starts the game
        AudioSrc.PlayOneShot(wooshSound);
        StartCoroutine(startGame());
    }
    public void fourthswitch()
    {
        AudioSrc.PlayOneShot(clickSound);
        frames[0].SetActive(false);
        frames[1].SetActive(false);
        frames[2].SetActive(false);
        frames[3].SetActive(false);
        frames[4].SetActive(true);
    }
    IEnumerator startGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

   
 
}
