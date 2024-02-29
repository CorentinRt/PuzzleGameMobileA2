using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehaviors : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;

    [SerializeField] private GameObject[] _stars;

    private LevelManager _levelManager;
    private GameManager _gameManager;


    public void DisplayStars()
    {
        _winPanel.SetActive(true);

        int value = 0;

        value = _gameManager.NbStars;

        for (int i = 0; i < value; i++)
        {
            _stars[i].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _levelManager = _gameManager.gameObject.GetComponent<LevelManager>();
        _gameManager.OnGameEnd += DisplayStars;
    }
    private void OnDestroy()
    {
        _gameManager.OnGameEnd -= DisplayStars;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        _levelManager.MainMenu();
    }

    public void NextLevel()
    {
        _levelManager.LoadLevel(_levelManager.GetCurrentLevel().GetID + 1);
    }
}
