using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayManuallyLoginButton : MonoBehaviour
{
    public void ManuallyLogin()
    {
        if (PlayGamesManager.Instance != null)
        {
            PlayGamesManager.Instance.SignIn();
        }
    }
}
