using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractBehavior : MonoBehaviour
{
    public GameObject manager;
    private bool buttonActive;
    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;
    private GameManagerAlgorithm gameManager;


    private void Start()
    { 
        gameManager = manager.GetComponent<GameManagerAlgorithm>();
    }

    private void Update()
    {
        buttonActive = gameManager.gameActive;
    }

    private void OnMouseDown()
    {
        if (buttonActive == true)
        {
            onMouseDown.Invoke();
        }
    }
    private void OnMouseUp()
    {
        if (buttonActive == true)
        {
            onMouseUp.Invoke();
        }
    }
}
