using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollectableControl : MonoBehaviour
{
    public static CollectableControl collectInstance;
    public static int coinCount;
    int doubleCoinCount;
    public static int totalCoinScore;
    public GameObject coinCountDisplay;
    public GameObject coinEndDisplay;
    public Button rewardButton;
    public Text totalCoin;
    public Text x2RewardCoin;
    private void Start()
    {
        totalCoinScore = PlayerPrefs.GetInt("coinScore");
        collectInstance = this;
        coinCount = 0;
        LoadData();
    }

    void Update()
    {
        coinCountDisplay.GetComponent<Text>().text = "" + coinCount;
        coinEndDisplay.GetComponent<Text>().text = "" + coinCount;

    }

    public void RewardedCoinEarn()
    {
        doubleCoinCount = (2 * coinCount);
        totalCoinScore = doubleCoinCount + int.Parse(totalCoin.text);
        x2RewardCoin.text = doubleCoinCount.ToString();
        x2RewardCoin.gameObject.SetActive(true);
        coinEndDisplay.gameObject.SetActive(false);
        rewardButton.gameObject.SetActive(false);
        PlayerPrefs.SetInt("coinScore", totalCoinScore);
    }

    public void SaveData()
    {
        if (doubleCoinCount / 2 != coinCount)
        {
            totalCoinScore = coinCount + int.Parse(totalCoin.text);
            PlayerPrefs.SetInt("coinScore", totalCoinScore);
        }




    }
    void LoadData()
    {
        totalCoin.text = PlayerPrefs.GetInt("coinScore").ToString();
    }
}
