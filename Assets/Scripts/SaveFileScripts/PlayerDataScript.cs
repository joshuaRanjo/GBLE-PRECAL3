using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    [SerializeField] private PlayerData playerDataSO;
    public string name;

    public Dictionary<string,int> completedLevels;
    public string currentLevel;
    public int moveCount;
    
    public bool precisionMode = false;

    private void Start() {
        LoadPlayerData();
    }

    private void OnEnable() {
        EventManager.StartListening("SaveData", SavePlayerData);
    }
    private void OnDisable()
    {
        EventManager.StopListening("SaveData",SavePlayerData);
    }

    public void LoadPlayerData(string name)
    {
        this.name = name;
        LoadPlayerData();
    }
    public void LoadPlayerData()
    {
        name = "Apollo";
        try
        {
            //Debug.Log("Trying to load save");
            string filePath = Application.persistentDataPath+ "/"+ name + ".json";
            string pDataString = System.IO.File.ReadAllText(filePath);
            Debug.Log(filePath);
            PlayerDataFile pData = JsonUtility.FromJson<PlayerDataFile>(pDataString);

            if(pData.completedLevels.Count == pData.changeCount.Count)
            {
                completedLevels = new Dictionary<string,int>();
                int j = 0;
                foreach(string levelName in pData.completedLevels)
                {
                    completedLevels.Add(levelName, pData.changeCount[j]);
                    j++;
                }
                if(playerDataSO.completedLevels == null)
                {
                    playerDataSO.completedLevels = new Dictionary<string,int>();
                }
                playerDataSO.completedLevels = completedLevels;
                
                //Debug.Log("Player data loaded");

            }
        }
        catch(System.IO.FileNotFoundException)
        {
            //Debug.LogWarning("NoSaveFile found. Making new save");
            playerDataSO.completedLevels = new Dictionary<string,int>();
            
            SavePlayerData();
        }
        EventManager.TriggerEvent("LoadedPlayerData");
    }

    public void SavePlayerData()
    {
        //Debug.Log("Trying to save");
        PlayerDataFile pData = new PlayerDataFile(name, playerDataSO.completedLevels);
        string json = JsonUtility.ToJson(pData);
        string filePath = Application.persistentDataPath+ "/"+ name + ".json";
        System.IO.File.WriteAllText(filePath, json);
        //Debug.Log("Save Success");
        EventManager.TriggerEvent("SaveComplete");
    }

    public void CompletedLevel()
    {
        if(!precisionMode)
        {
            moveCount = -1;
        }
        //Add to dictionary
        if(completedLevels.ContainsKey(currentLevel))
        { 
            if(moveCount < completedLevels[currentLevel])
                completedLevels[currentLevel] = moveCount;
        }
        else
        {
            completedLevels.Add(currentLevel,moveCount);
        }
    } 
}

[System.Serializable]
public class PlayerDataFile
{
    public string name;
    public List<string> completedLevels;
    public List<int> changeCount;

    public PlayerDataFile(string name, Dictionary<string,int> completedLevelsDictionary)
    {
        this.name = name;
        completedLevels = new List<string>();
        changeCount = new List<int>();

        foreach(KeyValuePair<string,int> kvp in completedLevelsDictionary)
        {
            completedLevels.Add(kvp.Key);
            changeCount.Add(kvp.Value);
        }
    }
}
