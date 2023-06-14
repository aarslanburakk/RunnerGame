using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTraps : MonoBehaviour
{

    private Animator trapAnimation;


    void Awake()
    {
        trapAnimation = GetComponent<Animator>();
    }
    
    public void OnTriggerEnter(Collider other)
    {
        trapAnimation.Play("TrapAnim");
    }

}
