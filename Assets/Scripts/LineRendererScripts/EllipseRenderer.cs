using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseRenderer : MonoBehaviour
{

    public float a = 1.0f;
    public float b = 1.0f;
    public int resolution = 360;

    public LineRenderer lineRenderer;

    void DrawEllipse()
    {
        
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = resolution + 1;
        Vector3[] points = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution * 2 * Mathf.PI;
            float x = a * Mathf.Cos(t);
            float y = b * Mathf.Sin(t);
            points[i] = new Vector3(x, y, 0f);
        }

        lineRenderer.SetPositions(points);
    }

    public void DrawEllipse(float newA, float newB)
    {

        a = newA;
        b = newB;
        DrawEllipse();

    }
}
