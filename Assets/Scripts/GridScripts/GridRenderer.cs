using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    public int gridSizeX = 5; // Number of grid lines in the X direction
    public int gridSizeY = 5; // Number of grid lines in the Y direction
    public float gridSpacing = 1.0f; // Spacing between grid lines
    public Color gridColor = Color.white; // Color of the grid lines

    public float lineWidth = 0.5f; // Width of grid lines

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            // If there is no LineRenderer component attached, add one.
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set LineRenderer properties
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = gridColor;
        lineRenderer.endColor = gridColor;
        lineRenderer.startWidth = lineWidth; 
        lineRenderer.endWidth = lineWidth; 

        // Create the grid
        DrawGrid();
    }

    private void DrawGrid()
    {
        // Calculate the total width and height of the grid
        float totalWidth = gridSizeX * gridSpacing;
        float totalHeight = gridSizeY * gridSpacing;

        // Calculate the half extents
        float halfWidth = totalWidth / 2.0f;
        float halfHeight = totalHeight / 2.0f;

        // Calculate the number of vertical and horizontal lines
        int numVerticalLines = gridSizeX + 1;
        int numHorizontalLines = gridSizeY + 1;

        // Set the LineRenderer's position count
        lineRenderer.positionCount = ((numVerticalLines + numHorizontalLines) * 2) + 1;

        int index = 0;

        bool leftToRight = true;
        bool bottomToTop = true;

        Vector3 upperLeftCorner = new Vector3(0, 0, 0f);

        // Draw horizontal grid lines
        for (int i = 0; i < numHorizontalLines; i++)
        {
            float y = i * gridSpacing - halfHeight;

            if(leftToRight)
            {
                lineRenderer.SetPosition(index++, new Vector3(-halfWidth, y, 0f));
                lineRenderer.SetPosition(index++, new Vector3(halfWidth, y, 0f));
                upperLeftCorner = new Vector3(-halfWidth, y, 0f);
            }
            else
            {
                lineRenderer.SetPosition(index++, new Vector3(halfWidth, y, 0f));
                lineRenderer.SetPosition(index++, new Vector3(-halfWidth, y, 0f));
            }
            
            leftToRight = !leftToRight;
        }

        if(!leftToRight)
        {
            lineRenderer.SetPosition(index++, upperLeftCorner);
        }

        // Draw vertical grid lines
        for (int i = 0; i < numVerticalLines; i++)
        {
            float x = i * gridSpacing - halfWidth;

            if(bottomToTop)
            {
                lineRenderer.SetPosition(index++, new Vector3(x, -halfHeight, 0f));
                lineRenderer.SetPosition(index++, new Vector3(x, halfHeight, 0f));
            }
            else
            {
                lineRenderer.SetPosition(index++, new Vector3(x, halfHeight, 0f));
                lineRenderer.SetPosition(index++, new Vector3(x, -halfHeight, 0f));
            }
            
            bottomToTop = !bottomToTop;
        }
    }
}
