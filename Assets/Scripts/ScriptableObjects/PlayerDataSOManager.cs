using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSOManager : MonoBehaviour
{

    // This class is mostly used to listen for events to modify the PlayerData Scriptable Object
    [SerializeField] private PlayerData playerData;
    private void OnEnable() 
    {
        EventManager.StartListening("LevelComplete", CompleteLevel);
        EventManager.StartListening("LevelReloaded", ResetChangeCount);
        EventManager.StartListening("LevelLoaded", ResetChangeCount);
        EventManager.StartListening("ChangeEvent", IncrementChangeCount);
    }
    private void OnDisable()
    {
        EventManager.StopListening("LevelComplete", CompleteLevel);
        EventManager.StopListening("LevelReloaded", ResetChangeCount);
        EventManager.StopListening("LevelLoaded", ResetChangeCount);
        EventManager.StopListening("ChangeEvent", IncrementChangeCount);

        playerData.precisionMode = false;
    }

    private void CompleteLevel()
    {
        playerData.AddCompleteLevel();
    }

    private void ResetChangeCount()
    {
        playerData.changeCount = 0;
        EventManager.TriggerEvent("ChangeCounterUpdate");
    }

    private void SetChangeCount(int newCount)
    {
        playerData.changeCount = newCount;
        EventManager.TriggerEvent("ChangeCounterUpdate");
    }

    private void IncrementChangeCount()
    {
        playerData.changeCount = playerData.changeCount + 1;
        EventManager.TriggerEvent("ChangeCounterUpdate");
    }

    public void SetPrecisionMode(bool newBool)
    {
        Debug.Log("Setting Precision Mode " + newBool);
        playerData.precisionMode = newBool;
    }
}
