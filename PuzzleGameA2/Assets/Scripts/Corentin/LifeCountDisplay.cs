using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class LifeCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lifeCountText;

    private void UpdateLifeCountText()
    {
        int value = PlayerManager.Instance.GetLivesNumber();
        Debug.Log("Updating Value to " + value);
        _lifeCountText.text = _lifeCountText.text.Substring(0, 2) + value.ToString();
    }

    private void Start()
    {
        //PlayerManager.Instance.OnLifeUpdate += UpdateLifeCountText;
        PlayerManager.Instance.OnPlayerDeath += UpdateLifeCountText;

        LevelManager.Instance.OnLevelFinishedLoad += SetLifeCount;
        _lifeCountText.text = _lifeCountText.text.Substring(0, 2) + 0;

    }

    private void SetLifeCount()
    {
        int value = LevelManager.Instance.GetCurrentLevel().LevelInfo.NbPlayerLives;
        _lifeCountText.text = _lifeCountText.text.Substring(0, 2) + value.ToString();
    }

    private void OnDestroy()
    {
        //PlayerManager.Instance.OnLifeUpdate -= UpdateLifeCountText;
        PlayerManager.Instance.OnPlayerDeath -= UpdateLifeCountText;
    }
}
