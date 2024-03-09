using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    private AchievementsManager _instance;
    public AchievementsManager Instance { get => _instance; set => _instance = value; }


    private bool _madScientist;
    private bool _bigBrain;
    private bool _noBrain;
    private bool _iBelieveICanFly;
    private bool _whatAShock;
    private bool _byAllMeans;
    private bool _shinyShiny;
    private bool _atLeastSuccess;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;

        if (_instance != null)
        {
            if (PlayerPrefs.HasKey("_madScientist"))
            {
                if (PlayerPrefs.GetInt("_madScientist") == 1)
                {
                    _madScientist = true;
                }
            }
            if (PlayerPrefs.HasKey("_bigBrain"))
            {
                if (PlayerPrefs.GetInt("_bigBrain") == 1)
                {
                    _bigBrain = true;
                }
            }
            if (PlayerPrefs.HasKey("_noBrain"))
            {
                if (PlayerPrefs.GetInt("_noBrain") == 1)
                {
                    _noBrain = true;
                }
            }
            if (PlayerPrefs.HasKey("_iBelieveICanFly"))
            {
                if (PlayerPrefs.GetInt("_iBelieveICanFly") == 1)
                {
                    _iBelieveICanFly = true;
                }
            }
            if (PlayerPrefs.HasKey("_whatAShock"))
            {
                if (PlayerPrefs.GetInt("_whatAShock") == 1)
                {
                    _whatAShock = true;
                }
            }
            if (PlayerPrefs.HasKey("_byAllMeans"))
            {
                if (PlayerPrefs.GetInt("_byAllMeans") == 1)
                {
                    _byAllMeans = true;
                }
            }
            if (PlayerPrefs.HasKey("_shinyShiny"))
            {
                if (PlayerPrefs.GetInt("_shinyShiny") == 1)
                {
                    _shinyShiny = true;
                }
            }
            if (PlayerPrefs.HasKey("_atLeastSuccess"))
            {
                if (PlayerPrefs.GetInt("_atLeastSuccess") == 1)
                {
                    _atLeastSuccess = true;
                }
            }
        }
    }


    public void AchieveMadScientist()   // Tuer 10 perso
    {
        if (!_madScientist)
        {
            _madScientist = true;

            PlayerPrefs.SetInt("_madScientist", 1);

            // faire succ�s
        }
    }
    public void AchieveBigBrain()   // Finir le jeu
    {
        if (!_bigBrain)
        {
            _bigBrain = true;

            PlayerPrefs.SetInt("_bigBrain", 1);

            // faire succ�s
        }
    }
    public void AchieveNoBrain()    // Rater un niveau
    {
        if (!_noBrain)
        {
            _noBrain = true;

            PlayerPrefs.SetInt("_noBrain", 1);

            // faire succ�s
        }
    }
    public void AchieveIBelieveICanFly()    // Faire sauter 10 perso avec le bumper
    {
        if (!_iBelieveICanFly)
        {
            _iBelieveICanFly = true;

            PlayerPrefs.SetInt("_iBelieveICanFly", 1);

            // faire succ�s
        }
    }
    public void AchieveWhatAShock()     // Tuer 10 perso avec les spheres
    {
        if (!_whatAShock)
        {
            _whatAShock = true;

            PlayerPrefs.SetInt("_whatAShock", 1);

            // faire succ�s
        }
    }
    public void AchieveByAllMeans()     // Tuer 10 perso dans les spikes
    {
        if (!_byAllMeans)
        {
            _byAllMeans = true;

            PlayerPrefs.SetInt("_byAllMeans", 1);

            // faire succ�s
        }
    }
    public void AchieveShinyShiny()     // R�ussir un niveau avec 3 �toiles
    {
        if (!_shinyShiny)
        {
            _shinyShiny = true;

            PlayerPrefs.SetInt("_shinyShiny", 1);

            // faire succ�s
        }
    }
    public void AchieveAtLeastSuccess()     // R�ussir un niveau avec 1 seule �toile
    {
        if (!_atLeastSuccess)
        {
            _atLeastSuccess = true;

            PlayerPrefs.SetInt("_atLeastSuccess", 1);

            // faire succ�s
        }
    }
}
