using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TutoPresentationBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private VideoPlayer _videoPlayer;

    [SerializeField] private RectTransform _screenRectTransf;

    [SerializeField] private List<VideoClip> _tutorialVideo;

    [SerializeField] private List<VideoClip> _videoToShow;
    private int _index;

    private bool _hasDisplayed;

    private enum TutorialVideo
    {
        DragDrop = 0,
        Jump = 1,
        Gravity = 2,
        Acceleration = 3,
        laser = 4,
        Button = 5,
        Direction = 6,
        Sphere = 7
    }

    [Button]
    private void TestDisplay()
    {
        AddToShowVideo(_tutorialVideo[0]);

        DisplayAnimation();
    }

    private void SetUpAnimation()
    {
        if (_hasDisplayed)
        {
            return;
        }

        Debug.Log("Setup anim");

        _hasDisplayed = true;

        _index = 0;

        Scene currentScene = SceneManager.GetActiveScene();

        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name != "GlobalScene")
            {
                currentScene = scene;
            }
        }

        Debug.Log("Current tuto scene : " + currentScene.name);

        if (currentScene.name == "tutotest 1")
        {
            AddToShowVideo(_tutorialVideo[0]);
            AddToShowVideo(_tutorialVideo[3]);
        }
        else if(currentScene.name == "tutotest2")
        {
            AddToShowVideo(_tutorialVideo[7]);
            AddToShowVideo(_tutorialVideo[5]);
        }
        else if (currentScene.name == "tutotest 3")
        {
            AddToShowVideo(_tutorialVideo[1]);
        }
        else if (currentScene.name == "tutotest 4")
        {
            AddToShowVideo(_tutorialVideo[2]);
        }
        else if (currentScene.name == "tutotest5")
        {
            AddToShowVideo(_tutorialVideo[6]);
        }

        if (_videoToShow.Count != 0)
        {
            DisplayAnimation();
        }
        else
        {
            _panel.SetActive(false);
        }
    }
    private void DisplayAnimation()
    {
        _videoPlayer.clip = _videoToShow[_index];
        _index++;
        _panel.SetActive(true);
    }

    private void AddToShowVideo(VideoClip video)
    {
        _videoToShow.Add(video);
    }

    public void SkipAnimation()
    {
        if (_index >= _videoToShow.Count)
        {
            _panel.SetActive(false);
        }
        else
        {
            DisplayAnimation();
        }
    }

    private void Start()
    {
        _hasDisplayed = false;

        _panel.SetActive(false);

        GameManager.Instance.OnPhase1Started += SetUpAnimation;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnPhase1Started -= SetUpAnimation;
    }
}
