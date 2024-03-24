using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
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

    [SerializeField] private float _changeDirectionAdj;
    [SerializeField] private float _accelerationAdj;
    [SerializeField] private float _gravityAdj;
    [SerializeField] private float _buttonClickAdj;
    [SerializeField] private float _laserButtonPressedAdj;
    [SerializeField] private float _sideJumpAdj;
    [SerializeField] private float _laserAdj;
    [SerializeField] private float _electricalSphereAdj;


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

        //DontDestroyOnLoad(gameObject);
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

    [Button]
    public void PlayVictorySound()
    {
        if (_victorySound != null)
        {
            _audioSource.PlayOneShot(_victorySound, _effectVolume);
        }
    }
    [Button]
    public void PlayDefeatSound()
    {
        if (_defeatSound != null)
        {
            _audioSource.PlayOneShot(_defeatSound, _effectVolume);
        }
    }
    [Button]
    public void PlayChangeDirectionSound()
    {
        if (_changeDirection != null)
        {
            Debug.Log("Play direction change sound");
            _audioSource.PlayOneShot(_changeDirection, _effectVolume * _changeDirectionAdj);
        }
    }
    [Button]
    public void PlayAccelerationSound()
    {
        if (_acceleration != null)
        {
            Debug.Log("Play acceleration sound");
            _audioSource.PlayOneShot(_acceleration, _effectVolume * _accelerationAdj);
        }
    }
    [Button]
    public void PlayGravitySound()
    {
        if (_gravity != null)
        {
            Debug.Log("Play gravity sound");
            _audioSource.PlayOneShot(_gravity, _effectVolume);
        }
    }
    [Button]
    public void PlayButtonClickSound()
    {
        if (_buttonClick != null)
        {
            Debug.Log("Play click sound");
            _audioSource.PlayOneShot(_buttonClick, _effectVolume * _buttonClickAdj);
        }
    }
    [Button]
    public void PlayLaserButtonPressed()
    {
        if (_laserButtonPressed != null)
        {
            Debug.Log("Laser Button Pressed sound");
            _audioSource.PlayOneShot(_laserButtonPressed, _effectVolume * _laserButtonPressedAdj);
        }
    }
    [Button]
    public void PlaySideJumpSound()
    {
        if (_sideJump != null)
        {
            Debug.Log("Play side jump sound");
            _audioSource.PlayOneShot(_sideJump, _effectVolume * _sideJumpAdj);
        }
    }
    [Button]
    public void PlayLaserSound()
    {
        if (_laser != null)
        {
            Debug.Log("Play laser sound");
            _audioSource.PlayOneShot(_laser, _effectVolume * _laserAdj);
        }
    }
    [Button]
    public void PlayElectricalSphereSound()
    {
        if (_electricalSphere != null)
        {
            Debug.Log("Play electrical sphere sound");
            _audioSource.PlayOneShot(_electricalSphere, _effectVolume * _electricalSphereAdj);
        }
    }
}
