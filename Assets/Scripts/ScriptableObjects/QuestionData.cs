using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Question Data", menuName = "QuestionData")]
public class QuestionData : ScriptableObject
{
    [Header("Puzzle Details")]
    public string prompt;
    public bool allowCircle, allowEllipse, allowParabola, allowHyperbola;
    public bool allowA, allowB, allowH, allowK, allowOrientation;
    public bool ceiling;
    public float maxA,minA,maxB,minB,maxH,minH,maxK,minK;
    public float defaultA, defaultB, defaultH, defaultK;

    public bool puzzleType; // True = interact with object, false = line creation

    [Header("ExpectedAnswer")]
    public List<ExpectedAnswer> expectedAnswerList;
    [Header("Puzzle Script")]
    public PuzzleScript puzzleScript;

    [System.NonSerialized]
    public UnityEvent questionUpdateEvent = new UnityEvent();
    public void AttachToQuestionData(string newPrompt, bool newAllowCircle
                                    , bool newAllowEllipse, bool newAllowParabola
                                    , bool newAllowHyperbola, List<ExpectedAnswer> newExpectedAnswerList
                                    , bool newPuzzleType, bool newAllowA, bool newAllowB, bool newAllowH
                                    , bool newAllowK, bool newAllowOrientation, bool newCeiling
                                    , float newMaxA, float newMinA, float newMaxB, float newMinB, float newMaxH, float newMinH, float newMaxK, float newMinK
                                    , float newDefaultA, float newDefaultB, float newDefaultH, float newDefaultK
                                    )
    {
        prompt = newPrompt;
        
        allowCircle = newAllowCircle;
        allowEllipse = newAllowEllipse;
        allowParabola = newAllowParabola;
        allowHyperbola = newAllowHyperbola;

        expectedAnswerList = newExpectedAnswerList;
        puzzleType = newPuzzleType;

        allowA = newAllowA;
        allowB = newAllowB;
        allowH = newAllowH;
        allowK = newAllowK;

        allowOrientation = newAllowOrientation;
        newCeiling = newCeiling;

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
       
        //Change UI
        questionUpdateEvent.Invoke();
    }

    public void ClearQuestionData()
    {

        prompt = null;

        allowCircle = false;
        allowEllipse = false;
        allowParabola = false;
        allowHyperbola = false;
        expectedAnswerList = null;

        allowA = false;
        allowB = false;
        allowH = false;
        allowK = false;

        allowOrientation = false;
    }




}