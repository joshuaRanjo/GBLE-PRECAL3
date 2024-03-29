using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;

public class ParabolaCreator : MonoBehaviour
{
    public float maxX = 5.0f;  // Maximum x value
    public float maxY = 5.0f;  // Maximum y value
    public int pointCount = 30;
    private bool isVertical = true;
    public float a = 1;
    public bool spriteShape = false;
    public bool addCollider = false;
    public bool line = false;
    public bool terrain = false;
    public bool ceiling = false;
    public void CreateObject()
    {
        // Instantiate the object
        GameObject newObject = new GameObject("NewObject");
        List<Vector3> points = DrawParabola();

        if(!spriteShape)
        {

            LineRenderer lineRenderer = newObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = points.Count;
            lineRenderer.useWorldSpace = false;
            lineRenderer.SetPositions(points.ToArray());
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
        }


        if(addCollider)
        {
            if(!line)
            {
                newObject.AddComponent<PolygonCollider2D>();   
            }
            if(line)
            {
                
                EdgeCollider2D edgeCollider = newObject.AddComponent<EdgeCollider2D>();
                Vector2[] arrayV2 = new Vector2[points.Count]; 

                for(int i = 0; i < points.Count; i++)
                {
                    arrayV2[i] = new Vector2(points[i].x,points[i].y);
                }
                edgeCollider.points = arrayV2;
            }
        }
        if(spriteShape)
        {
            UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer = newObject.AddComponent<UnityEngine.U2D.SpriteShapeRenderer>();
            SpriteShapeController shape = newObject.AddComponent<SpriteShapeController>();
            
            shape.spline.Clear();
            /*if(ceiling || a < 0)
            {
                int k = 0;
                for(int i = points.Count - 1; i >= 0 ; i--)
                {
                    shape.spline.InsertPointAt(k, points[i]);
                    shape.spline.SetHeight(k, 1f);
                    shape.spline.SetTangentMode(k, ShapeTangentMode.Continuous);
                    k++;
                }
                
            }
            else { 
                
                for(int i = 0; i < points.Count; i++)
                {
                    shape.spline.InsertPointAt(i, points[i]);
                    shape.spline.SetHeight(i, 1f);
                    shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                }
            }
            */
            if(ceiling )
            {
                points.Reverse();
            }

            for(int i = 0; i < points.Count; i++)
                {
                    shape.spline.InsertPointAt(i, points[i]);
                    shape.spline.SetHeight(i, 1f);
                    shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                }
                
            
            if(terrain)
            {
                float ceilingModifier = 20f;
                if(!ceiling)
                {/*
                    shape.spline.InsertPointAt(0, new Vector3(points[points.Count - 1].x, 20f,0f));
                    shape.spline.InsertPointAt(shape.spline.GetPointCount(), new Vector3(points[0].x, 20f, 0f));
                    */
                    ceilingModifier = -20f;
                }
                
                
                    shape.spline.InsertPointAt(0, new Vector3(points[0].x, ceilingModifier, 0f));
                    shape.spline.InsertPointAt(shape.spline.GetPointCount(), new Vector3(points[points.Count - 1].x, ceilingModifier,0f));
                //}
                
            }
        }
        
        
    }

    public List<Vector3> DrawParabola()
    {
        List<Vector3> points = new List<Vector3>();
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
}

#if UNITY_EDITOR
[CustomEditor(typeof(ParabolaCreator))]
public class ObjectCreationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ParabolaCreator objectCreator = (ParabolaCreator)target;

        if (GUILayout.Button("Create Object"))
        {
            objectCreator.CreateObject();
        }
    }


}
#endif
