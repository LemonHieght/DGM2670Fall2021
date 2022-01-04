using UnityEngine;
using UnityEngine.Events;

public class InteractBehavior : MonoBehaviour
{
    public GameObject manager;
    private bool _buttonActive;
    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;
    private GameManagerAlgorithm _gameManager;


    private void Start()
    { 
        _gameManager = manager.GetComponent<GameManagerAlgorithm>();
    }

    private void Update()
    {
        _buttonActive = _gameManager.gameActive;
    }

    private void OnMouseDown()
    {
        if (_buttonActive == true)
        {
            onMouseDown.Invoke();
        }
    }
    private void OnMouseUp()
    {
        if (_buttonActive == true)
        {
            onMouseUp.Invoke();
        }
    }
}
