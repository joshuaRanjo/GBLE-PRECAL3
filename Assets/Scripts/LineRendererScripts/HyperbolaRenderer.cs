using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class HyperbolaRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;
    public float a = 1f; // Semi-major axis length
    public float b = 1f; // Semi-minor axis length
    public int resolution = 100; // Number of points to generate


    public void DrawHyperbola(float newA, float newB, string orientation)
    {
        a = newA; b = newB; 
       
        if(orientation == "Vertical")
        {
            //DrawVerticalHyperbola();
        }
        else
        {
           // DrawHorizontalHyperbola();
        }
    }

    public void UpdateLine(GameObject lineObject)
    {
        ParabolaObject puzzleObject = lineObject.GetComponent<ParabolaObject>();
        a = puzzleObject.a;
        b = puzzleObject.b;
        if(puzzleObject.simplifiedEllipse)
        {
            a = Mathf.Sqrt(a);
            b = Mathf.Sqrt(b);
        }
        List<Vector3> line1;
        List<Vector3> line2;
        if(puzzleObject.orientation) // True = horizontal
        {
            DrawHorizontalHyperbola(out line1, out line2);
        }
        else
        {
            DrawVerticalHyperbola(out line1, out line2);
        }

        line1 = AdjustLineLength(line1, puzzleObject.maxLineLength);
        line2 = AdjustLineLength(line2, puzzleObject.maxLineLength);

        line1.RemoveAt(0);
        line2.RemoveAt(0);

        if(puzzleObject.split)
        {
            GameObject child1 = lineObject.transform.GetChild(0).gameObject;
            GameObject child2 = lineObject.transform.GetChild(1).gameObject;

            SpriteShapeController shape1 = child1.GetComponent<SpriteShapeController>();
            SpriteShapeController shape2 = child2.GetComponent<SpriteShapeController>();
            float height = shape1.spline.GetHeight(0);

            shape1.spline.Clear();
            shape2.spline.Clear();

            for(int i = 0; i < line2.Count; i++)
            {
                shape2.spline.InsertPointAt(i, line2[i]);
                shape2.spline.SetHeight(i, height);
                shape2.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }
            for(int i = 0; i < line1.Count; i++)
            {
                shape1.spline.InsertPointAt(i, line1[i]);
                shape1.spline.SetHeight(i, height);
                shape1.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }
        }
        else
        {
            SpriteShapeController shape1 = puzzleObject.GetComponent<SpriteShapeController>();
            float height = shape1.spline.GetHeight(0);
            shape1.spline.Clear();
            for(int i = 0; i < line2.Count; i++)
            {
                shape1.spline.InsertPointAt(i, line2[i]);
                shape1.spline.SetHeight(i, height);
                shape1.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }
            for(int i = 0; i < line1.Count; i++)
            {
                shape1.spline.InsertPointAt(i, line1[i]);
                shape1.spline.SetHeight(i, height);
                shape1.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }

        }
    }

    public void DrawHorizontalHyperbola(out List<Vector3> list1, out List<Vector3> list2)
    {
        int position= 0;
        list1 = new List<Vector3>();
        list2 = new List<Vector3>();
        for (float i = -10.0f; i <= 10.0f; i += 0.1f)
        {
            
            if (position < 200)
            {
                float x = Mathf.Sqrt((a*a) + (((a*a)*(i*i))/(b*b)));

                list1.Add(new Vector3(x,i,0));
                list2.Add(new Vector3(-x,i,0));
                
                position = position + 1;
            }
        }

    }

    private void DrawVerticalHyperbola(out List<Vector3> list1, out List<Vector3> list2)
    {
        
        list1 = new List<Vector3>();
        list2 = new List<Vector3>();
        int position= 0;
        for (float i = -10.0f; i <= 10.0f; i += 0.1f)
        {
            
            if (position < 200)
            {
                float y = Mathf.Sqrt((b*b) + (((b*b)*(i*i))/(a*a)));

                list1.Add(new Vector3(i,y,0));
                list2.Add(new Vector3(i,-y,0));
                position = position + 1;
            }
        }
    }
    
        private List<Vector3> AdjustLineLength(List<Vector3> points, float maxLength)
    {
        float totalLength = CalculateLineLength(points);

        while (totalLength > maxLength && points.Count > 2)
        {
            points = RemoveFirstAndLastPoints(points);
            totalLength = CalculateLineLength(points);
        }

        //Debug.Log("Final length of the line: " + totalLength);
        return points;
    }

    private List<Vector3> RemoveFirstAndLastPoints(List<Vector3> points)
    {
        points.RemoveAt(0); // Remove first point
        points.RemoveAt(points.Count - 1); // Remove last point
        return points;
    }

    private float CalculateLineLength(List<Vector3> points)
    {
        float totalLength = 0f;

        for (int i = 1; i < points.Count; i++)
        {
            totalLength += Vector3.Distance(points[i - 1], points[i]);
        }

        return totalLength;
    }
}
