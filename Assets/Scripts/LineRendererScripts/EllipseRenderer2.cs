using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class EllipseRenderer2 : MonoBehaviour
{
    

    [SerializeField] private int resolution = 360;
    private List<Vector3> DrawEllipse(float a, float b)
    {
    
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution * 2 * Mathf.PI;
            float x = a * Mathf.Cos(t);
            float y = b * Mathf.Sin(t);
            points.Add(new Vector3(x, y, 0));
        }

        return points;
    }

    public void UpdateLine(GameObject lineObject)
    {

        PuzzleObject puzzleObject = lineObject.GetComponent<PuzzleObject>();
        float a = puzzleObject.a;
        float b = puzzleObject.b;
        if(puzzleObject.simplifiedEllipse)
        {
            a = Mathf.Sqrt(a);
            b = Mathf.Sqrt(b);
        }
        List<Vector3> points = DrawEllipse(a, b);

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
