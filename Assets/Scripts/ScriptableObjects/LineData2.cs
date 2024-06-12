using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Line Data 2", menuName = "LineData2")]
public class LineData2 : ScriptableObject
{
    public float a;
    public float b;
    public float h;
    public float k;

    public int conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola
    public bool orientation; //true = horizontal , false = vertical
    public string changeType;

    public bool simplifiedEllipse;

    public GameObject puzzleObject;
    public Transform workArea;
    public string puzzleID;

    [System.NonSerialized]
    public UnityEvent dataChangeEvent = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent dataChangeEventA = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent dataChangeEventB = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent dataChangeEventH = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent dataChangeEventK = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent attachedDataEvent = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent simplifiedEquationChange = new UnityEvent();

    public PuzzleObject puzzleObjectScript;

    private void OnEnable()
    {

        ResetValues();
    }

    
    public void SetA(float newA, string changeType)
    {   
          this.changeType = changeType;  
        if(a != newA)
        {
            
            a = newA;
            puzzleObjectScript.SetA(newA); dataChangeEvent.Invoke(); EventManager.TriggerEvent("ChangeEvent");
        }
    }
    public void SetB(float newB, string changeType)
    {
        this.changeType = changeType; 
        if(b != newB)
        {
            b = newB;
            puzzleObjectScript.SetB(newB); dataChangeEvent.Invoke(); 
            EventManager.TriggerEvent("ChangeEvent");
        }


        
    }
    public void SetH(float newH, string changeType)
    {
        this.changeType = changeType; 
        if(h != newH)
        {
          h = newH;  puzzleObjectScript.SetH(newH); dataChangeEvent.Invoke(); EventManager.TriggerEvent("ChangeEvent");
        }
        
    }
    public void SetK(float newK, string changeType)
    {
          this.changeType = changeType;
        if(k != newK)
        {
           k = newK;  puzzleObjectScript.SetK(newK); dataChangeEvent.Invoke(); EventManager.TriggerEvent("ChangeEvent");
        }

    }

    public void SetAll(float newA, float newB, float newH, float newK)
    {
        a = newA; b = newB; h = newH; k = newK;
        puzzleObjectScript.SetAll(a,b,h,k);


        dataChangeEvent.Invoke(); 

        EventManager.TriggerEvent("ChangeEvent");
    }
    public void SetA(float newA){a = newA;  this.changeType = "none";  dataChangeEventA.Invoke();}
    public void SetB(float newB){b = newB;  this.changeType = "none"; dataChangeEventB.Invoke();}
    public void SetH(float newH){h = newH;  this.changeType = "none"; dataChangeEventH.Invoke();}
    public void SetK(float newK){k = newK;  this.changeType = "none"; dataChangeEventK.Invoke();}

    public void SetSimplifiedEllipse(bool newBool) {simplifiedEllipse = newBool; simplifiedEquationChange.Invoke();}

    public void SetType(int newType){ conicType = newType; ResetValues();  dataChangeEvent.Invoke();}
    public void SetOrientation(bool newOrientation){ orientation = newOrientation;  dataChangeEvent.Invoke();}


    public void AttachToLineData(float newA, float newB, float newH,float newK,bool  newOrientation, int newConicType, GameObject newPuzzleObject,  string newPuzzleID
                                , PuzzleObject poScript)
    {
        /*
        a = newA; 
        b = newB; 
        h = newH; 
        k = newK; 
        orientation = orientation; 
        conicType = newConicType; 
        puzzleObject = newPuzzleObject; 
        puzzleID = newPuzzleID;
        */
        a = poScript.a;
        b = poScript.b;
        h = poScript.h;
        k = poScript.k;
        orientation = poScript.orientation;
        conicType = poScript.conicType;
        puzzleObject = poScript.puzzleObject;
        puzzleID = poScript.puzzleID;
        puzzleObjectScript = poScript;

        simplifiedEllipse = poScript.simplifiedEllipse;

        attachedDataEvent.Invoke();
    }
    public void ResetValues()
    {
        a = 1f; b = 1f; h = 0f; k = 0f;  changeType = "none";
        if(conicType == 3)
        {
            orientation = false;
        }
        else
        {
            orientation = true;
        }
    }
    public void ClearLineData()
    {
        a = 1f; b = 1f; h = 0f; k = 0f;  changeType = "none"; puzzleObject = null; workArea = null;
    }



}
