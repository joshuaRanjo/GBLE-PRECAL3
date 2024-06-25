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
    public string currentLevelDescription;
    [System.NonSerialized]
    public UnityEvent levelChangeEvent = new UnityEvent();
    public void SetLevelList(List<GameObject> levelList)
    {
        this.levelList = levelList;
    }

    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
        LevelDetails ld = levelList[currentLevel].GetComponent<LevelDetails>();
        currentLevelDescription = ld.levelIntro;
        currentLevelName = ld.name;
        levelChangeEvent.Invoke();
    }

    public void SetCurrentLevelNoChange(int currentLevel)
    {
        this.currentLevel = currentLevel;
        LevelDetails ld = levelList[currentLevel].GetComponent<LevelDetails>();
        currentLevelDescription = ld.levelIntro;
        currentLevelName = ld.name;
    }

    public void NextLevel()
    {
        currentLevel++;
        if(currentLevel <= levelList.Count - 1)
        {
            LevelDetails ld = levelList[currentLevel].GetComponent<LevelDetails>();
            currentLevelDescription = ld.levelIntro;
            currentLevelName = ld.name;
            levelChangeEvent.Invoke();
        }
        else
        {
            EventManager.TriggerEvent("EnterMainMenu");
        }      
    }

    public void ReloadLevel()
    {
        Debug.Log("LevelReloaded");
        levelChangeEvent.Invoke();
    }

}