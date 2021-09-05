using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerName : MonoBehaviour
{


    //public string nameOfPlayer;
    public string saveName;

    public TMP_Text inputText;

 

    public void SetName()
    {

        saveName = inputText.text;
              
        //Debug.Log("Nimi ei kelpaa");

        PlayerPrefs.SetString("name", saveName);
        
    }
}
