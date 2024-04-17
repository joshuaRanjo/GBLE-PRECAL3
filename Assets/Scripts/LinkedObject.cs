using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedObject : LevelProp
{
    [Header("LineData ScriptableObject")]
    [SerializeField] private LineData2 ldScriptableObject;
    public float a,b,h,k;
    public bool connectionA ,connectionB ,connectionH , connectionK;
    public bool negativeCA,negativeCB,negativeCH,negativeCK;

    public List<PuzzleObject> puzzleObjectsGroup1 = new List<PuzzleObject>();
    public List<PuzzleObject> puzzleObjectsGroup2 = new List<PuzzleObject>();

    //public List<GameObject> gameObjectsGroup1 = new List<GameObject>();
    //public List<GameObject> gameObjectsGroup2 = new List<GameObject>();

    private void OnEnable() {
        base.OnEnable();

        ldScriptableObject.dataChangeEvent.AddListener(UpdateLine);
    }

    private void OnDisable() {

        ldScriptableObject.dataChangeEvent.RemoveListener(UpdateLine);
    }

    public void UpdateLine()
    {
        a = ldScriptableObject.puzzleObjectScript.a;
        b = ldScriptableObject.puzzleObjectScript.b;
        h = ldScriptableObject.puzzleObjectScript.h;
        k = ldScriptableObject.puzzleObjectScript.k;
        if(puzzleObjectsGroup1.Contains(ldScriptableObject.puzzleObjectScript) || puzzleObjectsGroup2.Contains(ldScriptableObject.puzzleObjectScript))
        {
            UpdateObjects();
        }
    }


    public void UpdateObjects()
    {
        if(puzzleObjectsGroup1.Contains(ldScriptableObject.puzzleObjectScript))
        {
            UpdateObjs(puzzleObjectsGroup1, puzzleObjectsGroup2);
        }
        else if(puzzleObjectsGroup2.Contains(ldScriptableObject.puzzleObjectScript))
        {
            UpdateObjs(puzzleObjectsGroup2, puzzleObjectsGroup1);
        }
    }

    private void UpdateObjs(List<PuzzleObject> group1, List<PuzzleObject> group2)
    {
        foreach(PuzzleObject po in group1)
        {
            if(po.puzzleObject != ldScriptableObject.puzzleObject)
            {
                if(connectionH)
                {
                    po.SetH(h);
                }
                if(connectionK)
                {
                    po.SetK(k);
                }
                if(connectionA)
                {
                    po.SetA(a);
                }
                if(connectionB)
                {
                    po.SetB(b);
                }
            }
        }

        foreach(PuzzleObject po in group2)
        {
            if(po.puzzleObject != ldScriptableObject.puzzleObject)
            {
                if(connectionH)
                {
                    po.SetH(-h);
                }
                if(connectionK)
                {
                    po.SetK(-k);
                }
                if(connectionA)
                {
                    po.SetA(-a);
                }
                if(connectionB)
                {
                    po.SetB(-b);
                }
            }
        }  
    }
}
