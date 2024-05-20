using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedObject2 :  MonoBehaviour
{
    [Header("LineData ScriptableObject")]
    [SerializeField] private LineData2 ldScriptableObject;
    [Tooltip("a b h k")]
    [SerializeField] private char varLink1;
    [Tooltip("a b h k")]
    [SerializeField] private char varLink2;
    [SerializeField] private bool inversed;


    [SerializeField] private PuzzleObject po1; 
    [SerializeField] private PuzzleObject po2; 
     private void OnEnable() {
        //base.OnEnable();

        ldScriptableObject.dataChangeEvent.AddListener(CheckObject);
    }

    private void OnDisable() {

        ldScriptableObject.dataChangeEvent.RemoveListener(CheckObject);
    }

    private void CheckObject()
    {
        if(ldScriptableObject.puzzleObjectScript == po1)
        {
            UpdateObject(varLink1,varLink2, po1,po2);
        } 
        if(ldScriptableObject.puzzleObjectScript == po2)
        {
            UpdateObject(varLink2,varLink1, po2,po1);
        }
    }

    private void UpdateObject(char link1, char link2, PuzzleObject poA, PuzzleObject poB)
    {
        float percentage, range;
        percentage = 1f;
        if(link1 == 'a')
        {
            percentage = (poA.a - poA.minA)/(poA.maxA - poA.minA);
        }
        if(link1 == 'b')
        {
            percentage = (poA.b - poA.minB)/(poA.maxB - poA.minB);
        }
        if(link1 == 'h')
        {
            percentage = (poA.h - poA.minH)/(poA.maxH - poA.minH);
        }
        if(link1 == 'k')
        {
            percentage = (poA.k - poA.minK)/(poA.maxK - poA.minK);
        }


        if(inversed)
        {
            percentage = Mathf.Abs(percentage-1f);
        }

        if(link2 == 'a')
        {
            range = poB.maxA - poB.minA;
            poB.SetA((range*percentage) + poB.minA);
        }
        if(link2 == 'b')
        {
            range = poB.maxB - poB.minB;
            poB.SetB((range*percentage) + poB.minB);
        }
        if(link2 == 'h')
        {
            range = poB.maxH - poB.minH;
            poB.SetH((range*percentage) + poB.minH);
        }
        if(link2 == 'k')
        {
            range = poB.maxK - poB.minK;
            poB.SetK((range*percentage) + poB.minK);
        }
        
    }
}
