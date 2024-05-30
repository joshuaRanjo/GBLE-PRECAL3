using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedObjectLine : LevelProp
{

    public Transform transform1;
    public Transform transform2;

    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // Get the Line Renderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Set the number of positions to 2 (start and end points)
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // Check if the transforms are not null
            if (transform1 == null || transform2 == null)
            {
                throw new System.NullReferenceException("One or both of the transforms are null");
            }

            // Update the positions of the Line Renderer to match the transforms
            lineRenderer.SetPosition(0, transform1.position);
            lineRenderer.SetPosition(1, transform2.position);
        }
        catch (System.NullReferenceException ex)
        {
            Debug.LogError(ex.Message);
            // Optionally, disable the Line Renderer if transforms are null
            lineRenderer.enabled = false;
        }
    }
}
