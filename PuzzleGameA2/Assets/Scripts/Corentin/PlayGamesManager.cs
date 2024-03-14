using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using TMPro;
using GooglePlayGames.BasicApi;

public class PlayGamesManager : MonoBehaviour
{
    private static PlayGamesManager _instance;
    public static PlayGamesManager Instance { get => _instance; set => _instance = value; }
    public bool UseGooglePlay { get => _useGooglePlay; set => _useGooglePlay = value; }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;

        DontDestroyOnLoad(_instance);
    }

    private bool _useGooglePlay;

    public void Start()
    {

        PlayGamesPlatform.DebugLogEnabled = true;


        //PlayGamesPlatform.Activate();



        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services

            _useGooglePlay = true;
        }
        else
        {
            
            _useGooglePlay = false;

            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }
}
