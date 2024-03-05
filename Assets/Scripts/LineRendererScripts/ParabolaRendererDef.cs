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

    public void UpdateLine(float newA, bool newIsVertical)
    {
        a = newA;
        isVertical = newIsVertical;

        
        lineRenderer.SetPositions(DrawParabola().ToArray());
    }

    public void UpdateLine()
    {
        a = ldScriptableObject.a;
        isVertical = !ldScriptableObject.orientation;
        ceiling = qdScriptableObject.ceiling;

        
        
        SpriteShapeController shape = lineObject.GetComponent<SpriteShapeController>();

        float shapeHeight = shape.spline.GetHeight(0);
        Debug.Log("Shape height = " + shapeHeight);
        shape.spline.Clear();

        List<Vector3> points = DrawParabola();

        // if LineObject is terrain / has a sprite shape
        if(shape != null)
        {
            if(isVertical)
            {
                if(ceiling)
                {
                    int k = 0;
                    for(int i = points.Count - 1; i >= 0 ; i--)
                    {
                        shape.spline.InsertPointAt(k, points[i]);
                        shape.spline.SetHeight(k, shapeHeight);
                        shape.spline.SetTangentMode(k, ShapeTangentMode.Continuous);
                        k++;
                    }
                    
                }
                else { 
                    for(int i = 0; i < points.Count; i++)
                    {
                        int count = shape.spline.GetPointCount();
                        if(count > 0 )
                        {
                               
                            if(Vector3.Distance(shape.spline.GetPosition(count-1), points[i]) > 0.1f
                                && Vector3.Distance(shape.spline.GetPosition(0), points[i]) > 0.1f  )
                            {
                                shape.spline.InsertPointAt(count, points[i]);
                                shape.spline.SetHeight(count, shapeHeight);
                                shape.spline.SetTangentMode(count, ShapeTangentMode.Continuous);
                            }                             
                        }
                        else{
                                shape.spline.InsertPointAt(count, points[i]);
                                shape.spline.SetHeight(count, shapeHeight);
                                shape.spline.SetTangentMode(count, ShapeTangentMode.Continuous);
                        }
                        
                    }
                }
                if(lineObject.CompareTag("Terrain"))
                { 
                    if(ceiling)
                    {
                        shape.spline.InsertPointAt(0, new Vector3(points[points.Count - 1].x, 20f,0f));
                        shape.spline.InsertPointAt(shape.spline.GetPointCount(), new Vector3(points[0].x, 20f, 0f));
                    }
                    else
                    {
                        shape.spline.InsertPointAt(0, new Vector3(points[0].x, -20f, 0f));
                        shape.spline.InsertPointAt(shape.spline.GetPointCount(), new Vector3(points[points.Count - 1].x, -20f,0f));
                    }
                }
            }
        }
        
        
    }
    
}
