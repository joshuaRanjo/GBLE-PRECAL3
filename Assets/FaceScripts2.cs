using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FaceScripts2 : MonoBehaviour
{
        // Reference to the Image component
    public Image targetImage;

    // Array of sprites to switch between
    public Sprite[] sprites;
    private int curSprite = 0;
    private bool inPuzzle = false;
    private bool inHelp = false;

    /* 
    0 = sleepy
    1 = interest
    2 = awake/happy
    3 = teacher
    */
    private void OnEnable() {
        EventManager.StartListening("PuzzleHover",SetInterest);
        EventManager.StartListening("PuzzleHoverExit",SetSleepy);
        EventManager.StartListening("EnterPuzzle", SetHappy);
        EventManager.StartListening("ExitPuzzle", SetSleepy);
        EventManager.StartListening("EnterHelpMode", SetTeacher);
        EventManager.StartListening("ExitHelpMode", SetTeacher);
    }

    private void OnDisable() {
        EventManager.StopListening("PuzzleHover",SetInterest);
        EventManager.StopListening("PuzzleHoverExit",SetSleepy);
        EventManager.StopListening("EnterPuzzle", SetHappy);
        EventManager.StopListening("ExitPuzzle", SetSleepy);
        EventManager.StopListening("EnterHelpMode", SetTeacher);
        EventManager.StopListening("ExitHelpMode", SetTeacher);
    }

    private void SetHappy()
    {
        SetSprite(2);

        inPuzzle = true;
    }

    private void SetSleepy()
    {
        if(!inHelp)
            SetSprite(0);
        inPuzzle = false;
    }

    private void SetInterest()
    {
        if(!inPuzzle && !inHelp)
            SetSprite(1);
    }
    private void SetSprite(int face)
    {
        targetImage.sprite = sprites[face];
        curSprite = face;
    }

    private void SetTeacher()
    {
        if(inHelp)
        {
            inHelp = false;
            if(inPuzzle)
            {
                SetHappy();
            }
            else
            {
                SetSleepy();
            }
        }
        else
        {
            inHelp = true;
            SetSprite(3);
        }
    }
}
