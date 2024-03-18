using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class PuzzleScript : MonoBehaviour
{
    [Header("Puzzle ID")]
    public string puzzleID;

    [Header("Question ScriptableObject")]
    [SerializeField] private QuestionData qdScriptableObject;

    [Header("LineData ScriptableObject")]
    [SerializeField] private LineData ldScriptableObject;

    [Header("Fungus Flowchart")]
    [SerializeField] private Flowchart fc;

    [Header("Question Details")]

    [SerializeField] private string prompt;
    
    [SerializeField] private bool allowCircle, allowEllipse, allowParabola, allowHyperbola;
    [Tooltip("// True = interact with object, false = line creation")]
    [SerializeField] private bool puzzleType; // True = interact with object, false = line creation
    [SerializeField] private bool allowA, allowB, allowH, allowK, allowOrientation;
    [Header("For Parabolas")]
    [SerializeField] private bool ceiling;


    [Header("Saved values")]
    [SerializeField] private float a;
    [SerializeField] private float b,h,k;

    [Tooltip("True = Horizontal, False = Vertical")]
    [SerializeField] private bool orientation; //true = horizontal , false = vertical
    [Tooltip("1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola")]
    [SerializeField] private int conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola

    [Header("Default values")]
    [SerializeField] private float default_a;
    [SerializeField] private float default_b,default_h,default_k;
    [Tooltip("True = Horizontal, False = Vertical")]
    [SerializeField] private bool default_orientation; //true = horizontal , false = vertical
    [Tooltip("1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola")]
    [SerializeField] private int default_conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola

    [Header("Max Values")]
    [SerializeField] private float maxA;
    [SerializeField] private float minA,maxB,minB,maxH,minH,maxK,minK;


    [Header("Expected Answer")]
    [SerializeField] private List<ExpectedAnswer> expectedAnswers = new List<ExpectedAnswer>();

    [Header("Puzzle Parts")]
    [SerializeField] private Transform workAreaTransform;
    [SerializeField] private GameObject gridObject;
    [SerializeField] private GameObject puzzleObject;
    [SerializeField] private Transform teleport;
    [Header("Camera Offset")]
    [SerializeField] private float xOffset = 0;
    [SerializeField] private float yOffset = 0;

    private bool inPuzzle = false;


    private void OnEnable() {
        EventManager.StartListening("ExitPuzzle",ExitPuzzle);
    }

    private void OnDisable() {
         EventManager.StopListening("ExitPuzzle",ExitPuzzle);
    }

    public void AttachToScriptableObjects()
    {


        ldScriptableObject.AttachToLineData(a,b,h,k, orientation, conicType, puzzleObject, workAreaTransform, puzzleID);
        qdScriptableObject.AttachToQuestionData(prompt, 
                                                allowCircle, allowEllipse, allowParabola, allowHyperbola
                                                , expectedAnswers, puzzleType
                                                , allowA, allowB, allowH, allowK
                                                , allowOrientation, ceiling
                                                , maxA,minA, maxB,minB, maxH,minH, maxK,minK
                                                ,default_a, default_b,default_h,default_k
                                                , xOffset, yOffset
                                                );
        
        if(teleport != null)
        {
            TeleportToPoint();
        }

        gridObject.SetActive(true);
        inPuzzle = true;
        EventManager.TriggerEvent("EnterPuzzle");
    }


    public void ExitPuzzle()
    {
        if(puzzleID == ldScriptableObject.puzzleID && inPuzzle == true)
        {   
            inPuzzle = false;
            gridObject.SetActive(false);
            GetSaveLineData();
        }
        
        //Invoke or clear data on both LineData and QuestionData Scriptable objects;
    }

    public void GetSaveLineData()
    {
        a = ldScriptableObject.a;
        b = ldScriptableObject.b;
        h = ldScriptableObject.h;
        k = ldScriptableObject.k;
        conicType = ldScriptableObject.conicType;
        orientation = ldScriptableObject.orientation; 
    }

    public void CheckAnswer()
    {
        int eaNumber = -1;
        int x = 0;
        foreach(ExpectedAnswer ea in expectedAnswers)
        {
            bool solved = true;
            Debug.Log("Checking Expected answer " + x);
            // Check A
                if((ea.minA != -99f && ea.maxA != -99f)  && ((a < ea.minA) ||  (a > ea.maxA)))
                {
                    Debug.Log("Checking Expected answer " + x + "A");
                    solved = false;
                }
            // Check B
                if((ea.minB != -99f && ea.maxB != -99f)  && ((b < ea.minB) ||  (b > ea.maxB)))
                {
                    Debug.Log("Checking Expected answer " + x + "B");
                    solved = false;
                }
            // Check H
                if((ea.minH != -99f && ea.maxH != -99f)  && ((h < ea.minH) ||  (h > ea.maxH)))
                {
                    solved = false;
                }
            // Check K
                if((ea.minK != -99f && ea.maxK != -99f)  && ((k < ea.minK) ||  (k > ea.maxK)))
                {
                    solved = false;
                }
            // Check conic type
                if(conicType != ea.conicType)
                {
                    solved = false;
                }

            // Check Orientation
                if(conicType > 2) // If conic type is a hyperbola or parabola
                {
                    if(orientation != ea.orientation)
                    {
                        solved = false;
                    }
                }

            if(solved)
            {eaNumber = x;break;}
            x += 1;
        }

        Debug.Log("Checked answer " + eaNumber);

        fc.SetIntegerVariable("CompletionState", eaNumber);
    }

    public void TeleportToPoint()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = teleport.position;
    }

}
