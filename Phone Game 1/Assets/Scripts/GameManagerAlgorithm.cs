using System.Collections;
using System.Collections.Generic;
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
    public bool gameActive;

    private float _waitBetweenPattern;
    private Renderer _render;
    private float _waitTimeCounter;
    private float _buttonLightCounter;
    private bool _shouldBeLit;
    private bool _shouldDark;
    private int _positionSequence;
    private int _inputPattern;


    private void Start()
    {
        _render = buttons[activePattern[_positionSequence]].GetComponent<Renderer>();
        gameActive = false;
        if (!PlayerPrefs.HasKey("HiScore"))
        {
            PlayerPrefs.SetInt("HiScore", 0);
        }

        scoreText.text = "Current Score:0";
        highscoreText.text = "Highscore:" + PlayerPrefs.GetInt("HiScore");

        _shouldDark = false;
        _shouldBeLit = false;
        _buttonLightCounter = buttonOn;
    }

    private void Update()
    {
        if (_shouldBeLit)
        {
            _buttonLightCounter -= Time.deltaTime;
            
            if(_buttonLightCounter <= 0)
            {
                if (_render.material.color == new Color(_render.material.color.r, _render.material.color.g, _render.material.color.b, 1.5f))
                {
                    ButtonFade();
                    buttonSound[activePattern[_positionSequence]].Stop();
                }
                _shouldBeLit = false;
                _shouldDark = true;
                _waitTimeCounter = waitTime;
                _positionSequence++;
            }
        }
        if (_shouldDark)
        {
            _waitTimeCounter -= Time.deltaTime;
            
            if (_positionSequence >= activePattern.Count)
            {
                _shouldDark = false;
                gameActive = true;
            }
            else
            {
                if (_waitTimeCounter <= 0)
                {
                    RenderColorSelect();
                    buttonSound[activePattern[_positionSequence]].Play();
                    
                    _buttonLightCounter = buttonOn;
                    _shouldBeLit = true;
                    _shouldDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        activePattern.Clear();
        _positionSequence = 0;
        _inputPattern = 0;
        scoreText.text = "Current Score:0";
        highscoreText.text = "Highscore:" + PlayerPrefs.GetInt("HiScore");
        colorSelect = Random.Range(0, buttons.Length);
        Debug.Log(colorSelect);
        activePattern.Add(colorSelect);
        
        RenderColorSelect();
        buttonSound[activePattern[_positionSequence]].Play();

        _buttonLightCounter = buttonOn;
        
        _shouldBeLit = true;
    }

    private void RenderColorSelect()
    {
        _render = buttons[activePattern[_positionSequence]].GetComponent<Renderer>();
        var material = _render.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1.5f);
    }

    public void ColorPress(int whatButton)
    {
        if (gameActive)
        {
            if (activePattern[_inputPattern] == whatButton)
            {
                colorSelect = _inputPattern;

                _inputPattern++;
                
                if (_inputPattern >= activePattern.Count)
                {
                    Debug.Log("Correct");
                    correct.Play();
                    if(activePattern.Count > PlayerPrefs.GetInt("HiScore"))
                    {
                        PlayerPrefs.SetInt("HiScore" , activePattern.Count);
                    }

                    scoreText.text = "Current Score:" + activePattern.Count;
                    highscoreText.text = "Highscore:" + PlayerPrefs.GetInt("HiScore");
                    
                    _positionSequence = 0;
                    _inputPattern = 0;
                    
                    colorSelect = Random.Range(0, buttons.Length);
                    activePattern.Add(colorSelect);

                    // RenderColorSelect();
                    // buttonSound[activePattern[positionSequence]].Play();
                    StartCoroutine(DelayNextPattern(0.5f));

                    _buttonLightCounter = buttonOn;
        
                    _shouldBeLit = true;
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
        _render.material.color = new Color(_render.material.color.r, _render.material.color.g, _render.material.color.b, 0.5f);
    }

    public void ButtonBright()
    {
        _render = buttons[activePattern[_inputPattern]].GetComponent<Renderer>();
        var material = _render.material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1.5f);
    }

    private void Restart()
    {
        gameOver.Invoke();
    }

    IEnumerator DelayNextPattern(float time)
    {
        yield return new WaitForSeconds(time);
        RenderColorSelect();
        buttonSound[activePattern[_positionSequence]].Play();
    }
}
