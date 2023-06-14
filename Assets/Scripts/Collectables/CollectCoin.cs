using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
  
    public void OnTriggerEnter(Collider other)
    {
        CollectableControl.coinCount += 1;
        this.gameObject.SetActive(false);

    }
        


}
