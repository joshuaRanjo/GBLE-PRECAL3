using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput controller;
    [SerializeField] private bool storyConversation = false;

    private void Start() {
        DisableMovement();
    }

    private void OnEnable() {
        EventManager.StartListening("EnterConversation", SwitchToConversation);
        EventManager.StartListening("ExitConversation", SwitchToPlayerMovement);

        EventManager.StartListening("EnterPuzzle", SwitchToConversation);
        EventManager.StartListening("ExitPuzzle", SwitchToPlayerMovement);
        
        EventManager.StartListening("DisableMovement", DisableMovement);
        EventManager.StartListening("EnableMovement", EnableMovement);

        EventManager.StartListening("EnterMainMenu", DisableMovement);
        EventManager.StartListening("ExitMainMenu", EnableMovement);
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", SwitchToConversation);
        EventManager.StopListening("ExitConversation", SwitchToPlayerMovement);

        EventManager.StopListening("EnterPuzzle", SwitchToConversation);
        EventManager.StopListening("ExitPuzzle", SwitchToPlayerMovement);

        EventManager.StopListening("DisableMovement", DisableMovement);
        EventManager.StopListening("EnableMovement", EnableMovement);

        EventManager.StopListening("EnterMainMenu", DisableMovement);
        EventManager.StopListening("ExitMainMenu", EnableMovement);
    }

    public void SwitchToConversation()
    {
        controller.actions.FindActionMap("PlayerMovement").Disable();
        controller.actions.FindActionMap("Conversation").Enable();
    }

    public void SwitchToPlayerMovement()
    {
        if(!storyConversation)
        {   
            controller.actions.FindActionMap("PlayerMovement").Enable();
            controller.actions.FindActionMap("Conversation").Disable();
        }
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


}
