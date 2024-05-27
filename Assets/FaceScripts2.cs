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

    /* 
    0 = sleepy
    1 = interest
    2 = awake/happy
    */
    private void OnEnable() {
        EventManager.StartListening("PuzzleHover",SetInterest);
        EventManager.StartListening("PuzzleHoverExit",SetSleepy);
        EventManager.StartListening("EnterPuzzle", SetHappy);
        EventManager.StartListening("ExitPuzzle", SetSleepy);
    }

    private void OnDisable() {
        EventManager.StopListening("PuzzleHover",SetInterest);
        EventManager.StopListening("PuzzleHoverExit",SetSleepy);
        EventManager.StopListening("EnterPuzzle", SetHappy);
        EventManager.StopListening("ExitPuzzle", SetSleepy);
    }

    private void SetHappy()
    {
        SetSprite(2);
    }

    private void SetSleepy()
    {
        SetSprite(0);
    }

    private void SetInterest()
    {
        SetSprite(1);
    }
    private void SetSprite(int face)
    {
        targetImage.sprite = sprites[face];
        curSprite = face;
    }
}
