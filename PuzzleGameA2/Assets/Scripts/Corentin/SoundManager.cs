using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private static SoundManager _instance;
    public static SoundManager Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }
        _instance = this;

        _audioSource = GetComponent<AudioSource>();
    }


    [SerializeField] private List<AudioClip> _menuMusics;
    [SerializeField] private List<AudioClip> _inGameMusics;

    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _defeatSound;

    public void PlayVictorySound()
    {
        _audioSource.PlayOneShot(_victorySound);
    }
    public void PlayDefeatSound()
    {
        _audioSource.PlayOneShot(_defeatSound);
    }
}
