using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private CanvasGroup panelBG;
    [SerializeField] private RectTransform pauseTransform;
    [SerializeField] private float hidePositionX;
    [SerializeField] private float moveSpeed;
    [SerializeField]private bool puzzleMode = false;
    [SerializeField]private bool inHelp = false;
    private Vector2 targetPosition;
    private RectTransform rectTransform;
    private bool animating = false;
    [SerializeField] private Vector2 originalPosition;

    private void OnEnable() {
        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);

        EventManager.StartListening("PauseGame", ShowPauseScreen);
        EventManager.StartListening("ResumeGame", HidePauseScreen);  

        EventManager.StartListening("EnterHelpMode", EnterHelpMode);
        EventManager.StartListening("ExitHelpMode", ExitHelpMode);

        EventManager.StartListening("UnHidePause", ShowDevice);
        EventManager.StartListening("HidePause", HideDevice);  

    }

    private void OnDisable() {
        EventManager.StopListening("EnterPuzzle",EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);

        EventManager.StopListening("PauseGame", ShowPauseScreen);
        EventManager.StopListening("ResumeGame", HidePauseScreen);

        EventManager.StopListening("EnterHelpMode", EnterHelpMode);
        EventManager.StopListening("ExitHelpMode", ExitHelpMode);

        EventManager.StopListening("UnHidePause", ShowDevice);
        EventManager.StopListening("HidePause", HideDevice);  
    }

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    private void ShowDevice()
    {
            targetPosition = originalPosition;
            StartCoroutine(MoveToTarget());
    }

    private void HideDevice()
    {
            targetPosition = new Vector2(rectTransform.anchoredPosition.x,hidePositionX);
            StartCoroutine(MoveToTarget());
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
        //Debug.Log("ShowPauseScreen");
        if(pausePanel != null)
        {
            panelBG.alpha = 0;
            pausePanel.SetActive(true);
            panelBG.LeanAlpha(1,0.5f);

            pauseTransform.localPosition = new Vector2(0, -Screen.height*2);
            LeanTween.move(pauseTransform, new Vector3(0,0,0), moveSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(DonePauseAnimation);
        }
            
    }

    public void HidePauseScreen()
    {
         //Debug.Log("ShowHideScreen");
        if(pausePanel != null)
        {
            panelBG.LeanAlpha(0, 0.5f);
            LeanTween.move(pauseTransform, new Vector3(0,-Screen.height*2,0), moveSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(DisablePausePanel);
            
        }
    }

    void DisablePausePanel()
    {
        pausePanel.SetActive(false);
        DonePauseAnimation();
    }

    private void DonePauseAnimation()
    {
        EventManager.TriggerEvent("PauseDoneAnimating");
    }


    public void PauseGame()
    {
        EventManager.TriggerEvent("PauseGame");
    }

    public void ResumeGame()
    {
        EventManager.TriggerEvent("ResumeGame");
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

    private void EnterPuzzle()
    {
        puzzleMode = true;
        HideDevice();
    }
    private void ExitPuzzle()
    {
        puzzleMode = false;
        if(!inHelp)
        {
            ShowDevice();
        }

    }

    private void EnterHelpMode()
    {
        inHelp = true;
        HideDevice();
    }

    private void ExitHelpMode()
    {
        inHelp = false;
        if(!puzzleMode)
        {
            ShowDevice();
        }
    }
}
