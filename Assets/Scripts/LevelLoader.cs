using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Fungus;

public class LevelLoader : MonoBehaviour
{
    public PlayerData playerData;
    public LevelManagerSO levelManagerSO;
    public LevelManager levelManager;
    public GameObject buttonPrefab;
    public GameObject scrollViewContent;
    public Flowchart fc;
    private Dictionary<string,int> completedLevelList;

    
    private void OnEnable()
    {
        EventManager.StartListening("LoadedPlayerData", ListPrefabsInFolder);
    }

    private void OnDisable()
    {
        EventManager.StopListening("LoadedPlayerData", ListPrefabsInFolder);

    }
     void ListPrefabsInFolder()
    {
        ClearScrollView();
        //Debug.Log("Listing prefabs");
        // Load all prefabs from the specified folder
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Levels");
        int k = 0;
        foreach (GameObject prefab in prefabs)
        {
            InstantiateButton(prefab,k);
            k++;
        }
        List<GameObject> list = new List<GameObject>();
        list.AddRange(prefabs);
        levelManagerSO.SetLevelList(list);
        
        //EventManager.StopListening("LoadedPlayerData", ListPrefabsInFolder);
        EventManager.StopListening("EnterMainMenu",ListPrefabsInFolder);
        EventManager.StartListening("EnterMainMenu",ListPrefabsInFolder);
    }

     void ClearScrollView()
    {
        foreach(Transform child in scrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

     void InstantiateButton(GameObject prefab, int number)
    {
        // Create a new button GameObject
        GameObject buttonGO = Instantiate(buttonPrefab,scrollViewContent.transform);
        buttonGO.transform.SetParent(scrollViewContent.transform);

        TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = prefab.name;
            //Debug.Log(buttonText.text);
        }

        // Add a click listener to the button to instantiate the prefab
        Button button = buttonGO.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClick(prefab, number));
        }

        LevelSelectionButton buttonScript = buttonGO.GetComponent<LevelSelectionButton>();  
        

            if(playerData.completedLevels.TryGetValue(prefab.name, out int value))
            {
                
                buttonScript.EnableCheckMark();
                if(value > 0)
                {
                    buttonScript.SetMoveCount(value);
                }
                else
                {
                    buttonScript.SetMoveCount(-1);
                }
                //Debug.Log("Got a completed level " + prefab.name + " " +playerData.completedLevels[prefab.name]);
            }
            else
            {
                buttonScript.SetMoveCount(-1);
            }


    }

    private void OnButtonClick(GameObject prefab, int number)
    {
        levelManagerSO.SetCurrentLevelNoChange(number);
        fc.ExecuteBlock("BeginLevel");
        //EventManager.TriggerEvent("ExitMainMenu");
    }

    public void LevelButtonClick()
    {
        EventManager.TriggerEvent("ExitMainMenu");
    }
}
