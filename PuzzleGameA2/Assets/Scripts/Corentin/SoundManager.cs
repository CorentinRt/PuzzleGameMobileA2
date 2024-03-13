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


    [SerializeField] private AudioClip _changeDirection;
    [SerializeField] private AudioClip _acceleration;
    [SerializeField] private AudioClip _gravity;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _laserButtonPressed;
    [SerializeField] private AudioClip _sideJump;
    [SerializeField] private AudioClip _laser;
    [SerializeField] private AudioClip _electricalSphere;


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
        _audioSource.volume = _musicVolume;

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
        if (_victorySound != null)
        {
            _audioSource.PlayOneShot(_victorySound, _effectVolume);
        }
    }
    public void PlayDefeatSound()
    {
        if (_defeatSound != null)
        {
            _audioSource.PlayOneShot(_defeatSound, _effectVolume);
        }
    }

    public void PlayChangeDirectionSound()
    {
        if (_changeDirection != null)
        {
            _audioSource.PlayOneShot(_changeDirection, _effectVolume);
        }
    }
    public void PlayAccelerationSound()
    {
        if (_acceleration != null)
        {
            _audioSource.PlayOneShot(_acceleration, _effectVolume);
        }
    }
    public void PlayGravitySound()
    {
        if (_gravity != null)
        {
            _audioSource.PlayOneShot(_gravity, _effectVolume);
        }
    }
    public void PlayButtonClickSound()
    {
        if (_buttonClick != null)
        {
            Debug.Log("Play click sound");
            _audioSource.PlayOneShot(_buttonClick, _effectVolume);
        }
    }
    public void PlayLaserButtonPressed()
    {
        if (_laserButtonPressed != null)
        {
            Debug.Log("Laser Button Pressed sound");
            _audioSource.PlayOneShot(_laserButtonPressed);
        }
    }
    public void PlaySideJumpSound()
    {
        if (_sideJump != null)
        {
            _audioSource.PlayOneShot(_sideJump, _effectVolume);
        }
    }
    public void PlayLaserSound()
    {
        if (_laser != null)
        {
            _audioSource.PlayOneShot(_laser, _effectVolume);
        }
    }
    public void PlayElectricalSphereSound()
    {
        if (_electricalSphere != null)
        {
            _audioSource.PlayOneShot(_electricalSphere, _effectVolume);
        }
    }
}
