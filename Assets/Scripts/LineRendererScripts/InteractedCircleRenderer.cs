using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedCircleRenderer : MonoBehaviour
{
    [SerializeField] private GameObject lineObject;

    public void SetLineObject(GameObject newObject)
    {
        Debug.Log("Line Object Set");
        lineObject = newObject;
    }

    public void UpdateLine(float a)
    {
        Debug.Log("Updating");
       Vector3 scale = lineObject.transform.localScale;
       lineObject.transform.localScale = new Vector3(a*2, a*2, scale.z);
    }

    public void UpdateLine(float a, float b)
    {
        Vector3 scale = lineObject.transform.localScale;
       lineObject.transform.localScale = new Vector3(a*2, b*2, scale.z);
    }
    private void ExitPuzzle()
    {
        lineObject = null;

    }
}
