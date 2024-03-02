using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _pausePanel;

    private bool _gamePaused;

    private float _timeScale;

    public void TogglePause()
    {
        if (_gamePaused)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Debug.Log("Pause Game");

        _pausePanel.SetActive(true);

        _timeScale = Time.timeScale;

        Time.timeScale = 0;

        _gamePaused = true;
    }
    private void UnPauseGame()
    {
        _pausePanel.SetActive(false);

        Time.timeScale = _timeScale;

        _gamePaused = false;

        Debug.Log("UnPause Game");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TogglePause();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        _pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
