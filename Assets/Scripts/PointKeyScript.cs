using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeyScript : MonoBehaviour
{
    private ClickableObject selfClickable;
    private ClickableObject parentClickable;

    public void SetUpClickableScript() {
        selfClickable = GetComponent<ClickableObject>();
        parentClickable = transform.parent.gameObject.GetComponent<ClickableObject>();

        if(transform.parent.gameObject.GetComponent<PuzzleObject>().conicType == 4 && transform.parent.gameObject.GetComponent<ParabolaObject>().split)
        {
            parentClickable = transform.parent.Find("Child1").gameObject.GetComponent<ClickableObject>();
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
