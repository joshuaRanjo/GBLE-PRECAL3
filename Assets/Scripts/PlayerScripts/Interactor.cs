using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// Handles triggering interactable objects
/// 
/// </summary>


public class Interactor : MonoBehaviour
{

    private bool canInteract = true;
    //[SerializeField] private BoxCollider2D interactionCollider;
    private GameObject interactable;

    [SerializeField] private GameObject interactionArea;

    private void OnEnable() {
        EventManager.StartListening("EnterConversation", DisableInteractionBox);
        EventManager.StartListening("ExitConversation", EnableInteractionBox);

        EventManager.StartListening("EnterPuzzle",DisableInteractionBox);
        EventManager.StartListening("ExitPuzzle", EnableInteractionBox);
        
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", DisableInteractionBox);
        EventManager.StopListening("ExitConversation", EnableInteractionBox);

        EventManager.StopListening("EnterPuzzle", DisableInteractionBox);
        EventManager.StopListening("ExitPuzzle", EnableInteractionBox);
    }
    public void DoInteraction(InputAction.CallbackContext context){
        if(canInteract && (interactable != null) && context.performed){
            if(interactable.GetComponent<InteractableObject>() != null)
            {   Debug.Log("DOI 1");
                interactable.GetComponent<InteractableObject>().DoAction();
            }
            if(interactable.GetComponent<InteractableButton>() != null)
            {
                Debug.Log("DOI 2");
                interactable.GetComponent<InteractableButton>().DoAction();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.gameObject.CompareTag("Interactable")){
                canInteract = true;
                interactable = collision.gameObject;
            }
    }

    private void OnTriggerExit2D(Collider2D collision) {
            if(collision.gameObject.CompareTag("Interactable")){
                canInteract = false;
                interactable = null;
            }
    }

    private void DisableInteractionBox()
    {
        interactionArea.SetActive(false);
    }

    private void EnableInteractionBox()
    {
        interactionArea.SetActive(true);
    }
}