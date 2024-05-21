using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Data", menuName = "PlayerData")]
public class PlayerData: ScriptableObject
{
    public string name;
    public bool precisionMode = true; // leave True to always keep track of changeCount
    public string currentLevel;
    public int changeCount;
    public bool tutorial;
    public Dictionary<string, int> completedLevels;

    public void AddCompleteLevel()
    {
        if(!completedLevels.ContainsKey(currentLevel))
        {
            if(precisionMode)
            {
                completedLevels.Add(currentLevel, changeCount);
            }
            else
            {
                completedLevels.Add(currentLevel, -1);
            }
        }
        else
        {
            if(precisionMode)
            {
                if((completedLevels[currentLevel] > changeCount || completedLevels[currentLevel] < 0)  && changeCount > 0)
                {
                    completedLevels[currentLevel] = changeCount;
                }
            }
        }
        changeCount = 0;
        EventManager.TriggerEvent("SaveData");
    }


}