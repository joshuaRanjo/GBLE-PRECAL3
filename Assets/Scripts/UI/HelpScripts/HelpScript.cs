using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> gameUIObjects;

    [SerializeField] List<GameObject> helpUIComponents;
    [SerializeField] GameObject circlePage;
    [SerializeField] GameObject ellipsePage;
    [SerializeField] GameObject parabolaPage;
    [SerializeField] GameObject hyperbolaPage;

    private List<GameObject> pageList;
    private int currentPage = 0;

    private void Start() {
        pageList = new List<GameObject>();
        pageList.Add (circlePage);
        pageList.Add (ellipsePage);
        pageList.Add (parabolaPage);
        pageList.Add (hyperbolaPage);
        currentPage = 0;
    }
    private void OnEnable() {
       

        EventManager.StartListening("EnterHelpMode",ShowHelp);
        EventManager.StartListening("ExitHelpMode",HideHelp);
        EventManager.StartListening("EnterPuzzleMode",HideHelp);
    }
    private void OnDisable() {
        EventManager.StopListening("EnterHelpMode",ShowHelp);
        EventManager.StopListening("ExitHelpMode",HideHelp);
        EventManager.StopListening("EnterPuzzleMode",ShowHelp);
    }

    private void NextPage()
    {
        if(currentPage > 2)
        {
            currentPage = 0;
        }
        else
        {
            currentPage++;
        }
        UpdatePage();
    }

    private void PreviousPage()
    {
        if(currentPage < 1)
        {
            currentPage = 3;
        }
        else
        {
            currentPage--;
        }
        UpdatePage();
    }

    private void UpdatePage()
    {
        DisableAllPages();
        pageList[currentPage].SetActive(true);
    }

    private void DisableAllPages()
    {
        foreach(GameObject page in pageList)
        {
            page.SetActive(false);
        }
    }

    private void ShowHelp()
    {
        currentPage = 0;
        DisableAllPages();
        UpdatePage();
        SetActiveGameUI(false, gameUIObjects);
        SetActiveGameUI(true, helpUIComponents);
    }

    private void HideHelp()
    {
        SetActiveGameUI(false, helpUIComponents);
        SetActiveGameUI(true, gameUIObjects);
    }

    private void SetActiveGameUI(bool active, List<GameObject> objs)
    {
        foreach (GameObject uiObj in objs)
        {
            uiObj.SetActive(active);
        }
    }
}
