using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMover : MonoBehaviour
{
    [SerializeField] private float hidePositionX;
    [SerializeField] private float moveSpeed;
    private bool puzzleMode = false;
    private Vector2 targetPosition;
    private RectTransform rectTransform;
    private void OnEnable() {
       EventManager.StartListening("EnterPuzzle", ShowDevice);
       EventManager.StartListening("ExitPuzzle", HideDevice);
    }

    private void OnDisable() {
        EventManager.StopListening("EnterPuzzle",ShowDevice);
        EventManager.StopListening("ExitPuzzle", HideDevice);
    }

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowDevice()
    {
        if(!puzzleMode)
        {
            puzzleMode = true;

            targetPosition = new Vector2(0,0);
            StartCoroutine(MoveToTarget());
        }
    }

    public void HideDevice()
    {
        if(puzzleMode)
        {
            puzzleMode = false;

            targetPosition = new Vector2(hidePositionX,0);
            StartCoroutine(MoveToTarget());
        }
    }

    IEnumerator MoveToTarget()
    {
        // Get the initial position of the UI element
        Vector2 startPosition = rectTransform.anchoredPosition;

        // Initialize the elapsed time
        float elapsedTime = 0f;

        while (rectTransform.anchoredPosition != targetPosition)
        {
            // Interpolate the position based on the elapsed time
            Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveSpeed);

            // Update the UI element's anchored position
            rectTransform.anchoredPosition = newPosition;

            // Increment the elapsed time by the time.deltaTime
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the UI element reaches the exact target position
        rectTransform.anchoredPosition = targetPosition;

        // Coroutine is done, you can perform any additional actions here

    }
}
