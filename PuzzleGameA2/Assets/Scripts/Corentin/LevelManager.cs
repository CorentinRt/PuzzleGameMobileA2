using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get => _instance; set => _instance = value; }

    [SerializeField] private List<Level> _levels;

    public List<Level> GetLevelList() => _levels;
    
    [SerializeField,Scene] private string _globalScene;
    [SerializeField, Scene] private string _mainMenu;
    private int _currentLevelID;
    private bool isLevelLoaded;

    private LevelController _currentLC;
    public LevelController GetCurrentLevelController => _currentLC = _currentLC == null ? LevelController.Instance : _currentLC;

    //FindObjectsOfType<LevelController>().Where(x => x.gameObject.scene == behav.gameObject.scene).FirstOrDefault();

    public event Action OnSaveLoaded;
    public event Action OnLevelFinishedLoad;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Level level in _levels)
        {
            level.SetStars(0);
            level.Lock();
        }
        
        foreach (LevelSavedData data in SaveManager.LoadData(_levels))
        {
            Level level = GetLevel(data.ID);
            level.Unlock(false);
            level.SetStars(data.Stars);
        }
        OnSaveLoaded?.Invoke();
        
        if (!_levels[0].isUnlocked) _levels[0].Unlock();
    }

    private void Start()
    {
#if UNITY_EDITOR
        Scene[] scenes = SceneManager.GetAllScenes();
        foreach (Scene scene in scenes)
        {
            if (scene.name !=_globalScene)
            {
                var l = _levels.Find(x => x.GetScene == scene.name);

                if (l == null)
                    continue;

                _levels.Find(x => x.GetScene == scene.name).Unlock();
                LoadGlobalSceneAndLevel(_levels.Find(x => x.GetScene == scene.name).GetID);
            }
            
        }
#endif

    }

    [Button]
    private void LoadFirstLevel()
    {
        LoadLevel(_levels[0].GetID);
    }

    public void LoadLastLevelUnlocked()
    {
        int id = _levels[0].GetID;
        foreach (Level level in _levels)
        {
            if (level.isUnlocked) id = level.GetID;
            else break;
        }
        LoadGlobalSceneAndLevel(id);
    }

    public Level GetLevel(int id) => _levels.Find(x => x.GetID == id);
    public Level GetCurrentLevel() => GetLevel(_currentLevelID);

    public void UnlockLevel(int id) => GetLevel(id).Unlock();

    public void LoadLevel(int id)
    {
        if (!DoesLevelExist(id)) return;
        Debug.Log("loading Level" + 1);
        if (GetLevel(id).isUnlocked)
        {
            if (isLevelLoaded)
            {
                SceneManager.UnloadSceneAsync(GetLevel(_currentLevelID).GetScene);
                GetCurrentLevelController.OnLevelUnload?.Invoke();
            }
            StartCoroutine(LoadLevelAndWait(id));
        }
        
    }

    public void LoadGlobalSceneAndLevel(int id)
    {
        StartCoroutine(WaitGlobalSceneAndLevel(id));
    }

    private IEnumerator WaitGlobalSceneAndLevel(int id)
    {
        var asyncOp = SceneManager.LoadSceneAsync(_globalScene);
        asyncOp.allowSceneActivation = true;
        while (!asyncOp.isDone)
        {
            yield return null;
        }
        LoadLevel(id);
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
        SceneManager.UnloadSceneAsync(GetLevel(_currentLevelID).LevelInfo.LevelScene);
        isLevelLoaded = false;
        LevelManager.Instance.GetCurrentLevelController.OnLevelUnload?.Invoke();
    }

    public void MainMenu()
    {
        UnloadCurrentLevel();
        SceneManager.LoadScene(_mainMenu);
    }

    public void RestartCurrentLevel()
    {
        LoadLevel(_currentLevelID);
    }

    public bool DoesLevelExist(int id)
    {
        foreach (Level level in _levels)
        {
            if (level.GetID == id) return true;
        }

        return false;
    }

    public void UnlockNextLevel()
    {
        if (DoesLevelExist(_currentLevelID + 1))
        {
            if (!GetLevel(_currentLevelID+1).isUnlocked) UnlockLevel(_currentLevelID + 1);
        }
    }

    public void ReloadSave()
    {
        foreach (Level level in _levels)
        {
            level.SetStars(0);
            level.Lock();
        }
        foreach (LevelSavedData data in SaveManager.LoadData(_levels))
        {
            Debug.Log("ID: " + data.ID + ", Stars: " + data.Stars +  ", Unlocked: " + data.Unlocked);
            Level level = GetLevel(data.ID);
            level.Unlock(false);
            level.SetStars(data.Stars);
        }
        if (!_levels[0].isUnlocked) _levels[0].Unlock();
        OnSaveLoaded?.Invoke();
    }
}

[Serializable]
public class Level
{
    [SerializeField, Expandable] private LevelInfo _levelInfo;
    [SerializeField] private int _starsWon;
    [SerializeField] private bool _unlocked;

    public bool isUnlocked => _unlocked;
    public int GetStarsNum => _starsWon;

    public void UpdateStars(int nbStars, bool save = true)
    {
        _starsWon = _starsWon < nbStars ? nbStars : _starsWon;
        if (save) SaveManager.SaveDataOfLevel(this);
    }

    public void SetStars(int nbStars)
    {
        _starsWon = nbStars;
    }

    public void Unlock(bool save = true)
    {
        _unlocked = true;
        if (save) SaveManager.SaveDataOfLevel(this);
    }

    public int GetID => _levelInfo.LevelID;
    public string GetScene => _levelInfo.LevelScene;
    public LevelInfo LevelInfo => _levelInfo;

    public void Lock()
    {
        _unlocked = false;
    }
}
