using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void SwitchToConversation()
    {
        controller.actions.FindActionMap("PlayerMovement").Disable();
        controller.actions.FindActionMap("Conversation").Enable();
    }

    private void SwitchToPlayerMovement()
    {
        controller.actions.FindActionMap("PlayerMovement").Enable();
        controller.actions.FindActionMap("Conversation").Disable();
    }
}
