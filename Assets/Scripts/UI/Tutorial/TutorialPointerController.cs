using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointerController : MonoBehaviour
{
    public RectTransform circle;
    public RectTransform circle2;
    public GameObject frame;
    private bool enabled = false;

    private void OnEnable() {
        EventManager.StartListening("Tutorial_HighlightInput", HighlightInput);
        EventManager.StartListening("Tutorial_HighlightSlider", HighlightSlider);
        EventManager.StartListening("Tutorial_HighlightBack", HighlightBackButton);
        EventManager.StartListening("Tutorial_HighlightHelp", HighlightHelpButton);
        EventManager.StartListening("Tutorial_HighlightEquation", HighlightEquation);
        EventManager.StartListening("Tutorial_HighlightChanges", HighlightChanges);
        EventManager.StartListening("Tutorial_Finish", Finish);
        EventManager.StartListening("Tutorial_Start", StartTut);
        
    }
    private void OnDisable() {
        EventManager.StopListening("Tutorial_HighlightInput", HighlightInput);
        EventManager.StopListening("Tutorial_HighlightSlider", HighlightSlider);
        EventManager.StopListening("Tutorial_HighlightBack", HighlightBackButton);
        EventManager.StopListening("Tutorial_HighlightHelp", HighlightHelpButton);
        EventManager.StopListening("Tutorial_HighlightEquation", HighlightEquation);
        EventManager.StopListening("Tutorial_HighlightChanges", HighlightChanges);
        EventManager.StopListening("Tutorial_Finish", Finish);
        EventManager.StopListening("Tutorial_Start", StartTut);
    }

    private void HighlightInput()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }

        LeanTween.move(circle, new Vector3(-40,90,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(240,200), 0.5f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(circle2, new Vector3(-650,90,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle2, new Vector2(240,200), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightSlider()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }
        
        LeanTween.move(circle, new Vector3(70,90,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(240,200), 0.5f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(circle2, new Vector3(-540,90,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle2, new Vector2(240,200), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightBackButton()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }

        LeanTween.move(circle2, new Vector3(-200,-50,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(circle, new Vector3(188 ,115,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(153,136), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightHelpButton()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }

        LeanTween.move(circle2, new Vector3(-200,-50,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(circle, new Vector3(192 ,60,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(130,130), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightEquation()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }

        LeanTween.move(circle2, new Vector3(-200,-50,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween.move(circle, new Vector3(-294,90,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(700, 235), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightChanges()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }
        LeanTween.move(circle, new Vector3(-167,-246,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(450,142), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void Finish()
    {

        LeanTween.move(circle2, new Vector3(-200,-50,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.move(circle, new Vector3(-200,-50,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        
        //frame.SetActive(false);
    }

    private void StartTut()
    {
        enabled = true;
        frame.SetActive(true);
    }
}
