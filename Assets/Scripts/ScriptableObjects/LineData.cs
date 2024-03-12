using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Line Data", menuName = "LineData")]
public class LineData : ScriptableObject
{
    public float a;
    public float b;
    public float h;
    public float k;

    public int conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola
    public bool orientation; //true = horizontal , false = vertical
    public string changeType;

    public GameObject lineObject;
    public Transform workArea;
    public string puzzleID;

    [System.NonSerialized]
    public UnityEvent dataChangeEvent = new UnityEvent();
    [System.NonSerialized]
    public UnityEvent attachedDataEvent = new UnityEvent();

    private void OnEnable()
    {

        ResetValues();
    }

    
    public void SetA(float newA, string changeType){a = newA;  this.changeType = changeType; dataChangeEvent.Invoke();}
    public void SetB(float newB, string changeType){b = newB;  this.changeType = changeType; dataChangeEvent.Invoke();}
    public void SetH(float newH, string changeType){h = newH;  this.changeType = changeType; dataChangeEvent.Invoke();}
    public void SetK(float newK, string changeType){k = newK;  this.changeType = changeType; dataChangeEvent.Invoke();}

    public void SetA(float newA){a = newA;  this.changeType = "none"; dataChangeEvent.Invoke();}
    public void SetB(float newB){b = newB;  this.changeType = "none"; dataChangeEvent.Invoke();}
    public void SetH(float newH){h = newH;  this.changeType = "none"; dataChangeEvent.Invoke();}
    public void SetK(float newK){k = newK;  this.changeType = "none"; dataChangeEvent.Invoke();}

    public void SetType(int newType){ conicType = newType; ResetValues();  dataChangeEvent.Invoke();}
    public void SetOrientation(bool newOrientation){ orientation = newOrientation;  dataChangeEvent.Invoke();}


    public void AttachToLineData(float newA, float newB, float newH,float newK,bool  newOrientation, int newConicType, GameObject newPuzzleObject, Transform newWorkArea, string newPuzzleID)
    {
        a = newA; b = newB; h = newH; k = newK; orientation = newOrientation; conicType = newConicType; lineObject = newPuzzleObject; workArea = newWorkArea; puzzleID = newPuzzleID;
        //Debug.Log("LD a value = " + a);
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
        a = 1f; b = 1f; h = 0f; k = 0f;  changeType = "none"; lineObject = null; workArea = null;
    }



}
