using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineRendererController2 : MonoBehaviour
{


    [Header("Line Scripts")]
    [SerializeField] private CircleRenderer2 circleRenderer;
    [SerializeField] private EllipseRenderer2 ellipseRenderer;
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
            
            if(puzzleObject.puzzleType) //True == Sprite renderer puzzle, False == Line renderer Type puzzle
            {
                interactedCircleRenderer.UpdateLine(puzzleObject.puzzleObject, puzzleObject.a);
            }
            else
            {
                circleRenderer.UpdateLine(puzzleObject.puzzleObject);
            }
        }
        if(puzzleObject.conicType == 2)
        {
            //Ellipse
            if(puzzleObject.puzzleType) //True == Sprite renderer puzzle, False == Line renderer Type puzzle
            {
                interactedCircleRenderer.UpdateLine(puzzleObject.puzzleObject, puzzleObject.a, puzzleObject.b);
            }
            else
            {
                ellipseRenderer.UpdateLine(puzzleObject.puzzleObject);
            }
        }
        if(puzzleObject.conicType == 3)
        {
            if(puzzleObject.puzzleType) //True == SpriteShapeRenderer, False == Line renderer Type puzzle
            {
                //parabolaRenderer.UpdateLine(ldScriptableObject.a,ldScriptableObject.orientation, qdScriptableObject.ceiling);
                parabolaRenderer.UpdateLineSpriteShape(puzzleObject.puzzleObject); 
            }
            else{
                
            }
        }
        if(puzzleObject.conicType == 4)
        {
            //Hyperbola
            if(puzzleObject.puzzleType)
            {
                //Debug.Log("Updating Sprite Shape");
                hyperbolaRenderer.UpdateLine(puzzleObject.puzzleObject);
            }
        }
    }


}
