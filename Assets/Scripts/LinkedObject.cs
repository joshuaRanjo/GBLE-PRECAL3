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
        //base.OnEnable();

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
        PuzzleObject poScript = ldScriptableObject.puzzleObjectScript;
        foreach(PuzzleObject po in group1)
        {
            if(po.puzzleObject != ldScriptableObject.puzzleObject)
            {
                float range1,range2;
                if(connectionH)
                {
                    range1 = poScript.maxH - poScript.minH;
                    range2 = po.maxH - poScript.minH;
                    
                    float h2 =  (range2 * ((h - poScript.minH)/(range1))) + po.minH;
                    po.SetH(h2);
                }
                if(connectionK)
                {
                    range1 = poScript.maxK - poScript.minK;
                    range2 = po.maxK - poScript.minK;
                    
                    float k2 =  (range2 * ((k - poScript.minK)/(range1))) + po.minK;
                    po.SetK(k2);
                }
                if(connectionA)
                {
                    range1 = poScript.maxA - poScript.minA;
                    range2 = po.maxA - poScript.minA;
                    
                    float a2 =  (range2 * ((a - poScript.minA)/(range1))) + po.minA;
                    po.SetA(a2);
                }
                if(connectionB)
                {
                    range1 = poScript.maxB - poScript.minB;
                    range2 = po.maxB - poScript.minB;
                    
                    float b2 =  (range2 * ((b - poScript.minB)/(range1))) + po.minB;
                    po.SetB(b2);
                }
            }
        }

        foreach(PuzzleObject po in group2)
        {
            if(po.puzzleObject != ldScriptableObject.puzzleObject)
            {
                float range1,range2;
                if(connectionH)
                {
                    range1 = poScript.maxH - poScript.minH;
                    range2 = po.maxH - po.minH;
                    float h2 = (range2 * Mathf.Abs(((h-poScript.minH)/range1)-1)) + po.minH;
                    po.SetH(h2);
                }
                if(connectionK)
                {
                    range1 = poScript.maxK - poScript.minK;
                    range2 = po.maxK - po.minK;
                    float k2 = (range2 * Mathf.Abs(((k-poScript.minK)/range1)-1)) + po.minK;
                    po.SetK(k2);
                }
                if(connectionA)
                {
                    range1 = poScript.maxA - poScript.minA;
                    range2 = po.maxA - po.minA;
                    float a2 = (range2 * Mathf.Abs(((a-poScript.minA)/range1)-1)) + po.minA;
                    po.SetA(a2);
                }
                if(connectionB)
                {
                    range1 = poScript.maxB - poScript.minB;
                    range2 = po.maxB - po.minB;
                    float b2 = (range2 * Mathf.Abs(((b-poScript.minB)/range1)-1)) + po.minB;
                    po.SetB(b2);
                }
            }
        }  
    }
}
