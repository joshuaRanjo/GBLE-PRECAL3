using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TestScript : MonoBehaviour
{
    private void Start() {
        TestSpline();
        DrawParabola();
    }
    public void TestFunction()
    {
        Debug.Log("TestScript Triggered");
    }

    public SpriteShapeController shape;

    public void TestSpline()
    {
        shape = GetComponent<SpriteShapeController>();

        shape.spline.Clear();
    }

    
    
    public float maxX = 5.0f;  // Maximum x value
    public float maxY = 5.0f;  // Maximum y value
    public int pointCount = 30;
    private bool isVertical = true;

    public void DrawParabola()
    {
        float a = -2f;
        bool flip = false;
        if(a != 0)
        {
            List<Vector3> points = new List<Vector3>();

            float xStep = (maxX - -maxX) / (pointCount - 1);
            // If vertical Axis
            for (int i = 0; i < pointCount; i++)
            {
                float x = -maxX + i * xStep; // Vary 'x' from -1 to 1
                float y = a * x * x;

                if ( y >= -maxY && y <= maxY)
                {
                    Vector3 point = new Vector3(x, y, 0);
                    points.Add(point);
                }
                
            }

            int verticalModifier = 1;
            if(a < 0)
            {
                verticalModifier = -1;
            }

            float lastX = Mathf.Sqrt(verticalModifier*maxY/a);
            points.Insert(0, new Vector3(-lastX,maxY*verticalModifier,0));
            points.Add(new Vector3(lastX,maxY*verticalModifier,0));



            for(int i = 0; i <points.Count; i++)
            {
                shape.spline.InsertPointAt(i, points[i]);
                shape.spline.SetHeight(i, 1f);
                shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }
            
            shape.spline.InsertPointAt(0, new Vector3(points[0].x, -20f, 0f));
            shape.spline.InsertPointAt(shape.spline.GetPointCount(), new Vector3(points[points.Count - 1].x, -20f,0f));

        }
        else
        {

            float x,y;
            if(isVertical){ x = 0; y = 3;}
            else{ x = 3; y = 0;}
        }
        
    }
}
