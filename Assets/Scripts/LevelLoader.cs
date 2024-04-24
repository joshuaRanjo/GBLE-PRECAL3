using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    public LevelManagerSO levelManagerSO;
    public LevelManager levelManager;
    public GameObject buttonPrefab;
    public GameObject scrollViewContent;

    private void Start()
    {
        ListPrefabsInFolder();
    }
     void ListPrefabsInFolder()
    {
        ClearScrollView();
        
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
    }

    private void OnButtonClick(GameObject prefab, int number)
    {
        levelManagerSO.SetCurrentLevel(number);
        Debug.Log("Clicked prefab " + prefab.name);
        EventManager.TriggerEvent("ExitMainMenu");
    }
}
