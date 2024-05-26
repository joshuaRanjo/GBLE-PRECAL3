using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableButton : LevelProp
{

    public bool isInRange;
    public UnityEvent interaction;

    [SerializeField] private GameObject redObject;
    [SerializeField] private GameObject eInteractor;
    private InputAction.CallbackContext context;

    private BoxCollider2D boxCollider;
    private bool interactable = true;



    public void DoAction(){
        if(interactable)
        {
            Debug.Log("DoingAction");
            interaction.Invoke();
            interactable = false;
            StartCoroutine(ButtonActivateCoroutine());
            StartCoroutine(ButtonAnimation());
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Interactor")){
            isInRange = true;
            eInteractor.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Interactor")){
            isInRange = false;
            eInteractor.SetActive(false);
        }
    }

    private void OnEnable() {
        boxCollider = GetComponent<BoxCollider2D>();

        EventManager.StartListening("EnterConversation", DisableCollider);
        EventManager.StartListening("ExitConversation", EnableCollider);

        EventManager.StartListening("EnterPuzzle",DisableCollider);
        EventManager.StartListening("ExitPuzzle", EnableCollider);
        
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", DisableCollider);
        EventManager.StopListening("ExitConversation", EnableCollider);

        EventManager.StopListening("EnterPuzzle", DisableCollider);
        EventManager.StopListening("ExitPuzzle", EnableCollider);
    }

    private void EnableCollider()
    {
        boxCollider.enabled = true;
    }
    private void DisableCollider()
    {
        boxCollider.enabled = false;
    }

    IEnumerator ButtonActivateCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        interactable = true;
    }
    IEnumerator ButtonAnimation()
    {
        DisableCollider();
        Vector3 downPosition = new Vector3(0f,0.6f,0f);
        Vector3 upPosition = new Vector3(0f,0.65f,0f);
        Vector3 startPosition = redObject.transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < 0.3f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / 0.5f);
            redObject.transform.localPosition = Vector3.Lerp(startPosition, downPosition, t);

            yield return null;
        }

        redObject.transform.localPosition = downPosition;

        elapsedTime = 0f;
        while (elapsedTime < 0.3f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / 0.5f);
            redObject.transform.localPosition = Vector3.Lerp(downPosition, upPosition, t);

            yield return null;
        }

        redObject.transform.localPosition = upPosition;
        EnableCollider();
    }
}