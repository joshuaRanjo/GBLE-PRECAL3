using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : LevelProp
{
    public List<Vector3> positions;  // List of positions the platform will move to
    public float speed = 5f;         // Speed of the platform
    private GameObject player;
    private Coroutine moveCoroutine;

    private void OnDestroy() {
        if(player != null)
        {
            player.transform.SetParent(null);
        }
    }

    private void OnDisable() {
    }

    public void MoveToTargetIndex(int index)
    {
        if (index < 0 || index >= positions.Count)
        {
            Debug.LogWarning("Invalid target index for MovingPlatform.");
            return;
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveToPosition(index));
    }

    private IEnumerator MoveToPosition(int targetIndex)
    {
        Vector3 targetPosition = positions[targetIndex];

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
            player = null;
        }
    }
}