using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Data", menuName = "PlayerData")]
public class PlayerData: ScriptableObject
{
    public string name;
    public bool precisionMode = false;
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
                if(completedLevels[currentLevel] > changeCount)
                {
                    completedLevels[currentLevel] = changeCount;
                }
            }
        }
        changeCount = 0;
        EventManager.TriggerEvent("SaveData");
    }


}