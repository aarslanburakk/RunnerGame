using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ObstacleCollision  :  MonoBehaviour
{
    
    public GameObject thePlayer;
    public GameObject charModel;
    public GameObject mainCam;
    public GameObject levelControl;
    public static bool isDead;
    public static bool isGameStarted;

    private void Start()
    {
        isDead = false;
        isGameStarted = false;

    }

    void Update()
    {
        if (isDead)
        {
           // this.gameObject.GetComponent<MeshCollider>().enabled = false;
            thePlayer.GetComponent<Player>().enabled = false;
            AudioManager.audioInstence.StopMusic("Background");
            charModel.GetComponent<Animator>().Play("Stumble Backwards");
            levelControl.GetComponent<LevelDistance>().enabled = false;
            mainCam.GetComponent<Animator>().enabled = true;
            levelControl.GetComponent<EndRunSequance>().enabled = true;
            HealthBar.hitPoint = 0;
            if (InventoryManager.instance.items.Count != 0)
            {
                ButtonPotion.potionInstence.IfPlayerDead();
            }
           
            HealthBar.instance.UpdateHealthBar();
          

        }
    }

   



}
