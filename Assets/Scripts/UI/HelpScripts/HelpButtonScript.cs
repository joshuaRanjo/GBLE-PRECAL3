using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonScript : MonoBehaviour
{
    [SerializeField] private Button helpButton;

    private void Start() {
        helpButton.onClick.AddListener(ShowHelp);
    }
    public void ShowHelp()
    {
        EventManager.TriggerEvent("EnterHelpMode");
        helpButton.onClick.RemoveListener(ShowHelp);
        helpButton.onClick.AddListener(HideHelp);
    }

    public void HideHelp()
    {
        EventManager.TriggerEvent("ExitHelpMode");
         helpButton.onClick.AddListener(ShowHelp);
         helpButton.onClick.RemoveListener(HideHelp);
    }
}
