using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : LevelProp
{
    public GameObject triggerObject;
    private bool openStatus = false;
    public bool defaultStatus = false;

    public float speed = 1f;

    private Vector3 closedPosition = Vector3.zero;
    public Vector3 openPosition = new Vector3(0,2,0);

    private Coroutine moveDoor;

    [SerializeField] private GameObject doorObject;

    private void Start() {
        base.Start();
        if(defaultStatus) //If the default status of the door is Open, open the door
        {
            Open();
        }
    }

    public void Open()
    {
        if(!openStatus)
        {
            openStatus = true;

            if(moveDoor != null)
            {
                StopCoroutine(moveDoor);
            }

            moveDoor = StartCoroutine(MoveObject(openPosition));
        }
        
    }

    public void Close()
    {
        
        openStatus = false;

        if(moveDoor != null)
        {
            StopCoroutine(moveDoor);
        }

        moveDoor = StartCoroutine(MoveObject(closedPosition));
    }

    public void Switch()
    {
        if(openStatus)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    IEnumerator MoveObject(Vector3 targetPosition)
    {
        Transform objectToMove = doorObject.transform;
        Vector3 startingPosition = objectToMove.localPosition;
        float distanceToTarget = Vector3.Distance(startingPosition, targetPosition);

        while (Vector3.Distance(objectToMove.localPosition, targetPosition) > 0.01f)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetPosition - objectToMove.localPosition).normalized;

            // Calculate the distance to move this frame based on speed
            float distanceToMove = speed * Time.deltaTime;

            // Ensure distanceToMove doesn't exceed remaining distance to target
            distanceToMove = Mathf.Min(distanceToMove, distanceToTarget);

            // Move the object towards the target
            objectToMove.localPosition += direction * distanceToMove;

            yield return null; // Wait for the end of frame

            // Update the remaining distance to the target
            distanceToTarget = Vector3.Distance(objectToMove.localPosition, targetPosition);
        }

        // Ensure the object reaches the exact target position at the end
        objectToMove.localPosition = targetPosition;
    }

}
