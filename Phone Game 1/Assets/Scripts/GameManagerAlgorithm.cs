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
    private float buttonLightCounter;


    private void Start()
    {
        render = buttons[colorSelect].GetComponent<Renderer>();
    }

    private void Update()
    {
        if (buttonLightCounter > 0)
        {
            buttonLightCounter -= Time.deltaTime;
        }
        else
        {
            render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 1f);
        }
    }

    public void StartGame()
    {
        colorSelect = Random.Range(0, buttons.Length);
        Debug.Log(colorSelect);
        render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, 0.5f);

        buttonLightCounter = buttonOn;
    }
}
