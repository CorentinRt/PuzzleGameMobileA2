using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private static SoundManager _instance;
    public static SoundManager Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] private float _musicVolume;
    [SerializeField] private float _effectVolume;

    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _inGameMusic;

    [SerializeField] private AudioClip _victorySound;
    [SerializeField] private AudioClip _defeatSound;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("_volumeMusic"))
        {
            PlayerPrefs.SetFloat("_volumeMusic", 0.5f);
        }
        if (!PlayerPrefs.HasKey("_effectVolume"))
        {
            PlayerPrefs.SetFloat("_effectVolume", 0.5f);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (PlayerPrefs.HasKey("_musicVolume"))
        {
            _musicVolume = PlayerPrefs.GetFloat("_musicVolume");
        }
        if (PlayerPrefs.HasKey("_effectVolume"))
        {
            _effectVolume = PlayerPrefs.GetFloat("_effectVolume");
        }

        if (SceneManager.GetActiveScene().name == "MainMenuScene" && _audioSource.clip != _menuMusic)
        {
            _audioSource.clip = _menuMusic;
            _audioSource.Play();
        }
        else if (SceneManager.GetActiveScene().name != "MainMenuScene" && _audioSource.clip != _inGameMusic)
        {
            _audioSource.clip = _inGameMusic;
            _audioSource.Play();
        }
    }

    public void PlayVictorySound()
    {
        _audioSource.PlayOneShot(_victorySound, _effectVolume);
    }
    public void PlayDefeatSound()
    {
        _audioSource.PlayOneShot(_defeatSound, _effectVolume);
    }
}
