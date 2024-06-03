using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonScript : MonoBehaviour
{
    [SerializeField] private Button helpButton;
    private void OnEnable() {
        EventManager.StartListening("ExitHelpMode", ExitHelp);
    }
    private void OnDisable() {
        EventManager.StopListening("ExitHelpMode", ExitHelp);
    }
    private void Start() {
        helpButton.onClick.AddListener(ShowHelp);
    }
    public void ShowHelp()
    {
        EventManager.TriggerEvent("EnterHelpMode");
        DisableButtonTemporarily();
        helpButton.onClick.RemoveListener(ShowHelp);
        helpButton.onClick.AddListener(HideHelp);
    }

    public void HideHelp()
    {
        EventManager.TriggerEvent("ExitHelpMode");
        
    }

    private void ExitHelp()
    {
        DisableButtonTemporarily();
        helpButton.onClick.AddListener(ShowHelp);
         helpButton.onClick.RemoveListener(HideHelp);
    }

    public void DisableButtonTemporarily()
    {
        StartCoroutine(DisableButtonCoroutine());
    }

    private IEnumerator DisableButtonCoroutine()
    {
        helpButton.interactable = false; // Make the button uninteractable
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        helpButton.interactable = true; // Make the button interactable again
    }
}
