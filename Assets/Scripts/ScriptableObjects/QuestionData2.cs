using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Question Data 2", menuName = "QuestionData2")]
public class QuestionData2 : ScriptableObject
{
    [Header("Puzzle Details")]

    public bool allowCircle, allowEllipse, allowParabola, allowHyperbola;
    public bool allowA, allowB, allowH, allowK, allowOrientation;
    public bool ceiling;
    public float maxA,minA,maxB,minB,maxH,minH,maxK,minK;
    public float defaultA, defaultB, defaultH, defaultK;
    public float xOffset,yOffset;
    public bool simplifiedEllipse;

    public bool puzzleType; // True = interact with object, false = line creation

    [System.NonSerialized]
    public UnityEvent questionUpdateEvent = new UnityEvent();
    public void AttachToQuestionData( bool newAllowCircle
                                    , bool newAllowEllipse, bool newAllowParabola
                                    , bool newAllowHyperbola 
                                    , bool newPuzzleType, bool newAllowA, bool newAllowB, bool newAllowH
                                    , bool newAllowK, bool newAllowOrientation, bool newCeiling
                                    , float newMaxA, float newMinA, float newMaxB, float newMinB, float newMaxH, float newMinH, float newMaxK, float newMinK
                                    , float newDefaultA, float newDefaultB, float newDefaultH, float newDefaultK
                                    , float newXOffset, float newYOffset
                                    , bool newSimplifiedEllipse
                                    )
    {        
        allowCircle = newAllowCircle;
        allowEllipse = newAllowEllipse;
        allowParabola = newAllowParabola;
        allowHyperbola = newAllowHyperbola;

        puzzleType = newPuzzleType;

        allowA = newAllowA;
        allowB = newAllowB;
        allowH = newAllowH;
        allowK = newAllowK;

        allowOrientation = newAllowOrientation;
        ceiling = newCeiling;

        maxA = newMaxA;
        minA = newMinA;
        maxB = newMaxB;
        minB = newMinB;
        maxH = newMaxH;
        minH = newMinH;
        maxK = newMaxK;
        minK = newMinK;

        defaultA = newDefaultA;
        defaultB = newDefaultB;
        defaultH = newDefaultH;
        defaultK = newDefaultK;

        xOffset = newXOffset;
        yOffset = newYOffset;
        simplifiedEllipse = newSimplifiedEllipse;

        if(simplifiedEllipse)
        {
            SquareLimits();
        }
       
        //Change UI
        questionUpdateEvent.Invoke();
    }

    public void ClearQuestionData()
    {
;

        allowCircle = false;
        allowEllipse = false;
        allowParabola = false;
        allowHyperbola = false;

        allowA = false;
        allowB = false;
        allowH = false;
        allowK = false;

        allowOrientation = false;
    }

    private void SquareLimits()
    {
        maxA *= maxA;
        minA = 0.01f;
        maxB *= maxB;
        minB = 0.01f;
    }

    private void SquareRootLimits()
    {
        maxA = Mathf.Sqrt(maxA);
        minA = -maxA;
        maxB = Mathf.Sqrt(maxB);
        minB = -maxB;
    }

    public void ModifyMaxMinSimplifiedEllipse()
    {   
        //convert to unsimplified
        if(simplifiedEllipse)
        {
            simplifiedEllipse = false;
            SquareRootLimits();

        }
        else // convert to simplified
        {
            simplifiedEllipse = true;
            SquareLimits();
        }
    }




}