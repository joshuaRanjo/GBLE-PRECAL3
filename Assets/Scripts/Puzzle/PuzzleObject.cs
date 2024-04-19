using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class PuzzleObject : LevelProp
{
    [Header("Puzzle ID")]
    public string puzzleID;

    [Header("Question ScriptableObject")]
    [SerializeField] private QuestionData2 qdScriptableObject;

    [Header("LineData ScriptableObject")]
    [SerializeField] private LineData2 ldScriptableObject;
    
    [SerializeField] private bool allowCircle, allowEllipse, allowParabola, allowHyperbola;
    [Tooltip("// True = interact with object, false = line creation")]
    [SerializeField] public bool puzzleType; // True = interact with object, false = line creation
    [SerializeField] private bool allowA, allowB, allowH, allowK, allowOrientation;




    [Header("Saved values")]
    [SerializeField] public float a;
    [SerializeField] public float b,h,k;

    [Tooltip("True = Horizontal, False = Vertical")]
    [SerializeField] public bool orientation; //true = horizontal , false = vertical
    [Tooltip("1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola")]
    [SerializeField] public int conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola

    [Header("Default values")]
    [SerializeField] private float default_a;
    [SerializeField] private float default_b,default_h,default_k;
    [Tooltip("True = Horizontal, False = Vertical")]
    [SerializeField] public bool default_orientation; //true = horizontal , false = vertical
    [Tooltip("1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola")]
    [SerializeField] private int default_conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola

    [Header("Max Values")]
    [SerializeField] private float maxA;
    [SerializeField] private float minA,maxB,minB,maxH,minH,maxK,minK;



    [Header("Puzzle Parts")]
    [SerializeField] public GameObject puzzleObject;

    [Header("For Parabolas")]
    [SerializeField] private bool ceiling;

    
    //[Header("Camera Offset")]
     private float xOffset = 0;
     private float yOffset = 0;

    private bool inPuzzle = false;

    [SerializeField] private LineRendererController2 lrController;

    private void LateUpdate() {
        h = Mathf.Round(puzzleObject.transform.localPosition.x *100) / 100;
        k = Mathf.Round(puzzleObject.transform.localPosition.y *100) / 100;
    }

    private void OnEnable() {

        //Transform levelprops = GameObject.Find("LevelProps").transform;
        //    transform.SetParent(levelprops);
        //base.OnEnable();

        puzzleObject = this.gameObject;

        EventManager.StartListening("ExitPuzzle",ExitPuzzle);
        lrController = GameObject.Find("Game").GetComponent<LineRendererController2>();

        h = Mathf.Round(puzzleObject.transform.localPosition.x *100) / 100;
        k = Mathf.Round(puzzleObject.transform.localPosition.y *100) / 100;
    }

    private void OnDisable() {
         EventManager.StopListening("ExitPuzzle",ExitPuzzle);
    }

    public void AttachToScriptableObjects()
    {
        if(!inPuzzle)
        {
            h = Mathf.Round(puzzleObject.transform.localPosition.x *100) / 100;
            k = Mathf.Round(puzzleObject.transform.localPosition.y *100) / 100;

            ldScriptableObject.AttachToLineData(a,b,h,k, orientation, conicType, puzzleObject, puzzleID,this);
            qdScriptableObject.AttachToQuestionData(allowCircle, allowEllipse, allowParabola, allowHyperbola
                                                    , puzzleType
                                                    , allowA, allowB, allowH, allowK
                                                    , allowOrientation, ceiling
                                                    , maxA,minA, maxB,minB, maxH,minH, maxK,minK
                                                    ,default_a, default_b,default_h,default_k
                                                    , xOffset, yOffset
                                                    );
            


            inPuzzle = true;
            EventManager.TriggerEvent("EnterPuzzle");
        }

    }


    public void ExitPuzzle()
    {
        if(puzzleID == ldScriptableObject.puzzleID && inPuzzle == true)
        {   
            inPuzzle = false;
           // GetSaveLineData();
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

    private void UpdateObject()
    {
        lrController.UpdateObject(this);
    }

    public void SetA(float newA)
    {
        
        a = newA;
        UpdateObject();
    }

    public void SetB(float newB)
    {
        b = newB;
        UpdateObject();
    }
    public void SetH(float newH)
    {
        h = newH;
        UpdateObject();
    }

    public void SetK(float newK)
    {
        k = newK;
        UpdateObject();
    }

    public void SetAll(float newA, float newB, float newH, float newK)
    {
        a = newA;
        b = newB;
        h = newH;
        k = newK;
        UpdateObject();
    }
    public void ResetObject()
    {
        a = default_a;
        b = default_b;
        h = default_h;
        k = default_k;

        UpdateObject();
    }

}
