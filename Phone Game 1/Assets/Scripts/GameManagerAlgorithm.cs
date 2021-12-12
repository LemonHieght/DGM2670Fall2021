using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerAlgorithm : MonoBehaviour
{
    public GameObject[] buttons;
    public int colorSelect;
    private Renderer render;
    public float buttonOn;
    public float waitTime;
    public List<int> activePattern;

    private float waitTimeCounter;
    private float buttonLightCounter;
    private bool shouldBeLit;
    private bool shouldDark;
    private bool gameActive;
    private int positionSequence;
    private int inputPattern;


    private void Start()
    {
        render = buttons[activePattern[positionSequence]].GetComponent<Renderer>();
        gameActive = false;
    }

    private void Update()
    {
        if (shouldBeLit)
        {
            buttonLightCounter -= Time.deltaTime;
            
            if(buttonLightCounter < 0)
            { 
                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
                shouldBeLit = false;
                shouldDark = true;
                waitTimeCounter = waitTime;
                positionSequence++;
            }
        }

        if (shouldDark)
        {
            waitTimeCounter -= Time.deltaTime;
            
            if (positionSequence >= activePattern.Count)
            {
                shouldDark = false;
                gameActive = true;
            }
            else
            {
                if (waitTimeCounter < 0)
                {
                    RenderColorSelect();
                    
                    buttonLightCounter = buttonOn;
                    shouldBeLit = true;
                    shouldDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        activePattern.Clear();
        positionSequence = 0;
        inputPattern = 0;
        
        colorSelect = Random.Range(0, buttons.Length);
        Debug.Log(colorSelect);
        activePattern.Add(colorSelect);
        
        RenderColorSelect();

        buttonLightCounter = buttonOn;
        
        shouldBeLit = true;
    }

    public void RenderColorSelect()
    {
        render = buttons[activePattern[positionSequence]].GetComponent<Renderer>();
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.5f);
    }

    public void ColorPress(int whatButton)
    {
        if (gameActive)
        {
            if (activePattern[inputPattern] == whatButton)
            {
                Debug.Log("Correct");

                inputPattern++;
                if (inputPattern >= activePattern.Count)
                {
                    positionSequence = 0;
                    inputPattern = 0;
                    
                    colorSelect = Random.Range(0, buttons.Length);
                    activePattern.Add(colorSelect);
        
                    RenderColorSelect();

                    buttonLightCounter = buttonOn;
        
                    shouldBeLit = true;
                    gameActive = false;
                }
            }
            else
            {
                Debug.Log("wrong");
                gameActive = false;
            }
        }
    }
}
