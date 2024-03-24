using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehaviors : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    [SerializeField] private GameObject[] _stars;

    private LevelManager _levelManager;
    private GameManager _gameManager;

    [SerializeField] private float _cooldownStarAppears;


    public void DisplayStars()
    {
        foreach (var star in _stars)
        {
            star.SetActive(false);
        }
        _winPanel.SetActive(true);

        int value = 0;

        value = _gameManager.NbStars;

        //for (int i = 0; i < value; i++)
        //{
        //    _stars[i].SetActive(true);
        //}

        StartCoroutine(AppearStarsCooldownCoroutine(value));
    }

    public void DisplayGameOver()
    {
        _losePanel.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _levelManager = LevelManager.Instance;
        _gameManager.OnGameEnd += DisplayStars;
        _gameManager.OnGameLost += DisplayGameOver;
    }
    private void OnDestroy()
    {
        _gameManager.OnGameEnd -= DisplayStars;
        _gameManager.OnGameLost -= DisplayGameOver;
    }

    public void MainMenu()
    {
        _levelManager.MainMenu();
    }

    public void NextLevel()
    {
        _levelManager.LoadLevel(_levelManager.GetCurrentLevel().GetID + 1);
        _winPanel.SetActive(false); 
    } 
 
    public void RestartLevel() 
    { 
        _levelManager.RestartCurrentLevel(); 
        _losePanel.SetActive(false); 
    }


    IEnumerator AppearStarsCooldownCoroutine(int value)
    {
        int currentIndex = 0;

        while (currentIndex < value)
        {
            _stars[currentIndex].SetActive(true);

            currentIndex++;

            yield return new WaitForSeconds(_cooldownStarAppears);
        }

        yield return null;
    }
}
