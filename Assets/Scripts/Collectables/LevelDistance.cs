using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public static LevelDistance levelInstance;
    public GameObject disDisplay;
    public GameObject endDisDisplay;
    public Text totalScore;
    public TMP_Text scoreX;
    public static int disRun;
    public bool addingDis = false;
    public float disDelay = 0.35f;
    public int countScore;
    int addScore = 1;



   



    void Start()
    {

        if(PlayerPrefs.GetInt("HighScore")<disRun)
        {
            LoadData();
        }
        else
        {
            totalScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        scoreX.text = addScore.ToString() + "X";
        disRun = 0;
        levelInstance = this;
    }
    

    void FixedUpdate()
    {

        if (addingDis == false)
        {
            addingDis = true;
            StartCoroutine(AddingDis());
        }
      


    }

    IEnumerator AddingDis()
    {


        disRun += addScore;
        disDisplay.GetComponent<Text>().text = "" + disRun;
        endDisDisplay.GetComponent<Text>().text = "" + disRun;
        if (disRun / 250 > addScore - 1 && disRun < 1000)
        {
            addScore++;
            Player.playerSpeed += 2f;
            scoreX.text = addScore.ToString() + "X";
        }
        yield return new WaitForSeconds(disDelay);
        addingDis = false;



    }
   



    public void SaveDataGame()
    {
        countScore = disRun + PlayerPrefs.GetInt("score");
        PlayerPrefs.SetInt("score", countScore);
        
        
    }
    void LoadData()
    {
        PlayerPrefs.SetInt("HighScore", disRun);
        totalScore.text = PlayerPrefs.GetInt("HighScore").ToString();


    }

}
