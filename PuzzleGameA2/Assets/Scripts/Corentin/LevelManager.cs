using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField,Scene] private string _globalScene;
    [SerializeField, Scene] private string _mainMenu;
    private int _currentLevelID;
    private bool isLevelLoaded;
    public event Action OnLevelUnload;
    public event Action OnLevelFinishedLoad;

    [Button]
    private void LoadFirstLevel()
    {
        LoadLevel(_levels[0].GetID);
    }

    public Level GetLevel(int id) => _levels.Find(x => x.GetID == id);
    public Level GetCurrentLevel() => GetLevel(_currentLevelID);

    public void UnlockLevel(int id) => GetLevel(id).Unlock();

    public void LoadLevel(int id)
    {
        if (GetLevel(id).isUnlocked)
        {
            if (isLevelLoaded)
            {
                SceneManager.UnloadSceneAsync(GetLevel(_currentLevelID).GetScene);
                OnLevelUnload?.Invoke();
            }
            StartCoroutine(LoadLevelAndWait(id));
        }
        
    }

    public void LoadGlobalSceneAndLevel(int id)
    {
        SceneManager.LoadScene(_globalScene);
        LoadLevel(id);
        Debug.Log(SceneManager.loadedSceneCount);
    }

    private IEnumerator LoadLevelAndWait(int id)
    {
        var asyncOp = SceneManager.LoadSceneAsync(GetLevel(id).LevelInfo.LevelScene, LoadSceneMode.Additive);
        asyncOp.allowSceneActivation = true;
        while (!asyncOp.isDone)
        {
            yield return null;
        }
        isLevelLoaded = true;
        _currentLevelID = id;
        OnLevelFinishedLoad?.Invoke();
        Debug.Log("lManager");
        yield return null;
    }
    public void UnloadCurrentLevel()
    {
        SceneManager.LoadScene(GetLevel(_currentLevelID).LevelInfo.LevelScene, LoadSceneMode.Additive);
        isLevelLoaded = false;
        OnLevelUnload?.Invoke();
    }

    public void MainMenu()
    {
        UnloadCurrentLevel();
        SceneManager.LoadScene(_mainMenu);
    }
    
}

[Serializable]
public class Level
{
    [SerializeField] private LevelInfo _levelInfo;
    [SerializeField] private int _starsWon;
    [SerializeField] private bool _unlocked;

    public bool isUnlocked => _unlocked;
    public int GetStarsNum => _starsWon;

    public void SetStars(int nbStars) => _starsWon = _starsWon<nbStars? nbStars : _starsWon;
    public void Unlock() => _unlocked = true;
    public int GetID => _levelInfo.LevelID;
    public string GetScene => _levelInfo.LevelScene;
    public LevelInfo LevelInfo => _levelInfo;
}
