using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : LevelProp
{

    public bool openStatus = false;
    public bool defaultStatus = false;

    public float duration = 1f;

    private Vector3 closedPosition = Vector3.zero;
    private Vector3 openPosition = new Vector3(0,2,0);

    private Coroutine moveDoor;

    [SerializeField] private GameObject doorObject;

    private void Start() {

        if(defaultStatus) //If the default status of the door is Open, open the door
        {
            Open();
        }
    }

    public void Open()
    {
        Debug.Log("Opening");
        openStatus = true;

        if(moveDoor != null)
        {
            StopCoroutine(moveDoor);
        }

        moveDoor = StartCoroutine(MoveObject(openPosition));
    }

    public void Close()
    {
        Debug.Log("Closing");
        openStatus = false;

        if(moveDoor != null)
        {
            StopCoroutine(moveDoor);
        }

        moveDoor = StartCoroutine(MoveObject(closedPosition));
    }

    IEnumerator MoveObject(Vector3 targetPosition)
    {

        Transform objectToMove = doorObject.transform;
        float timeElapsed = 0f;
        Vector3 startingPosition = objectToMove.localPosition;

        while (timeElapsed < duration)
        {
            // Calculate the normalized progress of the movement
            float normalizedTime = timeElapsed / duration;

            // Interpolate the position between starting and target positions
            objectToMove.localPosition = Vector3.Lerp(startingPosition, targetPosition, normalizedTime);

            // Increment time elapsed
            timeElapsed += Time.deltaTime;

            yield return null; // Wait for the end of frame
        }

        // Ensure the object reaches the exact target position at the end
        objectToMove.localPosition = targetPosition;
    }
    /*
    private void OnEnable() {
        if(transform.parent.gameObject.name != "LevelProps")
            transform.parent = GameObject.Find("LevelProps").transform;
    }*/
}
