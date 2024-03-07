using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput controller;

    private void OnEnable() {
        EventManager.StartListening("EnterConversation", SwitchToConversation);
        EventManager.StartListening("ExitConversation", SwitchToPlayerMovement);

        EventManager.StartListening("EnterPuzzle", SwitchToConversation);
        EventManager.StartListening("ExitPuzzle", SwitchToPlayerMovement);
        
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", SwitchToConversation);
        EventManager.StopListening("ExitConversation", SwitchToPlayerMovement);

        EventManager.StopListening("EnterPuzzle", SwitchToConversation);
        EventManager.StopListening("ExitPuzzle", SwitchToPlayerMovement);
    }

    public void SwitchToConversation()
    {
        Debug.Log("SWitchtoConversation");
        controller.actions.FindActionMap("PlayerMovement").Disable();
        controller.actions.FindActionMap("Conversation").Enable();
    }

    public void SwitchToPlayerMovement()
    {
        var sayDialog = Fungus.SayDialog.GetSayDialog(); 
        if(!sayDialog.isActiveAndEnabled)
        {
            Debug.Log("SwitchToMove");
            controller.actions.FindActionMap("PlayerMovement").Enable();
            controller.actions.FindActionMap("Conversation").Disable();
        }
    }
}
