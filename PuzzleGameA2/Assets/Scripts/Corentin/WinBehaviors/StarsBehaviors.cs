using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehaviors : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;

    [SerializeField] private GameObject[] _stars;


    public void DisplayStars()
    {
        _winPanel.SetActive(true);

        int value = 0;

        value = GameManager.Instance.NbStars;

        for (int i = 0; i < value; i++)
        {
            _stars[i].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameEnd += DisplayStars;
    }
    private void OnDestroy()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
