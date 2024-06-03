using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput controller;
    [SerializeField] private bool storyConversation = false;
    [SerializeField]private bool isPaused = false;
    [SerializeField]private bool inGame = false;
    [SerializeField]private bool inPuzzle = false;
    [SerializeField]private bool inHelp = false;

    private void Start() {
        Debug.developerConsoleVisible = true;
        DisableMovement();
    }

    private void OnEnable() {
        EventManager.StartListening("EnterConversation", SwitchToConversation);
        EventManager.StartListening("ExitConversation", SwitchToPlayerMovement);

        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
        
        EventManager.StartListening("DisableMovement", DisableMovement);
        EventManager.StartListening("EnableMovement", EnableMovement);

        EventManager.StartListening("EnterMainMenu", EnterMainMenu);
        EventManager.StartListening("ExitMainMenu", ExitMainMenu);
        
        EventManager.StartListening("EnterHelpMode",EnterHelpMode);
        EventManager.StartListening("ExitHelpMode",ExitHelpMode);

        EventManager.StartListening("PauseGame", PauseGame);
        EventManager.StartListening("ResumeGame", ResumeGame);  
        controller.actions.FindActionMap("Pause").Enable();
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", SwitchToConversation);
        EventManager.StopListening("ExitConversation", SwitchToPlayerMovement);

        EventManager.StopListening("EnterPuzzle", EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);

        EventManager.StopListening("DisableMovement", DisableMovement);
        EventManager.StopListening("EnableMovement", EnableMovement);

        EventManager.StopListening("EnterMainMenu", EnterMainMenu);
        EventManager.StopListening("ExitMainMenu", ExitMainMenu);

        EventManager.StopListening("EnterHelpMode",EnterHelpMode);
        EventManager.StopListening("ExitHelpMode",ExitHelpMode);

        EventManager.StopListening("PauseGame", PauseGame);
        EventManager.StopListening("ResumeGame", ResumeGame);  
        
    }

    public void SwitchToConversation()
    {
        controller.actions.FindActionMap("PlayerMovement").Disable();
        controller.actions.FindActionMap("Conversation").Enable();
    }

    public void SwitchToPlayerMovement()
    {  
            controller.actions.FindActionMap("PlayerMovement").Enable();
            controller.actions.FindActionMap("Conversation").Disable();
    }

    public void DisableMovement()
    {
        controller.actions.FindActionMap("PlayerMovement").Disable();
    }

    public void EnableMovement()
    {
        controller.actions.FindActionMap("PlayerMovement").Enable();
    }

    public void SwitchToStoryMoment()
    {
        storyConversation = true;
    }

    public void SwitchOutOfStoryMoment()
    {
        storyConversation = false;
    }

    public void EnterMainMenu()
    {
        
        DisableMovement();
        inGame = false;
        isPaused = false;
    }

    public void ExitMainMenu()
    {
        Debug.Log("exitedMainMenu");
        EnableMovement();
        inGame = true;
        isPaused = false;
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        
        if (!context.started) return;
        if(inGame && !inPuzzle && !inHelp)
        {   
            if(isPaused)
            {
                isPaused = false;
                EventManager.TriggerEvent("ResumeGame");
            }
            else
            {
                isPaused = true;
                EventManager.TriggerEvent("PauseGame");
            }
        }   
        if(inGame && inHelp)
        {
            EventManager.TriggerEvent("ExitHelpMode");
        }
    }

    private void PauseGame()
    {
        DisableMovement();
        isPaused = true;
    }

    private void ResumeGame()
    {
        EnableMovement();
        isPaused = false;
    }

    private void EnterPuzzle()
    {
        SwitchToConversation();
        inPuzzle = true;
        controller.actions.FindActionMap("Pause").Disable();
    }

    private void ExitPuzzle()
    {
        SwitchToPlayerMovement();
        inPuzzle = false;
        controller.actions.FindActionMap("Pause").Enable();
    }

    public void ExitPuzzleMode(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        EventManager.TriggerEvent("ExitPuzzle");
    }

    public void EnterHelpMode()
    {
        controller.actions.FindActionMap("Pause").Disable();
        inHelp = true;
        
        if(inPuzzle)
        {
            controller.actions.FindActionMap("Conversation").Disable();
        }
        StartCoroutine(DisableActionCoroutine());
    }

    private IEnumerator DisableActionCoroutine()
    {
        
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        controller.actions.FindActionMap("Pause").Enable();
    }

    public void ExitHelpMode()
    {
        if(!inPuzzle)
        {
            SwitchToPlayerMovement();
        }
        else
        {
            controller.actions.FindActionMap("Pause").Disable();
            controller.actions.FindActionMap("Conversation").Enable();
        }
        inHelp = false;
    }
}
