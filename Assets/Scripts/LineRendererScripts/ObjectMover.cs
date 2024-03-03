using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private  GameObject lineObject;
    [SerializeField] private  Transform workArea;

    private void OnEnable() {
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
    }

    private void OnDisable() {
         EventManager.StopListening("ExitPuzzle", ExitPuzzle);
    }

    public void SetLineObject(GameObject newObject, Transform newWorkArea)
    {
        lineObject = newObject; workArea= newWorkArea;
    }

    public void UpdateLine(float h, float k)
    {
        Vector3 currentPosition = lineObject.transform.position;

        currentPosition.x = h;
        currentPosition.y = k;
        lineObject.transform.localPosition = currentPosition;
        //Debug.Log("ObjectMover" + h + " and " +k);
    }

    private void ExitPuzzle()
    {
        lineObject = null;
        workArea = null;
    }
}
