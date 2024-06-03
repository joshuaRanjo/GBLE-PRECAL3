using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelpScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panelObject;
    [SerializeField] private GameObject inputBlockerPanel;
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 originalPosition;
    [SerializeField] private Vector2 showPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool inHelp = false;
    private Vector2 targetPosition;
    
    
    private void Start() {
        //originalPosition = rectTransform.anchoredPosition;
    }
    private void OnEnable() {
        panel.alpha = 0;
        rectTransform.anchoredPosition = originalPosition;
        EventManager.StartListening("EnterHelpMode",ShowHelp);
        EventManager.StartListening("ExitHelpMode",HideHelp);
        EventManager.StartListening("EnterPuzzle",HideHelp);
    }
    private void OnDisable() {
        EventManager.StopListening("EnterHelpMode",ShowHelp);
        EventManager.StopListening("ExitHelpMode",HideHelp);
        EventManager.StopListening("EnterPuzzle",HideHelp);
    }


    private void ShowHelp()
    {   
        if(!inHelp)
        {
            inHelp = true;
            inputBlockerPanel.SetActive(true);
            panel.alpha = 0;
            panelObject.SetActive(true);
            panel.LeanAlpha(1,moveSpeed);
            LeanTween.move(rectTransform, showPosition, moveSpeed).setEase(LeanTweenType.easeInOutQuad);
        }
        
    }

    private void HideHelp()
    {
        if(inHelp)
        {
            inHelp = false;
            inputBlockerPanel.SetActive(false);

            panel.LeanAlpha(0, moveSpeed);

            LeanTween.move(rectTransform, originalPosition, moveSpeed).setEase(LeanTweenType.easeInOutQuad).setOnComplete(OnComplete);
        }
        
    }

    void OnComplete()
    {
        panelObject.SetActive(false);
    }

}
