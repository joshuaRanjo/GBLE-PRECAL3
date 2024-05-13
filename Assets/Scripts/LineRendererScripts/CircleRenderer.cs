using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CircleRenderer : MonoBehaviour
{


    public int segments = 360;

    public List<Vector3> GenerateCirclePoints(float radius)
    {
        List<Vector3> points = new List<Vector3>();

        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            points.Add(new Vector3(x, y, 0));
        }

        return points;
    }

    public void UpdateLine(GameObject lineObject)
    {
        PuzzleObject puzzleObject = lineObject.GetComponent<PuzzleObject>();
        List<Vector3> points = GenerateCirclePoints(puzzleObject.a);

        SpriteShapeController shape = lineObject.GetComponent<SpriteShapeController>();
        float height = shape.spline.GetHeight(0);
        shape.spline.Clear();

        for(int i = 0; i < points.Count; i++)
        {
            int count = shape.spline.GetPointCount();
            if(count > 0 )
            {
                    
                if(Vector3.Distance(shape.spline.GetPosition(count-1), points[i]) > 0.1f
                    && Vector3.Distance(shape.spline.GetPosition(0), points[i]) > 0.1f  )
                {
                    shape.spline.InsertPointAt(count, points[i]);
                    shape.spline.SetHeight(count, height);
                    shape.spline.SetTangentMode(count, ShapeTangentMode.Continuous);
                }                             
            }
            else{
                    shape.spline.InsertPointAt(count, points[i]);
                    shape.spline.SetHeight(count, height);
                    shape.spline.SetTangentMode(count, ShapeTangentMode.Continuous);
            }
            
        }
    }
}
