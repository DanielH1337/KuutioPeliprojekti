using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighScoreTable : MonoBehaviour
{
    public bool ClearList=false;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreentryTransformList;
    public int maxScoreBoardEntries = 10;
    private string pname;
    private float time;
    bool nullNameTime;
    private void Awake()
    {
 

        entryContainer = transform.Find("highScoreEmptyContainer");
        entryTemplate = entryContainer.Find("highScoreEmptyTemplate");

        entryTemplate.gameObject.SetActive(false);

        if (ClearList == true)
        {
            Clearlist();
        }
        pname = PlayerPrefs.GetString("name");
        time = PlayerPrefs.GetFloat("Time");
        Debug.Log(pname);
        Debug.Log(time);

        if (time == 0)
        {
            nullNameTime = false;
        }
        else
        {
            nullNameTime = true;
        }

        if (nullNameTime == true)
        {
            AddHighscoreEntry(time, pname);
            PlayerPrefs.DeleteKey("name");
            PlayerPrefs.DeleteKey("Time");

      
        }



        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        if (highscores == null)
        {
            // There's no stored table, initialize
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(5, "player");
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<HighScores>(jsonString);
        }
    

       

        // tulosten järjestäminen
        for (int i =0;i<highscores.highScoreEntryList.Count; i++)
        {
            for(int j=i+1;j < highscores.highScoreEntryList.Count; j++)
            {
                if (highscores.highScoreEntryList[j].score < highscores.highScoreEntryList[i].score)
                {
                    //Vaihto
                    HighScoreEntry tmp = highscores.highScoreEntryList[i];
                    highscores.highScoreEntryList[i] = highscores.highScoreEntryList[j];
                    highscores.highScoreEntryList[j] = tmp;
                   
                }
            }
        }

        highscoreentryTransformList = new List<Transform>();
        foreach(HighScoreEntry highScoreEntry in highscores.highScoreEntryList)
        {

            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreentryTransformList);
        }
        
    }
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry,Transform container,List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "th"; break;
            case 1: rankString = "1st"; break;
            case 2: rankString = "2nd"; break;
            case 3: rankString = "3rd"; break;
        }
        
        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rankString;
        float score = highScoreEntry.score;
        string scoreString;
        float minutes = score / 60;
        float seconds =  score%60;
        switch (score)
        {
            default:
                scoreString =minutes.ToString("f0")+ ":"+seconds.ToString("f0");break;
        }
        entryTransform.Find("scoreText").GetComponent<TMP_Text>().text=scoreString;
        string name = highScoreEntry.name;
        entryTransform.Find("nameText").GetComponent<TMP_Text>().text = name;
        

        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
        if (rank ==1)
        {
            entryTransform.Find("posText").GetComponent<TMP_Text>().color = Color.red;
            entryTransform.Find("scoreText").GetComponent<TMP_Text>().color = Color.red;
            entryTransform.Find("nameText").GetComponent<TMP_Text>().color = Color.red;
        }
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(float score, string name)
    {

        //Luodaan HighScore Entry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        //Ladataan highscoret
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highscores == null)
        {
            //Tyjhä pöytä
            highscores = new HighScores()
            {
                highScoreEntryList = new List<HighScoreEntry>()
            };
        }

        bool scoreAdded = false;

        for(int i = 0; i < highscores.highScoreEntryList.Count;i++)
        {
            if (highScoreEntry.score < highscores.highScoreEntryList[i].score)
            {
                highscores.highScoreEntryList.Insert(i, highScoreEntry);
                scoreAdded = true;
                break;
            }
        }
        if (!scoreAdded && highscores.highScoreEntryList.Count < maxScoreBoardEntries)
        {
            highscores.highScoreEntryList.Add(highScoreEntry);
        }
        if (highscores.highScoreEntryList.Count> maxScoreBoardEntries)
        {
            highscores.highScoreEntryList.RemoveRange(maxScoreBoardEntries, highscores.highScoreEntryList.Count - maxScoreBoardEntries);
        }
        //tallennetaan highscoret
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

   
    }
    
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }


    [System.Serializable]
    private class HighScoreEntry
    {
        public float score;
        public string name;
    }
    private void Clearlist()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);
        highscores.highScoreEntryList.Clear();
        PlayerPrefs.DeleteAll();
    }


}
