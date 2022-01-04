using System;
using System.Collections;
using System.Collections.Generic;
using Antlr.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManagerAlgorithm : MonoBehaviour
{
    public GameObject[] buttons;
    public AudioSource[] buttonSound;
    public int colorSelect;
    public float buttonOn;
    public float waitTime;
    public List<int> activePattern;
    public Text scoreText;
    public Text highscoreText;
    public UnityEvent gameOver;
    public AudioSource correct;
    public AudioSource incorrect;

    private float waitBetweenPattern;
    private Renderer render;
    private float waitTimeCounter;
    private float buttonLightCounter;
    private bool shouldBeLit;
    private bool shouldDark;
    public bool gameActive;
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

        shouldDark = false;
        shouldBeLit = false;
        buttonLightCounter = buttonOn;
    }

    private void Update()
    {
        if (shouldBeLit)
        {
            buttonLightCounter -= Time.deltaTime;
            
            if(buttonLightCounter <= 0)
            {
                if (render.material.color == new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1.5f))
                {
                    ButtonFade();
                    buttonSound[activePattern[positionSequence]].Stop();
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
                if (waitTimeCounter <= 0)
                {
                    RenderColorSelect();
                    buttonSound[activePattern[positionSequence]].Play();
                    
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
        buttonSound[activePattern[positionSequence]].Play();

        buttonLightCounter = buttonOn;
        
        shouldBeLit = true;
    }

    public void RenderColorSelect()
    {
        render = buttons[activePattern[positionSequence]].GetComponent<Renderer>();
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1.5f);
    }

    public void ColorPress(int whatButton)
    {
        if (gameActive)
        {
            if (activePattern[inputPattern] == whatButton)
            {
                colorSelect = inputPattern;

                inputPattern++;
                
                if (inputPattern >= activePattern.Count)
                {
                    Debug.Log("Correct");
                    correct.Play();
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

                    // RenderColorSelect();
                    // buttonSound[activePattern[positionSequence]].Play();
                    StartCoroutine(DelayNextPattern(0.5f));

                    buttonLightCounter = buttonOn;
        
                    shouldBeLit = true;
                    gameActive = false;
                }
            }
            else
            {
                Debug.Log("wrong");
                incorrect.Play();
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
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1.5f);
    }

    private void Restart()
    {
        gameOver.Invoke();
    }

    IEnumerator DelayNextPattern(float time)
    {
        yield return new WaitForSeconds(time);
        RenderColorSelect();
        buttonSound[activePattern[positionSequence]].Play();
    }
}
