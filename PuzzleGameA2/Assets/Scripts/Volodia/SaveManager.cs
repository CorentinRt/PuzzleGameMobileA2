using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    public static List<LevelSavedData> LoadData(List<Level> levels)
    {
        List<LevelSavedData> levelsData = new List<LevelSavedData>(0);
        foreach (Level level in levels)
        {
            LevelSavedData lvlData = LoadDataFromID(level.GetID);
            if (!lvlData.Unlocked) return levelsData;
            levelsData.Add(LoadDataFromID(level.GetID));
        }
        Debug.Log("Data has been saved");
        Debug.Log(levelsData);
        return levelsData;
    }

    public static void SaveData(List<Level> levels)
    {
        foreach (Level level in levels)
        {
            if (!level.isUnlocked) return;
            PlayerPrefs.SetInt("Level"+level.GetID, level.GetStarsNum);
        }
        Debug.Log("Data has been saved");
        PlayerPrefs.Save();
    }

    public static void SaveDataOfLevel(Level level)
    {
        if (!level.isUnlocked) return;
        PlayerPrefs.SetInt("Level"+level.GetID, level.GetStarsNum);
    }

    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        Debug.Log("Data has been Deleted");
        Debug.Log(PlayerPrefs.HasKey("Level" + 1));
    }

    private static LevelSavedData LoadDataFromID(int levelID)
    {
        LevelSavedData levelSavedData = new LevelSavedData();
        levelSavedData.ID = levelID;
        levelSavedData.Unlocked = PlayerPrefs.HasKey("Level" + levelID);
        levelSavedData.Stars = 0;
        if (!levelSavedData.Unlocked) return levelSavedData;
        int data = PlayerPrefs.GetInt("Level" + levelID);
        levelSavedData.Stars = data;
        return levelSavedData;
    }
}

public class LevelSavedData
{
    private int _id;
    private bool _unlocked;
    private int _stars;

    public int Stars{set => _stars = value; get => _stars;}
    public int ID{set => _id = value; get => _id;}
    public bool Unlocked{set => _unlocked = value; get => _unlocked;}
}