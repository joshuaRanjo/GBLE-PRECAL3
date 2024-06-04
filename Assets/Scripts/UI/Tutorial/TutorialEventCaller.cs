using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TutorialEventCaller : MonoBehaviour
{
    [SerializeField] Flowchart fc;
    private bool tutorialStart = false;
    public void HighlightInput()
    {
        EventManager.TriggerEvent("Tutorial_HighlightInput");
    }

    public void HighlightSlider()
    {
        EventManager.TriggerEvent("Tutorial_HighlightSlider");
    }

    public void HighlightBack()
    {
        EventManager.TriggerEvent("Tutorial_HighlightBack");
    }

    public void HighlightHelp()
    {
        EventManager.TriggerEvent("Tutorial_HighlightHelp");
    }

    public void HighlightEquation()
    {
        EventManager.TriggerEvent("Tutorial_HighlightEquation");
    }

    public void FinishTutorial()
    {
        EventManager.TriggerEvent("Tutorial_Finish");
    }

    public void StartTutorial()
    {
        if(!tutorialStart)
        {
            tutorialStart = true;
            EventManager.TriggerEvent("Tutorial_Start");
            fc.ExecuteBlock("BeginTutorial");
        }

    }
    
    
}
