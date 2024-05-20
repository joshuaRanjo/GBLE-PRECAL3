using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform gridTransform;
    public Transform levelPropsTransform;
    public LevelManagerSO levelManagerSO;
    public PlayerData playerData;
    public string levelName;

    private void OnEnable() {
        EventManager.StartListening("EnterMainMenu", ClearLevel);

        levelManagerSO.levelChangeEvent.AddListener(LoadLevelSO);
    }
    private void OnDisable()
    {
        EventManager.StopListening("EnterMainMenu", ClearLevel);
        EventManager.StopListening("SaveComplete", NextLevel);

        levelManagerSO.levelChangeEvent.RemoveListener(LoadLevelSO);
    }

    public void LoadLevel(GameObject levelPrefab)
    {
        EventManager.TriggerEvent("ExitPuzzle");
        ClearLevel();

        GameObject instantiatedObj = Instantiate(levelPrefab,Vector3.zero, Quaternion.identity);
        instantiatedObj.transform.SetParent(gridTransform);

        playerData.currentLevel = levelPrefab.name;
        EventManager.TriggerEvent("LevelLoaded");
    }

    public void LoadLevelSO()
    {
        LoadLevel(levelManagerSO.levelList[levelManagerSO.currentLevel]);
    }

    public void ClearLevel()
    {
        ClearChildren(gridTransform);
        ClearChildren(levelPropsTransform);
    }

    private void ClearChildren(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount-1; i >= 0; i--)
        {

            DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }

    public void NextLevel()
    {
        EventManager.StopListening("SaveComplete", NextLevel);
        levelManagerSO.NextLevel();

    }

    public void ReloadLevel()
    {
        EventManager.TriggerEvent("LevelReloaded");
        levelManagerSO.ReloadLevel();
    }

    public void LevelCleared()
    {
        EventManager.StartListening("SaveComplete", NextLevel);
        EventManager.TriggerEvent("LevelComplete");
    }
}
