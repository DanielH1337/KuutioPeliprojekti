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




    // Adding tutorial to frames, where every frame shown and hidden after button press.
    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();
        wooshSound = Resources.Load<AudioClip>("woosh");
        frames[0].SetActive(true);
        frames[1].SetActive(false);
        frames[2].SetActive(false);
        frames[3].SetActive(false);
    }

    public void firstSwitch()
    {
        AudioSrc.PlayOneShot(wooshSound);
        frames[0].SetActive(false);
        frames[1].SetActive(true);
        frames[2].SetActive(false);
        frames[3].SetActive(false);
    }
    public void secondSwitch()
    {
        AudioSrc.PlayOneShot(wooshSound);
        frames[0].SetActive(false);
        frames[1].SetActive(false);
        frames[2].SetActive(true);
        frames[3].SetActive(false);
    }
    public void thirdSwitch()
    {
        AudioSrc.PlayOneShot(wooshSound);
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
    IEnumerator startGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }

   
 
}
