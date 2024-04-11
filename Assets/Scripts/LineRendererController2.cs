using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineRendererController2 : MonoBehaviour
{


    [Header("Line Scripts")]
    [SerializeField] private CircleRenderer circleRenderer;
    [SerializeField] private EllipseRenderer ellipseRenderer;
    [SerializeField] private ParabolaRendererDef parabolaRenderer;
    [SerializeField] private HyperbolaRenderer hyperbolaRenderer;
    [SerializeField] private InteractedCircleRenderer interactedCircleRenderer;
    [SerializeField] private ObjectMover objectMoverScript;

    public void UpdateObject(PuzzleObject puzzleObject)
    {   
        //Debug.Log("UpdateObject LR2");
        objectMoverScript.UpdateLine(puzzleObject.puzzleObject, puzzleObject.h, puzzleObject.k);
        if(puzzleObject.conicType == 1)
        {
            interactedCircleRenderer.UpdateLine(puzzleObject.puzzleObject, puzzleObject.a);
        }
    }


}
