using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunSequance : MonoBehaviour
{
    public GameObject liveCoins;  
    public GameObject liveDis;
    public GameObject endScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndSequence());
        LevelDistance.levelInstance.SaveDataGame();
    }

    // Update is called once per frame  
    IEnumerator EndSequence()
    {
        
        yield return new WaitForSeconds(4);
        //IntersitialAD.addInstence.ShowAd();
        liveCoins.SetActive(false);
        liveDis.SetActive(false);
        endScreen.SetActive(true);
       
    }
}
