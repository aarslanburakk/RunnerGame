using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    public static WallTrap wallInsTence;
    public GameObject wallTrap;
    private Animator wallTrapAnimation;
    

    private void Start()
    {
        wallInsTence = this;
        wallTrapAnimation = GetComponent<Animator>();
    }
    public void OnTriggerEnter(Collider other)
    {
        wallTrapAnimation.Play("WallTrap1");

    }
    
    public void DeleteColider()
    {

        wallTrapAnimation.StopRecording();
    }
    
}