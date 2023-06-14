using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{


    public static string currentUserName;
    public static int countScore;
    public Text createUserName;
    


    private void Start()
    {


        currentUserName = PlayerPrefs.GetString("CurrentPlayerName");
        countScore = PlayerPrefs.GetInt("score");


    }

   
    public async void SaveUsers()
    {
        var result = await GetUsers.CreateUsers(createUserName.text);

        //User Check Name
        if (result == "false")
        {
            PopUp.instence.SetTitle("Error").SetMessage("User Name Allready Taken!").Show();

        }
        else
        {
            PlayerPrefs.SetString("CurrentPlayerName", createUserName.text);
            currentUserName = PlayerPrefs.GetString("CurrentPlayerName");
            MainMenu.instance.CreateUser();
            

        }



    }
 
}