using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarer : MonoBehaviour
{
    public GameObject countDown3;
    public GameObject countDown2;
    public GameObject countDown1;
    public GameObject countDownGo;
    public GameObject fadeIn;
   
    void Start()
    {
        Events.levelStartOptions = false;
        StartCoroutine(CountSequence());
    }

    IEnumerator CountSequence()
    {
        yield return new WaitForSeconds(1f);
        countDown3.SetActive(true);
        AudioManager.audioInstence.PlaySfxMusic("Ready");
        yield return new WaitForSeconds(1f);
        countDown2.SetActive(true);
        AudioManager.audioInstence.PlaySfxMusic("Ready");
        yield return new WaitForSeconds(1f);
        countDown1.SetActive(true);
        AudioManager.audioInstence.PlaySfxMusic("Ready");
        yield return new WaitForSeconds(1f);
        countDownGo.SetActive(true);
        AudioManager.audioInstence.PlaySfxMusic("Go");
        Player.canMove = true;
        Events.levelStartOptions = true;
    }

}
