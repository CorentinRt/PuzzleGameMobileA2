using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackHomeSystem : MonoBehaviour
{
    public void GoToMainMenu()
    {
        LevelManager.Instance.MainMenu();
    }
}
