using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    private void OnEnable() 
    {
        EventManager.StartListening("EnterMainMenu", ShowMainMenu);
        EventManager.StartListening("ExitMainMenu", HideMainMenu);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnterMainMenu", ShowMainMenu);
        EventManager.StopListening("ExitMainMenu", HideMainMenu);
    }

    private void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        Debug.Log("ShowMainMenu");
    }

    private void HideMainMenu()
    {
        mainMenuCanvas.SetActive(false);
    }
}
