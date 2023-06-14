using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    
    public Image currentHealthBar;
    public GameObject charModel;
    public ParticleSystem healEfect;
    public TMP_Text textHeal;
    
    
    public GameObject mainCam;
    public GameObject levelControl;
    public static float hitPoint;
    public static float maxHitPoint;
    int heal = 25;
    
    
    void Start()
    {
        UpdateHealthBar();
        instance = this;
    


    }
    private void Awake()
    {
        hitPoint = PlayerPrefs.GetInt("HealthBar");
        maxHitPoint = PlayerPrefs.GetInt("HealthBar"); 
    }
    public void UpdateHealthBar()
    {
        float ratio = hitPoint / maxHitPoint;
        textHeal.text = hitPoint.ToString();
        currentHealthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);

    }

    public void TakeDamage(float damage)
    {
        hitPoint -= damage;
        if(hitPoint<=0)
        {
            hitPoint = 0;
            UpdateHealthBar();
            if (InventoryManager.instance.items.Count != 0)
            {
                ButtonPotion.potionInstence.IfPlayerDead();
            }
            charModel.GetComponent<Player>().enabled = false;
            charModel.GetComponent<Animator>().Play("Standing React Death Right");
            AudioManager.audioInstence.StopMusic("Background");
            levelControl.GetComponent<LevelDistance>().enabled = false;
            mainCam.GetComponent<Animator>().enabled = true;
            charModel.GetComponent<CharacterController>().enabled = false;
            levelControl.GetComponent<EndRunSequance>().enabled = true;

        }
        AudioManager.audioInstence.PlaySfxMusic("Hurt");
        UpdateHealthBar();
    }
    public void UseHeal()
    {
        hitPoint += heal;
        healEfect.Play();
        if (hitPoint >= PlayerPrefs.GetInt("HealthBar"))
        {
            hitPoint = PlayerPrefs.GetInt("HealthBar");
            UpdateHealthBar();

        }
        UpdateHealthBar();
    }
    
}
