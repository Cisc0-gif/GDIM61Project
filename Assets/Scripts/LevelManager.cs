using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager 
{
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new LevelManager();
            }
            return _instance;
        }
    }



    public Dictionary<int, bool> CompletedLevels { get; private set; }

    private LevelManager()
    {
        CompletedLevels = new Dictionary<int, bool>();
        ResetLevelProgress();
        
    }
    
    public void ChangeState(int LevelIndex, bool IsCompleted)
    {
        if(CompletedLevels.ContainsKey(LevelIndex))
        {
            CompletedLevels[LevelIndex] = IsCompleted;
        }
        else if(LevelIndex < SceneManager.sceneCount)
        {
            CompletedLevels.Add(LevelIndex, IsCompleted);
        }
    }

    public void ResetLevelProgress()
    {
        CompletedLevels?.Clear();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            CompletedLevels.Add(i, false);
        }
    }
}
