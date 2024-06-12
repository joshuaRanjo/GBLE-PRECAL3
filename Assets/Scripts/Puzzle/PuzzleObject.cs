using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class PuzzleObject : LevelProp
{
    public delegate void UpdateObjectDelegate();
    public event UpdateObjectDelegate updateObjectCalled;

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

    [SerializeField] private bool inverseAB = false;


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
    [SerializeField] public float maxA;
    [SerializeField] public float minA,maxB,minB,maxH,minH,maxK,minK;

    [Header("For Ellipse")]
    [SerializeField] public bool simplifiedEllipse = false;

    [Header("Puzzle Parts")]
    [SerializeField] public GameObject puzzleObject;

    [Header("For Parabolas")]
    [SerializeField] public bool ceiling;

    private Vector3 targetPosition;
    private float speed = 1f;

    
    //[Header("Camera Offset")]
     private float xOffset = 0;
     private float yOffset = 0;

    private bool inPuzzle = false;
    private bool precisionMode = false;

    [SerializeField] private LineRendererController2 lrController;

    private void LateUpdate() {
        h = Mathf.Round(puzzleObject.transform.localPosition.x *100) / 100;
        k = Mathf.Round(puzzleObject.transform.localPosition.y *100) / 100;
    }

    private void OnEnable() {

        puzzleObject = this.gameObject;
        targetPosition = this.transform.position;

        EventManager.StartListening("ExitPuzzle",ExitPuzzle);

        EventManager.StartListening("SwitchSimplifiedEllipse",SwitchSimplifiedEllipse);

        lrController = GameObject.Find("Game").GetComponent<LineRendererController2>();

        h = Mathf.Round(puzzleObject.transform.localPosition.x *100) / 100;
        k = Mathf.Round(puzzleObject.transform.localPosition.y *100) / 100;
        default_h = h;
        default_k = k;

        if(conicType == 1 && puzzleType)
        {
            a = puzzleObject.transform.localScale.x / 2;
        }
        if(conicType == 2 && puzzleType)
        {
            a = puzzleObject.transform.localScale.x / 2;
            b = puzzleObject.transform.localScale.y / 2;
            
        }
        if(simplifiedEllipse && (conicType == 2 || conicType == 4)) 
        {
            a *= a;
            b *= b;

            maxA *= maxA;
            minA = 0.25f;
            maxB *= maxB;
            minB = 0.25f;
        }

        puzzleObject = this.gameObject;
    }

    private void OnDisable() {
         EventManager.StopListening("ExitPuzzle",ExitPuzzle);
         EventManager.StopListening("SwitchSimplifiedEllipse",SwitchSimplifiedEllipse);
    }

    public void AttachToScriptableObjects()
    {
        if(!inPuzzle && CheckVicinity())
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
                                                    , simplifiedEllipse
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
        updateObjectCalled?.Invoke();
    }

    public void SetA(float newA)
    {
        float oldA = a;
        a = newA;
        if(inverseAB)
        {
            float rangeA = maxA - minA;
            float rangeB = maxB - minB;

            b = (rangeB * Mathf.Abs(((a-minA)/rangeA)-1)) + minB;
            b = Mathf.Round(b * 100f) / 100f;
            if(ldScriptableObject.puzzleObjectScript == this)
                ldScriptableObject.SetB(b);
        }
        
        UpdateObject();
        
    }
    public void SetAPrecision(float newA){ precisionMode = true; SetA(newA); }
    public void SetBPrecision(float newB){ precisionMode = true; SetB(newB); }

    public void SetB(float newB)
    {
        float oldB = b;
        b = newB;
        if(inverseAB)
        {

            float rangeA = maxA - minA;
            float rangeB = maxB - minB;

            a = (rangeA * Mathf.Abs(((b-minB)/rangeB)-1)) + minA;
            a = Mathf.Round(a * 1000f) / 1000f;
            if(ldScriptableObject.puzzleObjectScript == this)
                ldScriptableObject.SetA(a);
        }
        if(!precisionMode)
            UpdateObject();
        else
        {
            precisionMode = false;
            b = oldB;
        }
       
    }
    public void SetH(float newH)
    {
        h = newH;
        
        UpdateObject();
        if(!CheckVicinity())
            EventManager.TriggerEvent("ExitPuzzle");
        
    }

    public void SetK(float newK)
    {
        k = newK;

        UpdateObject();

        if(!CheckVicinity())
            EventManager.TriggerEvent("ExitPuzzle");
        
    }

    public void SetAll(float newA, float newB, float newH, float newK)
    {
        a = newA;
        b = newB;
        h = newH;
        k = newK;
        UpdateObject();
    }

    public void SwitchSimplifiedEllipse()
    {
        if(ldScriptableObject.puzzleObjectScript == this)
        {
            simplifiedEllipse = !simplifiedEllipse;

            if(simplifiedEllipse)
            {
                a *= a;
                b *= b;
                
                maxA *= maxA;
                minA = 1f;
                maxB *= maxB;
                minB = 1f;
            }
            else
            {
                a = Mathf.Sqrt(a);
                b = Mathf.Sqrt(b);

                maxA = Mathf.Sqrt(maxA);
                minA = -maxA;
                maxB = Mathf.Sqrt(maxB);
                minB = -maxB;
            }
            ldScriptableObject.SetA(a);
            ldScriptableObject.SetB(b);
            ldScriptableObject.SetSimplifiedEllipse(simplifiedEllipse);
        }
        
    }
    public void ResetObject()
    {
        a = default_a;
        b = default_b;
        h = default_h;
        k = default_k;

        UpdateObject();
    }

    private bool CheckVicinity()
    {
        
        Collider2D colliders = Physics2D.OverlapPoint(new Vector2(h,k), LayerMask.GetMask("InterferenceLayer"));
    
        
        if(colliders != null)
        {
            return  false;
        }
        else
        {
            return  true;
        }
    }
}
