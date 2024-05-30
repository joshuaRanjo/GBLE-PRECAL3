using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Level Manager Scriptable Object", menuName = "LevelManagerSO")]
public class LevelManagerSO: ScriptableObject
{

    public List<GameObject> levelList;

    public int currentLevel;
    public string currentLevelName;
    [System.NonSerialized]
    public UnityEvent levelChangeEvent = new UnityEvent();
    public void SetLevelList(List<GameObject> levelList)
    {
        this.levelList = levelList;
    }

    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
        levelChangeEvent.Invoke();
    }

    public void SetCurrentLevelNoChange(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }

    public void NextLevel()
    {
        currentLevel++;
        if(currentLevel <= levelList.Count - 1)
        {
            levelChangeEvent.Invoke();
        }
        else
        {
            EventManager.TriggerEvent("EnterMainMenu");
        }      
    }

    public void ReloadLevel()
    {
        levelChangeEvent.Invoke();
    }

}