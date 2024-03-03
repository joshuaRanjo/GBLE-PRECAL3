using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using Fungus;


public class PuzzleObject : MonoBehaviour
{
    [Header("Puzzle ID")]
    [SerializeField] private string puzzleID;

    [Header("Question ScriptableObject")]
    [SerializeField] private QuestionData qdScriptableObject;

    [Header("LineData ScriptableObject")]
    [SerializeField] private LineData ldScriptableObject;

    [Header("Fungus Flowchart")]
    [SerializeField] private Flowchart fc;

    [Header("Question Details")]

    [SerializeField] private string prompt;
    
    [SerializeField] private bool allowCircle, allowEllipse, allowParabola, allowHyperbola;
    [SerializeField] private bool puzzleType; // True = interact with object, false = line creation
    [SerializeField] private bool allowA, allowB, allowH, allowK, allowOrientation;
    [Header("For Parabolas")]
    [SerializeField] private bool ceiling;


    [Header("Saved values")]
    [SerializeField] private float a;
    [SerializeField] private float b,h,k;
    [SerializeField] private bool orientation; //true = horizontal , false = vertical
    [SerializeField] private int conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola

    [Header("Default values")]
    [SerializeField] private float default_a;
    [SerializeField] private float default_b,default_h,default_k;
    [SerializeField] private bool default_orientation; //true = horizontal , false = vertical
    [SerializeField] private int default_conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola
        [Header("Max Values")]
    [SerializeField] private float maxA;
    [SerializeField] private float minA,maxB,minB,maxH,minH,maxK,minK;

    [Header("Puzzle Parts")]
    [SerializeField] private Transform workAreaTransform;
    [SerializeField] private GameObject gridObject;
    [SerializeField] private GameObject puzzleObject;

    private void OnEnable() {
        EventManager.StartListening("ExitPuzzle",ExitPuzzle);
    }

    private void OnDisable() {
         EventManager.StopListening("ExitPuzzle",ExitPuzzle);
    }

    public void AttachToScriptableObjects()
    {
        ldScriptableObject.AttachToLineData(a,b,h,k, orientation, conicType, puzzleObject, workAreaTransform, puzzleID);
        /*
        qdScriptableObject.AttachToQuestionData(prompt, allowCircle, allowEllipse, allowParabola, allowHyperbola 
                                                , null, puzzleType
                                                , allowA, allowB, allowH, allowK
                                                , allowOrientation, ceiling
                                                , maxA,minA, maxB,minB, maxH,minH, maxK,minK
                                                , default_a,default_b,default_h,default_k
                                                );
        */
        

        gridObject.SetActive(true);
        EventManager.TriggerEvent("EnterPuzzle");
    }


    public void ExitPuzzle()
    {
        gridObject.SetActive(false);
        GetSaveLineData(); //Get Line Data to save
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

}
