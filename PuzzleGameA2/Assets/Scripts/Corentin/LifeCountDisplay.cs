using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class LifeCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lifeCountText;

    private void UpdateLifeCountText(int value)
    {
        _lifeCountText.text = _lifeCountText.text.Substring(0, 2) + value.ToString();
    }

    private void Start()
    {
        //PlayerManager.Instance.OnLifeUpdate += UpdateLifeCountText;
    }
    private void OnDestroy()
    {
        //PlayerManager.Instance.OnLifeUpdate -= UpdateLifeCountText;
    }
}
