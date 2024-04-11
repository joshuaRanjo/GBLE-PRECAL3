using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineRendererController : MonoBehaviour
{
    [Header("Line Data")]
    [SerializeField] private LineData2 ldScriptableObject;
    [Header("Question Data")]
    [SerializeField] private QuestionData2 qdScriptableObject;

    [Header("Line Scripts")]
    [SerializeField] private CircleRenderer circleRenderer;
    [SerializeField] private EllipseRenderer ellipseRenderer;
    [SerializeField] private ParabolaRendererDef parabolaRenderer;
    [SerializeField] private HyperbolaRenderer hyperbolaRenderer;
    [SerializeField] private InteractedCircleRenderer interactedCircleRenderer;
    [SerializeField] private ObjectMover objectMoverScript;
    private GameObject puzzleObject;

    private void OnEnable() {
        ldScriptableObject.attachedDataEvent.AddListener(AttachData);
        ldScriptableObject.dataChangeEvent.AddListener(UpdateLine);
    }

    private void OnDisable() {
        ldScriptableObject.attachedDataEvent.RemoveListener(AttachData);
        ldScriptableObject.dataChangeEvent.RemoveListener(UpdateLine);
    }

    private void AttachData()
    {
        objectMoverScript.SetLineObject(ldScriptableObject.puzzleObject, ldScriptableObject.workArea);
        if(ldScriptableObject.conicType == 1)
        {
             
            //Circle
            if(qdScriptableObject.puzzleType) //True == Interact type puzzle // False == Line type puzzle;
            {
                Debug.Log("Circle Attached");
                interactedCircleRenderer.SetLineObject(ldScriptableObject.puzzleObject);
            }
            else 
            {

            }
        }
        if(ldScriptableObject.conicType == 2)
        {
            //Ellipse
            if(qdScriptableObject.puzzleType) //True == Interact type puzzle // False == Line type puzzle;
            {
                interactedCircleRenderer.SetLineObject(ldScriptableObject.puzzleObject);
            }
            else 
            {

            }
        }
        if(ldScriptableObject.conicType == 3)
        {
            //Parabola
            if(qdScriptableObject.puzzleType) //True == Interact type puzzle // False == Line type puzzle;
            {
               parabolaRenderer.SetLineObject(ldScriptableObject.puzzleObject);
            }
            else 
            {

            }
        }
        if(ldScriptableObject.conicType == 4)
        {
            //Hyperbola
        }
    }

    private void UpdateLine()
    {
        //Use ObjectMoverScript to change the x and y position of object
        objectMoverScript.UpdateLine(ldScriptableObject.h, ldScriptableObject.k); 


        // Modifies the line itself
        if(ldScriptableObject.conicType == 1)
        {
            //Circle
            if(qdScriptableObject.puzzleType) //True == Interactable Puzzle, False == Line renderer Type puzzle
            {
                interactedCircleRenderer.UpdateLine(ldScriptableObject.a);
            }
            else    
            {

            }
        }
        if(ldScriptableObject.conicType == 2)
        {
            //Ellipse
            if(qdScriptableObject.puzzleType) //True == Interactable Puzzle, False == Line renderer Type puzzle
            {
                interactedCircleRenderer.UpdateLine(ldScriptableObject.a,ldScriptableObject.b);
            }
        }
        if(ldScriptableObject.conicType == 3)
        {
            if(qdScriptableObject.puzzleType) //True == Interactable Type Puzzle, False == Line renderer Type puzzle
            {
                //parabolaRenderer.UpdateLine(ldScriptableObject.a,ldScriptableObject.orientation, qdScriptableObject.ceiling);
                parabolaRenderer.UpdateLine();
            }
        }
        if(ldScriptableObject.conicType == 4)
        {
            //Hyperbola
        }
    }

    public void SetPuzzleObject()
    {
        puzzleObject = ldScriptableObject.puzzleObject;
    }


}
