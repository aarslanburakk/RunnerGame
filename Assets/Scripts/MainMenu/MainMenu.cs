using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject levelOne;
    public GameObject buyLevelOne;
    public GameObject buyLevelTwo;
    public GameObject levelTwo;
    public GameObject levelThree;
    public GameObject registerUser;
    public GameObject levelMenu;
    public GameObject showToastMenu;
    public GameObject redPotion;
    public Button quitButton;
    public Button buyPotionButton;
    public Text coin;
    public Text name;
    public Text score;
    public TMP_Text toastMessage;
    public TMP_Text upgradeHealthBar;
    public TMP_Text upgradeHealthBarCoin;
    public Image[] currentBuyPotionImage;
    public Image[] currentBuyHealthImage;
    public bool isOpenLevel = false;
    public bool isOpenLevelTwo = false;
    public bool createUser = false;
    string openedGame = "True";
    int levelOneCoin = 500;
    int levelTwoCoin = 1000;
    int buyCoinCount = 0;




    public static MainMenu instance;

    private void Start()
    {
        instance = this;

       
        Application.targetFrameRate = 60;
        coin.text = PlayerPrefs.GetInt("coinScore").ToString();
        if (PlayerPrefs.GetString("Buygame") == openedGame)
        {
            buyLevelOne.SetActive(isOpenLevel);
            isOpenLevel = true;

        }
        if (PlayerPrefs.GetString("LevelTwo") == openedGame)
        {
            buyLevelTwo.SetActive(isOpenLevelTwo);
            isOpenLevelTwo = true;
        }
        if (PlayerPrefs.GetInt("HealthBar") == 0)
        {
            HealthBar.hitPoint = 100;
            HealthBar.maxHitPoint = 100;
            PlayerPrefs.SetInt("HealthBar", 100);
            PlayerPrefs.SetInt("HealthBarUpgradeCoin", 2500);
        }
        else
        {
            HealthBar.hitPoint = PlayerPrefs.GetInt("HealthBar");
            HealthBar.maxHitPoint = PlayerPrefs.GetInt("HealthBar");
            for (int i = 0; i < PlayerPrefs.GetInt("HealthBarImage"); i++)
            {
                currentBuyHealthImage[i].color = new Color(255, 255, 255);
            }
        }
        if (PlayerPrefs.GetInt("HealthBarUpgradeCoin") == 40000)
        {
            upgradeHealthBarCoin.text = "MAX";
        }
        else
        {
            upgradeHealthBarCoin.text = PlayerPrefs.GetInt("HealthBarUpgradeCoin").ToString();
        }
        upgradeHealthBar.text = PlayerPrefs.GetInt("HealthBar").ToString();
      



        levelOne.SetActive(isOpenLevel);
        levelTwo.SetActive(isOpenLevelTwo);
        quitButton.onClick.AddListener(QuitGame);
        CreatedUser();

    }

    public void PlayLevelOne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void CreatedUser()
    {
        name.text = PlayerPrefs.GetString("CurrentPlayerName");
        score.text = SaveData.countScore.ToString();
    }

    public void OpenLevelOne()
    {
        if (PlayerPrefs.GetInt("coinScore") >= levelOneCoin)
        {
            levelOne.SetActive(true);
            buyLevelOne.SetActive(false);
            CollectableControl.totalCoinScore = PlayerPrefs.GetInt("coinScore") - levelOneCoin;
            PlayerPrefs.SetInt("coinScore", CollectableControl.totalCoinScore);
            coin.text = CollectableControl.totalCoinScore.ToString();
            PlayerPrefs.SetString("Buygame", openedGame);

        }

    }
    public void OpenLevelTwo()
    {
        if (PlayerPrefs.GetInt("coinScore") >= levelTwoCoin)
        {
            levelTwo.SetActive(true);
            buyLevelTwo.SetActive(false);
            CollectableControl.totalCoinScore = PlayerPrefs.GetInt("coinScore") - levelTwoCoin;
            PlayerPrefs.SetInt("coinScore", CollectableControl.totalCoinScore);
            coin.text = CollectableControl.totalCoinScore.ToString();
            PlayerPrefs.SetString("LevelTwo", openedGame);

        }

    }
    public void SellectLevel()
    {
        if (PlayerPrefs.GetString("CreateUser") == openedGame)
        {
            registerUser.SetActive(false);
            createUser = true;
        }
        levelMenu.SetActive(createUser);
    }
    public void CreateUser()
    {
        levelMenu.SetActive(true);
        registerUser.SetActive(false);
        CreatedUser();
        PlayerPrefs.SetString("CreateUser", openedGame);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
    public void BuyPotion()
    {
        if (PlayerPrefs.GetInt("coinScore") >= 500)
        {
            if (buyCoinCount < 3)
            {
                CollectableControl.totalCoinScore = PlayerPrefs.GetInt("coinScore") - 500;
                PlayerPrefs.SetInt("coinScore", CollectableControl.totalCoinScore);
                coin.text = CollectableControl.totalCoinScore.ToString();
                InventoryManager.instance.items.Add(redPotion);
                currentBuyPotionImage[buyCoinCount].color = new Color(255, 255, 255);
                buyCoinCount++;

            }
            else
            {
                showToastMenu.SetActive(true);
                buyPotionButton.GetComponent<Button>().enabled = false;
                StartCoroutine(ShowToastMessage2());
            }


        }
        else
        {
            showToastMenu.SetActive(true);
            buyPotionButton.GetComponent<Button>().enabled = false;
            StartCoroutine(ShowToastMessage());
        }

    }
    IEnumerator ShowToastMessage()
    {
        toastMessage.text = "Not Enough Coin";
        yield return new WaitForSeconds(2.5f);
        showToastMenu.SetActive(false);
        buyPotionButton.GetComponent<Button>().enabled = true;

    }
    IEnumerator ShowToastMessage2()
    {
        toastMessage.text = "You Have Maximum Number of Potion";
        yield return new WaitForSeconds(2.5f);
        showToastMenu.SetActive(false);
        buyPotionButton.GetComponent<Button>().enabled = true;

    }
    IEnumerator ShowToastMessage3()
    {
        toastMessage.text = "You Have Maximum Health Bar";
        yield return new WaitForSeconds(2.5f);
        showToastMenu.SetActive(false);
        buyPotionButton.GetComponent<Button>().enabled = true;

    }


    //Buy Health
    public void UpgradeHealth()
    {


        if (PlayerPrefs.GetInt("HealthBar") < 200 && PlayerPrefs.GetInt("coinScore") >= PlayerPrefs.GetInt("HealthBarUpgradeCoin"))
        {
            PlayerPrefs.SetInt("coinScore", PlayerPrefs.GetInt("coinScore") - PlayerPrefs.GetInt("HealthBarUpgradeCoin"));
            PlayerPrefs.SetInt("HealthBar", PlayerPrefs.GetInt("HealthBar") + 25);
            PlayerPrefs.SetInt("HealthBarImage", PlayerPrefs.GetInt("HealthBarImage") + 1);
            upgradeHealthBar.text = PlayerPrefs.GetInt("HealthBar").ToString();
            currentBuyHealthImage[PlayerPrefs.GetInt("HealthBarImage") - 1].color = new Color(255, 255, 255);
            PlayerPrefs.SetInt("HealthBarUpgradeCoin", PlayerPrefs.GetInt("HealthBarUpgradeCoin") * 2);
            if (PlayerPrefs.GetInt("HealthBarUpgradeCoin") == 40000)
            {
                upgradeHealthBarCoin.text = "MAX";
            }
            else
            {
                upgradeHealthBarCoin.text = PlayerPrefs.GetInt("HealthBarUpgradeCoin").ToString();
            }

            coin.text = PlayerPrefs.GetInt("coinScore").ToString();

        }
        else if (PlayerPrefs.GetInt("coinScore")! <= PlayerPrefs.GetInt("HealthBarUpgradeCoin") && PlayerPrefs.GetInt("HealthBar") !<= 200)
        {
            showToastMenu.SetActive(true);
            StartCoroutine(ShowToastMessage());
        }
        else
        {
            showToastMenu.SetActive(true);
            StartCoroutine(ShowToastMessage3());
        }

    }
}
