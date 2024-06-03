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
    [SerializeField] private int curSprite = 0;
    [SerializeField] private bool inPuzzle = false;
    [SerializeField] private bool inHelp = false;

    /* 
    0 = sleepy
    1 = interest
    2 = awake/happy
    3 = teacher
    */
    private void OnEnable() {
        EventManager.StartListening("PuzzleHover",SetInterest);
        EventManager.StartListening("PuzzleHoverExit",PuzzleHoverExit);
        EventManager.StartListening("EnterPuzzle", SetHappy);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
        EventManager.StartListening("EnterHelpMode", EnterHelpMode);
        EventManager.StartListening("ExitHelpMode", ExitHelpMode);
    }

    private void OnDisable() {
        EventManager.StopListening("PuzzleHover",SetInterest);
        EventManager.StopListening("PuzzleHoverExit",PuzzleHoverExit);
        EventManager.StopListening("EnterPuzzle", SetHappy);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);
        EventManager.StopListening("EnterHelpMode", EnterHelpMode);
        EventManager.StopListening("ExitHelpMode", ExitHelpMode);
    }

    private void SetHappy()
    {
        SetSprite(2);

        inPuzzle = true;
    }

    private void SetSleepy()
    {
        SetSprite(0);
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
        SetSprite(3);
    }

    private void ExitPuzzle()
    {
        if(!inHelp)
            SetSleepy();
        inPuzzle = false;
    }

    private void PuzzleHoverExit()
    {
        if(!inHelp)
        {
            SetSleepy();
        }
    }

    private void EnterHelpMode()
    {
        inHelp = true;
        SetTeacher();
    }

    private void ExitHelpMode()
    {
        inHelp = false;
        if(inPuzzle)
        {
            SetHappy();
        }
        else{
            SetSleepy();
        }
    }
}
