
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

// Delete SERT
//public class CertificateWhore : CertificateHandler
//{
//    protected override bool ValidateCertificate(byte[] certificateData)
//    {
//        return true;
//    }
//}

public class GetUsers : MonoBehaviour
{
    public static GetUsers instance;

    // Rank User Name Scores and list 
    public static List<Users> userList;
    public Text[] rNames;
    public Text[] rScores;

    // Current user Name and Score
    public Text rankUserName;
    public Text rankUserScore;

    //Search User Name And Score
    public InputField searchUserNameInput;
    public Text searchUserNameText;
    public Text searchUserScoreText;



    void Start()
    {
        instance = this;

    }

    //Update User Score
    public async void UploadScore()  //CALLED when Uploading new Score to WEBSITE
    {
        var url = $"http://104.40.246.13/Runner/api/User?userName={PlayerPrefs.GetString("CurrentPlayerName")}&score={PlayerPrefs.GetInt("score")}";
        await DatabaseController.Put<string>(url, null);
    }

    // Create User
    public static async Task<string> CreateUsers(string username)  //CALLED when Uploading new Score to WEBSITE
    {
        var url = "http://104.40.246.13/Runner/api/User?userName=" + username;
        return await DatabaseController.Post<string>(url, null);

    }
    // Get User Rank Ten
    public async void GetUserRank()
    {
        var url = "http://104.40.246.13/Runner/api/User";

        if (PlayerPrefs.HasKey("CurrentPlayerName"))
        {
            var userRankUrl = "http://104.40.246.13/Runner/api/User/id?userName=" + PlayerPrefs.GetString("CurrentPlayerName");
            string currentUserRank = await DatabaseController.Get<string>(userRankUrl);
            rankUserName.text = currentUserRank + ". " + PlayerPrefs.GetString("CurrentPlayerName");
            rankUserScore.text = PlayerPrefs.GetInt("score").ToString();
        }
        else
        {
            rankUserName.text = "No User";
            rankUserScore.text = "No User";
        }
        userList = await DatabaseController.Get<List<Users>>(url);
        for (int i = 0; i < userList.Count; i++)
        {
            rNames[i].text = (i + 1).ToString() + ". " + userList[i].userName;
            rScores[i].text = userList[i].userScore.ToString();
        }

    }


    // search User Name And Score   
    public async void SearchUser()
    {
        if (String.IsNullOrEmpty(searchUserNameInput.text))
        {
            PopUp.instence.SetTitle("Error").SetMessage("Enter a User Name").Show();
        }
        else
        {

            var userRankUrl = "http://104.40.246.13/Runner/api/User/name?userName=" + searchUserNameInput.text;
            var deneme = await DatabaseController.Get<List<SearchUserData>>(userRankUrl);
          
            if (deneme == null)
            {
                PopUp.instence.SetTitle("Error").SetMessage("User Not Found").Show();
            }
            else
            {
                searchUserNameText.text = deneme[0].rank.ToString() + ". " + deneme[0].userName;
                searchUserScoreText.text = deneme[0].userScore.ToString();
            }


        }


    }
}
