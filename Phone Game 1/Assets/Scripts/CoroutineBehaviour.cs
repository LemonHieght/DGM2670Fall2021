using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineBehaviour : MonoBehaviour
{
    public UnityEvent startEvent;
    public bool canRun = true;
    public float holdTime = 2f;
    private WaitForSeconds wFS;
    IEnumerator Start()
    {
        wFS = new WaitForSeconds(holdTime);
        while (canRun)
        {
            yield return wFS;
            print("hello World");
        }
    }
}
