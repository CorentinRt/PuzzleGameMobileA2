using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundParameters : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectSlider;

    public void SetMusicVolume()
    {
        PlayerPrefs.SetFloat("_musicVolume", _musicSlider.value);
    }
    public void SetEffectVolume()
    {
        PlayerPrefs.SetFloat("_effectVolume", _effectSlider.value);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("_musicVolume"))
        {
            _musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("_musicVolume"));
        }

        if (PlayerPrefs.HasKey("_effectVolume"))
        {
            _effectSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("_effectVolume"));
        }
    }
}
