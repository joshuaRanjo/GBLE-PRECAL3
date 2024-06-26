using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeyHoles : LevelProp
{   
    [SerializeField] private bool isVertex;
    [SerializeField] private MultipleConditionSwitch mcsScript;
    private bool activated;
    private List<Collider2D> colliderList;
    [SerializeField] private SpriteRenderer light;
    private void OnEnable() {
        colliderList = new List<Collider2D>();
        if(isVertex)
        {
            light.color =  new Color(0.996f, 0.996f, 0.114f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PointKey"))
        {
            //Debug.Log("PointKey entered");
            if( (isVertex && other.transform.name.Contains("vertexPoint")) 
                || (!isVertex && ((other.transform.name.Contains("fociPoint"))))
              )
            {
                colliderList.Add(other);
                if(!activated)
                {
                    //Debug.Log("PointKey entered, isVertex = " + isVertex);
                    activated = true;
                    //if(mcsScript != null)
                        mcsScript?.IncreaseCondition();
                    
                }
            }
        }
        //Debug.Log("TriggerEntered " + other.transform.name);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(colliderList.Contains(other))
        {
            colliderList.RemoveAll(obj => obj == other);
            if(colliderList.Count == 0)
            {
                activated = false;
                if(mcsScript != null)
                    mcsScript.DecreaseCondition();
                //Debug.Log("No more valid keys in keyhole");
            }
            //Debug.Log("PointKey exited");
        }
    }

    private void TurnOnLight()
    {

    }

    private void TurnOffLight()
    {

    }

    
}
