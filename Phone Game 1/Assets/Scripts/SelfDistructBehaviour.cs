using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelfDistructBehaviour : MonoBehaviour
{
    public float holdTime = 2f;
    private WaitForSeconds wFS;

    private IEnumerator Start()
    {
        wFS = new WaitForSeconds(holdTime);
        
        yield return wFS;
        Destroy(gameObject);
    }
}
