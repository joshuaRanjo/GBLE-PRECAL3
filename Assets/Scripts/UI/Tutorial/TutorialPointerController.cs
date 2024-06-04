using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointerController : MonoBehaviour
{
    public RectTransform circle;
    public GameObject frame;
    private bool enabled = false;

    private void OnEnable() {
        EventManager.StartListening("Tutorial_HighlightInput", HighlightInput);
        EventManager.StartListening("Tutorial_HighlightSlider", HighlightSlider);
        EventManager.StartListening("Tutorial_HighlightBack", HighlightBackButton);
        EventManager.StartListening("Tutorial_HighlightHelp", HighlightHelpButton);
        EventManager.StartListening("Tutorial_HighlightEquation", HighlightEquation);
        EventManager.StartListening("Tutorial_Finish", Finish);
        EventManager.StartListening("Tutorial_Start", StartTut);
        
    }
    private void OnDisable() {
        EventManager.StopListening("Tutorial_HighlightInput", HighlightInput);
        EventManager.StopListening("Tutorial_HighlightSlider", HighlightSlider);
        EventManager.StopListening("Tutorial_HighlightBack", HighlightBackButton);
        EventManager.StopListening("Tutorial_HighlightHelp", HighlightHelpButton);
        EventManager.StopListening("Tutorial_HighlightEquation", HighlightEquation);
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

        LeanTween.move(circle, new Vector3(-217,-70,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(250,400), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightSlider()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }
        LeanTween.move(circle, new Vector3(-108,-70,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(250,400), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightBackButton()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }
        LeanTween.move(circle, new Vector3(-5,260,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(130,195), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightHelpButton()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }
        LeanTween.move(circle, new Vector3(-5,170,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(130,130), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void HighlightEquation()
    {
        if(!enabled)
        {
            enabled = true;
            frame.SetActive(true);
        }
        LeanTween.move(circle, new Vector3(-167,110,0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.size(circle, new Vector2(450,220), 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void Finish()
    {
        frame.transform.localPosition = new Vector2(-367,-14);
        frame.SetActive(false);
    }

    private void StartTut()
    {
        enabled = true;
        frame.SetActive(true);
    }
}
