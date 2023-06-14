using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
   public float coinRotate = 2f;

    void Update()
    {
        transform.Rotate(0, coinRotate, 0, Space.World);
    }
}
