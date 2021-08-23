using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerName : MonoBehaviour
{


    public string nameOfPlayer;
    public string saveName;

    public TMP_Text inputText;

    public TMP_Text loadedName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameOfPlayer = PlayerPrefs.GetString("name","none");
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name",saveName);
    }
}
