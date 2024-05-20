using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSOManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private void OnEnable() 
    {
        EventManager.StartListening("LevelComplete", CompleteLevel);
        EventManager.StartListening("LevelReloaded", ResetChangeCount);
    }
    private void OnDisable()
    {
        EventManager.StopListening("LevelComplete", CompleteLevel);
    }

    private void CompleteLevel()
    {
        playerData.AddCompleteLevel();
    }

    private void ResetChangeCount()
    {
        playerData.changeCount = 0;
    }

    private void SetChangeCount(int newCount)
    {
        playerData.changeCount = newCount;
    }

    private void IncrementChangeCount()
    {
        playerData.changeCount++;
    }
}
