using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private float hidePositionX;
    [SerializeField] private float moveSpeed;
    private bool puzzleMode = true;
    private Vector2 targetPosition;
    private RectTransform rectTransform;
    private void OnEnable() {
       EventManager.StartListening("EnterPuzzle", HideDevice);
       EventManager.StartListening("ExitPuzzle", ShowDevice);

       EventManager.TriggerEvent("EnterMainMenu");
    }

    private void OnDisable() {
        EventManager.StopListening("EnterPuzzle",HideDevice);
        EventManager.StopListening("ExitPuzzle", ShowDevice);
    }

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void ShowDevice()
    {
        if(!puzzleMode)
        {
            puzzleMode = true;

            targetPosition = new Vector2(-25,-25);
            StartCoroutine(MoveToTarget());
        }
    }

    private void HideDevice()
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

    public void ShowPauseScreen()
    {
        pausePanel.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        HidePauseScreen();
        EventManager.TriggerEvent("EnterMainMenu");
    }
}
