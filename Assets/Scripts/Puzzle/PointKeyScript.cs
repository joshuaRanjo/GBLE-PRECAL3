using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeyScript : MonoBehaviour
{
    private ClickableObject selfClickable;
    private ClickableObject parentClickable;

    public ParabolaObject parabolaObject;
    public PuzzleObject puzzleObject;

    public void SetUpClickableScript() {
        selfClickable = GetComponent<ClickableObject>();
        parentClickable = puzzleObject.gameObject.GetComponent<ClickableObject>();



        if(parabolaObject != null)
        if(puzzleObject.conicType == 4 && parabolaObject.split)
        {
            parentClickable = puzzleObject.gameObject.transform.Find("Child1").gameObject.GetComponent<ClickableObject>();
        }
        

        if(parentClickable != null)
        {
            selfClickable.spriteRenderers = parentClickable.spriteRenderers;
            selfClickable.spriteShapeRenderer = parentClickable.spriteShapeRenderer;
            selfClickable.spriteShapeRenderers = parentClickable.spriteShapeRenderers;

            selfClickable.interaction = parentClickable.interaction;
            selfClickable.startColor = parentClickable.startColor;
            selfClickable.endColor = parentClickable.endColor;
        }
    }


}
