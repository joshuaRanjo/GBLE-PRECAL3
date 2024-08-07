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
        /*
        Vector3 currentPosition = lineObject.transform.position;

        currentPosition.x = h;
        currentPosition.y = k;
        currentPosition.z = 0.1f;

        lineObject.transform.localPosition = currentPosition;
        */
        //Debug.Log("ObjectMover" + h + " and " +k);
    }

    public void UpdateLine(GameObject newObject, float h, float k)
    {
        //if(newObject.GetComponent<PuzzleObject>().conicType == 4)
        
        {
            /*
            Vector3 currentPosition = newObject.transform.position;

            currentPosition.x = h;
            currentPosition.y = k;
            currentPosition.z = 0.1f;
            
            newObject.transform.localPosition = currentPosition;
            */
            LeanTween.cancel(newObject);
            LeanTween.move(newObject, new Vector3(h, k , 0), 0.01f).setEase(LeanTweenType.easeInOutQuad);
        }
        
        
    }

    private void ExitPuzzle()
    {
        lineObject = null;
        workArea = null;
    }
}
