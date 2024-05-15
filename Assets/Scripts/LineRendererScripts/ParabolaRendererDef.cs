using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;


public class ParabolaRendererDef : MonoBehaviour
{
    [SerializeField] private LineData ldScriptableObject;
    [SerializeField] private QuestionData qdScriptableObject;
    private float a = 1.0f;  // The 'a' coefficient in the y = ax^2 equation
    public int pointCount = 100;  // Number of points to draw the parabola
    public float maxX = 5.0f;  // Maximum x value
    public float maxY = 5.0f;  // Maximum y value

    private LineRenderer lineRenderer;
    private GameObject lineObject;
    
    private bool isVertical = true;
    private bool ceiling;

    public void SetLineObject(GameObject newLineObject)
    {
        lineObject = newLineObject;
    }

    public List<Vector3> DrawParabola()
    {
        List<Vector3> points = new List<Vector3>();

        // vertical \/  /\
        //horizontal ><
        if(a != 0)
        {
            float xStep = (maxX - -maxX) / (pointCount - 1);
            // If vertical Axis
            for (int i = 0; i < pointCount; i++)
            {
                float x = -maxX + i * xStep; // Vary 'x' from -1 to 1
                float y = a * x * x;

                if ( y >= -maxY && y <= maxY)
                {
                    if(x >= -maxX && x <=maxX)
                    {
                        Vector3 point = new Vector3(x, y, 0);
                        points.Add(point);
                    }
                    
                }
            }
            int verticalModifier = 1;
            if(a < 0)
            {
                verticalModifier = -1;
            }
            
            // Adds last points to the line so that it is symmetrical on both ends
            
            float lastX = Mathf.Sqrt(verticalModifier*maxY/a);
            float lastY = a * maxX * maxX;

            if(lastX <= maxX && lastX >= -maxX && lastX != maxX)
            {
                points.Insert(0, new Vector3(-lastX,maxY*verticalModifier,0));
                points.Add(new Vector3(lastX,maxY*verticalModifier,0));
            }
            /*
            float lastY = a * maxX * maxX;

            points.Insert(0, new Vector3(-maxX, lastY*verticalModifier,0));
            points.Add(new Vector3(maxX,lastY*verticalModifier,0))
            */

        }            
        else
        {

            float x,y;
            if(isVertical){ x = 10; y = 0;}
            else{ x = 0; y = 10;}
            points.Add(new Vector3(-x,-y,0));
            points.Add(new Vector3(x,y,0));
        }
        
        return points;
    }




    public void UpdateLineSpriteShape(GameObject lineObject)
    {
        ParabolaObject puzzleObject = lineObject.GetComponent<ParabolaObject>();
        List<Vector3> points = GetParabolaPoints(puzzleObject.a, puzzleObject.xLimit, puzzleObject.yLimit, puzzleObject.orientation);
        points = AdjustLineLength(points, puzzleObject.maxLineLength);

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

    public void UpdateLineLineRenderer(GameObject lineObject)
    {
        ParabolaObject puzzleObject = lineObject.GetComponent<ParabolaObject>();
        List<Vector3> points = GetParabolaPoints(puzzleObject.a, puzzleObject.xLimit, puzzleObject.yLimit, puzzleObject.orientation);
        points = AdjustLineLength(points, puzzleObject.maxLineLength);
    } 
    
    public List<Vector3> GetParabolaPoints(float a, float maxX, float maxY, bool orientation)
    {
        List<Vector3> points = new List<Vector3>();
        if(a != 0)
        {
            
            
            float xStep = (maxX - -maxX) / (pointCount - 1);
            // If vertical axis
            if(!orientation)
            {
                for (int i = 0; i < pointCount; i++)
                {
                    float x = -maxX + i * xStep; // Vary 'x' from -1 to 1
                    //float y = a * x * x;
                    float y = (x*x)/(4*a);// y = x^2/4a

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
            }
            else //if Horizontal axis
            {
                float yStep = (maxY- - maxY) / (pointCount - 1);
                for(int i = 0; i < pointCount; i++)
                {
                    float y = -maxY + i * yStep;
                    //float x = a * y * y;
                    float x = (y*y)/(4*a); // x = y^2/4a
                    if (x >= -maxX && x <= maxX)
                    {
                        Vector3 point = new Vector3(x,y,0);
                        points.Add(point);
                    }
                }
                int horizontalModifier = 1;
                if(a < 0 )
                {
                    horizontalModifier = -1;
                }
                float lastY = Mathf.Sqrt(horizontalModifier*maxX/a);
                points.Insert(0, new Vector3(-lastY,maxX*horizontalModifier,0));
                points.Add(new Vector3(lastY, maxX*horizontalModifier,0));
            }
        }
        else
        {

            float x,y;
            if(isVertical){ x = 3; y = 0;}
            else{ x = 0; y = 3;}

            points.Insert(0, new Vector3(-x,-y,0));
            points.Insert(1, new Vector3(x,y,0));
        }
        return points;
        
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
