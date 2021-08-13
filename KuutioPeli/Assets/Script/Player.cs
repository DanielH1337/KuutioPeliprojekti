using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    public int health=100;
    private float startTime;
    public int level;
    public Text Timertext;
    // Start is called before the first frame update
    void Start()
    {
        startTime=Time.time;
        level = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f0");
        Timertext.text = minutes +":"+ seconds;
    }
}
