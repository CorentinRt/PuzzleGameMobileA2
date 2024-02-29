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
    [SerializeField,Scene] private string _globalScene;
    [SerializeField, Scene] private string _mainMenu;
    private int _currentLevelID;
    private bool isLevelLoaded;

    private LevelController _currentLC;
    public LevelController GetCurrentLevelController => _currentLC = _currentLC == null ? LevelController.Instance : _currentLC;
    //FindObjectsOfType<LevelController>().Where(x => x.gameObject.scene == behav.gameObject.scene).FirstOrDefault();

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

    public Level GetLevel(int id) => _levels.Find(x => x.GetID == id);
    public Level GetCurrentLevel() => GetLevel(_currentLevelID);

    public void UnlockLevel(int id) => GetLevel(id).Unlock();

    public void LoadLevel(int id)
    {
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
        SceneManager.LoadScene(_globalScene);
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

}

[Serializable]
public class Level
{
    [SerializeField, Expandable] private LevelInfo _levelInfo;
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
