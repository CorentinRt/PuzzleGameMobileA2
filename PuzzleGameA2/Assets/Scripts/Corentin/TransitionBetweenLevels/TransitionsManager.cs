using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsManager : MonoBehaviour
{
    private static TransitionsManager _instance;
    public static TransitionsManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.LogWarning("You have more than one Transition manager in your scenes !!!");
        }
        _instance = this;
    }


    [SerializeField] private Animator _animator;

    [SerializeField] private float _transitionTime;

    [SerializeField] private GameObject _transitionPanel;

    public float TransitionTime { get => _transitionTime; set => _transitionTime = value; }


    private void StartTransitionInMenu()
    {
        _animator.SetTrigger("InMenu");
    }

    [Button]
    public void StartTransition()
    {
        _animator.ResetTrigger("End");
        _animator.SetTrigger("Start");
    }

    [Button]
    public void EndTransition()
    {
        _animator.ResetTrigger("Start");
        _animator.SetTrigger("End");
    }

    private void Start()
    {
        _transitionPanel.SetActive(true);

        Debug.Log("Name scene : " + SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {

        }
        else
        {

        }
        StartTransition();
    }
}
