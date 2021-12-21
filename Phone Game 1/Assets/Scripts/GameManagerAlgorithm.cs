using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManagerAlgorithm : MonoBehaviour
{
    public GameObject[] buttons;
    public int colorSelect;
    public float buttonOn;
    public float waitTime;
    public List<int> activePattern;
    public Text scoreText;
    public Text highscoreText;
    public UnityEvent gameOver; 

    private Renderer render;
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
        if (!PlayerPrefs.HasKey("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", 0);
        }

        scoreText.text = "Current Score:0";
        highscoreText.text = "Highscore:" + PlayerPrefs.GetInt("HiScore");
    }

    private void Update()
    {
        if (shouldBeLit)
        {
            buttonLightCounter -= Time.deltaTime;
            
            if(buttonLightCounter < 0)
            {
                if (render.material.color == new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f))
                {
                    ButtonFade();
                }
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
        scoreText.text = "Current Score:0";
        highscoreText.text = "Highscore:" + PlayerPrefs.GetInt("HiScore");
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
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
    }

    public void ColorPress(int whatButton)
    {
        if (gameActive)
        {
            if (activePattern[inputPattern] == whatButton)
            {
                Debug.Log("Correct");
                colorSelect = inputPattern;

                inputPattern++;
                if (inputPattern >= activePattern.Count)
                {
                    if(activePattern.Count > PlayerPrefs.GetInt("HiScore"))
                    {
                        PlayerPrefs.SetInt("HiScore" , activePattern.Count);
                    }

                    scoreText.text = "Current Score:" + activePattern.Count;
                    highscoreText.text = "Highscore:" + PlayerPrefs.GetInt("HiScore");
                    
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
                Restart();
            }
        }
    }

    public void ButtonFade()
    {
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.5f);
    }

    public void ButtonBright()
    {
        render = buttons[activePattern[inputPattern]].GetComponent<Renderer>();
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
    }
    public void Restart()
    {
        gameOver.Invoke();
    }
}
