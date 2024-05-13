using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractedCircleRenderer : MonoBehaviour
{
    [SerializeField] private GameObject lineObject;

    public void SetLineObject(GameObject newObject)
    {
        lineObject = newObject;
    }

    public void UpdateLine(float a)
    {
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

    public void UpdateLine(GameObject puzzleObject, float a)
    {
       Vector3 scale = puzzleObject.transform.localScale;
       puzzleObject.transform.localScale = new Vector3(a*2, a*2, scale.z);
    }

    public void UpdateLine(GameObject puzzleObject, float newA, float newB)
    {
        Vector3 scale = puzzleObject.transform.localScale;
        if(newA != 0 && newB != 0)
        {
            float a = newA;
            float b = newB;
            if(puzzleObject.GetComponent<PuzzleObject>().simplifiedEllipse)
            {
                a = Mathf.Sqrt(Mathf.Abs(a));
                b = Mathf.Sqrt(Mathf.Abs(b));
            }
            puzzleObject.transform.localScale = new Vector3(a*2, b*2, scale.z);
        }

    }
}
