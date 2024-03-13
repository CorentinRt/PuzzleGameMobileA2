using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using GooglePlayGames;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    private static AchievementsManager _instance;
    public static AchievementsManager Instance { get => _instance; set => _instance = value; }

    private bool _madScientist;
    private bool _bigBrain;
    private bool _noBrain;
    private bool _iBelieveICanFly;
    private bool _whatAShock;
    private bool _byAllMeans;
    private bool _shinyShiny;
    private bool _atLeastSuccess;

    private int _killCount;
    private int _jumpCount;
    private int _shockCount;
    private int _spikeCount;


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

            if (PlayerPrefs.HasKey("_killCount"))
            {
                _killCount = PlayerPrefs.GetInt("_killCount");
            }
            if (PlayerPrefs.HasKey("_jumpCount"))
            {
                _jumpCount = PlayerPrefs.GetInt("_jumpCount");
            }
            if (PlayerPrefs.HasKey("_shockCount"))
            {
                _shockCount = PlayerPrefs.GetInt("_shockCount");
            }
            if (PlayerPrefs.HasKey("_spikeCount"))
            {
                _spikeCount = PlayerPrefs.GetInt("_spikeCount", _spikeCount);
            }
        }
    }

    public void IncreaseKillCount()
    {
        _killCount++;
        PlayerPrefs.SetInt("_killCount", _killCount);
    }
    public void IncreaseJumpCount()
    {
        _jumpCount++;
        PlayerPrefs.SetInt("_jumpCount", _jumpCount);
    }
    public void IncreaseShockCount()
    {
        _shockCount++;
        PlayerPrefs.SetInt("_shockCount", _shockCount);
    }
    public void IncreaseSpikeCount()
    {
        _spikeCount++;
        PlayerPrefs.SetInt("_spikeCount", _spikeCount);
    }

    private void Update()
    {
        if (_killCount >= 10 && !_madScientist)
        {
            AchieveMadScientist();
        }

        if (_jumpCount >= 10 && !_iBelieveICanFly)
        {
            AchieveIBelieveICanFly();
        }
        if (_shockCount >= 10 && !_whatAShock)
        {
            AchieveWhatAShock();
        }
        if (_spikeCount >= 10 && !_byAllMeans)
        {
            AchieveByAllMeans();
        }

        if (!_bigBrain)
        {
            List<Level> levels = LevelManager.Instance.GetLevelList();

            int lockedCount = 0;

            foreach (var level in levels)
            {
                if (!level.isUnlocked || level.GetStarsNum == 0)
                {
                    lockedCount++;
                }
            }
            if (lockedCount == 0)
            {
                AchieveBigBrain();
            }
        }
    }


    public void AchieveMadScientist()   // Tuer 10 perso
    {
        if (!_madScientist)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQAQ", 100.0f, (bool success) => {
                // handle success or failure
            });

            _madScientist = true;

            PlayerPrefs.SetInt("_madScientist", 1);

            Debug.Log("Achieve Mad Scientist");

            // faire succès
        }
    }
    public void AchieveBigBrain()   // Finir le jeu
    {
        if (!_bigBrain)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQAg", 100.0f, (bool success) => {
                // handle success or failure
            });

            _bigBrain = true;

            PlayerPrefs.SetInt("_bigBrain", 1);

            Debug.Log("Achieve Big Brain");

            // faire succès
        }
    }
    public void AchieveNoBrain()    // Rater un niveau
    {
        if (!_noBrain)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQAw", 100.0f, (bool success) => {
                // handle success or failure
            });

            _noBrain = true;

            PlayerPrefs.SetInt("_noBrain", 1);

            Debug.Log("Achieve No Brain");

            // faire succès
        }
    }
    public void AchieveIBelieveICanFly()    // Faire sauter 10 perso avec le bumper
    {
        if (!_iBelieveICanFly)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQBA", 100.0f, (bool success) => {
                // handle success or failure
            });

            _iBelieveICanFly = true;

            PlayerPrefs.SetInt("_iBelieveICanFly", 1);

            Debug.Log("Achieve I Believe I Can Fly");

            // faire succès
        }
    }
    public void AchieveWhatAShock()     // Tuer 10 perso avec les spheres
    {
        if (!_whatAShock)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQBQ", 100.0f, (bool success) => {
                // handle success or failure
            });

            _whatAShock = true;

            PlayerPrefs.SetInt("_whatAShock", 1);

            Debug.Log("Achieve What A Shock");

            // faire succès
        }
    }
    public void AchieveByAllMeans()     // Tuer 10 perso dans les spikes
    {
        if (!_byAllMeans)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQBg", 100.0f, (bool success) => {
                // handle success or failure
            });

            _byAllMeans = true;

            PlayerPrefs.SetInt("_byAllMeans", 1);

            Debug.Log("Achieve By All Means");

            // faire succès
        }
    }
    public void AchieveShinyShiny()     // Réussir un niveau avec 3 étoiles
    {
        if (!_shinyShiny)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQBw", 100.0f, (bool success) => {
                // handle success or failure
            });

            _shinyShiny = true;

            PlayerPrefs.SetInt("_shinyShiny", 1);

            Debug.Log("Achieve Shiny Shiny");

            // faire succès
        }
    }
    public void AchieveAtLeastSuccess()     // Réussir un niveau avec 1 seule étoile
    {
        if (!_atLeastSuccess)
        {
            Social.ReportProgress("CgkIgfrixdgKEAIQCA", 100.0f, (bool success) => {
                // handle success or failure
            });

            _atLeastSuccess = true;

            PlayerPrefs.SetInt("_atLeastSuccess", 1);

            Debug.Log("Achieve At Least Success");

            // faire succès
        }
    }
}
