using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public static Events eventInstance;
    public GameObject stopCanvas;
    public static bool levelStartOptions = false;
    // Start is called before the first frame update
    private void Awake()
    {
        
        eventInstance = this;
    }
    public void ReplayGame()
    {
  
   
        InventoryManager.instance.items = new List<GameObject>();
        Player.playerSpeed = 7f;
        Player.canMove = false;
        GetUsers.instance.UploadScore();
        SceneManager.LoadScene("StartLevel");

    }
    public void QuitMenu()
    {
        
        InventoryManager.instance.items = new List<GameObject>();  
        AudioManager.audioInstence.StopMusic("Background");
        Player.playerSpeed = 7f;
        Player.canMove = false;
        Time.timeScale = 1;
        GetUsers.instance.UploadScore(); 
        SceneManager.LoadScene("MainMenu");
    }
    public void SettingsOn()
    {
        stopCanvas.SetActive(true);
        Player.canMove = false;
        Time.timeScale = 0;
    }
    public void SettingsOff()
    {
        stopCanvas.SetActive(false);
        Time.timeScale = 1;
        if (levelStartOptions==true)
        {
            Player.canMove = true;
        }
        else
        {
            Player.canMove = false;
        }
    }

}
