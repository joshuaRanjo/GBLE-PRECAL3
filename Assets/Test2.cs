using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeRenderer))]
public class Test2 : MonoBehaviour
{
    public float radius = 1f;
    public int numPoints = 100;

    void Start()
    {
        SpriteShapeController spriteShapeController = GetComponent<SpriteShapeController>();

        // Clear any existing points
        spriteShapeController.spline.Clear();

        // Calculate angle increment between each point
        float angleIncrement = 360f / numPoints;

        // Generate points for the circle
        for (int i = 0; i < numPoints; i++)
        {
            // Calculate angle for this point
            float angle = Mathf.Deg2Rad * (i * angleIncrement);

            // Calculate position of the point on the circle
            Vector2 pointPosition = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);

            // Add the point to the spline
            spriteShapeController.spline.InsertPointAt(i, pointPosition);
        }

        
    }
}
