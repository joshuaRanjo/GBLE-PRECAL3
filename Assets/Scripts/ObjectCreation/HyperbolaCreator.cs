using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;

public class HyperbolaCreator : MonoBehaviour
{
    public SpriteShape profile;
    public int pointCount = 100;
    public float lineLength = 10f;
    public float height = 1f;
    public bool isVertical = true;
    public bool simplified = false;
    public float aSet = 1;
    public float bSet = 1;
    public bool spriteShape = false;
    public bool split = true;

    public bool line = false;
    public bool terrain = false;
    public bool ceiling = false;
    private float a;
    private float b;
    public void CreateObject()
    {
        // Instantiate the object
        GameObject newObject = new GameObject("NewObject");
        UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer1;
        UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer2;

        SpriteShapeController shape1;
        SpriteShapeController shape2 = null;

        a = aSet;
        b = bSet;

        if(simplified)
        {
            a = Mathf.Sqrt(a);
            b = Mathf.Sqrt(b);
        }

        GameObject child1;
        GameObject child2;

        if(split)
        {
            child1 = new GameObject("Child1");
            child2 = new GameObject("Child2");

            spriteShapeRenderer1 = child1.AddComponent<UnityEngine.U2D.SpriteShapeRenderer>();
            spriteShapeRenderer2 = child2.AddComponent<UnityEngine.U2D.SpriteShapeRenderer>();

            shape1 = child1.AddComponent<SpriteShapeController>();
            shape2 = child2.AddComponent<SpriteShapeController>();

            shape1.spline.Clear();
            shape2.spline.Clear();

            child1.transform.SetParent(newObject.transform);
            child2.transform.SetParent(newObject.transform);

            shape1.spriteShape = profile;
            shape2.spriteShape = profile;
        }
        else
        {
            spriteShapeRenderer1 = newObject.AddComponent<UnityEngine.U2D.SpriteShapeRenderer>();
            shape1 = newObject.AddComponent<SpriteShapeController>();
            
        }


        List<Vector3> line1;
        List<Vector3> line2;

        if(isVertical)
        {
            DrawVerticalHyperbola(out line1, out line2);
        }
        else{
            DrawHorizontalHyperbola(out line1, out line2);
        }

        line1 = AdjustLineLength(line1, lineLength);
        line2 = AdjustLineLength(line2, lineLength);

        line1.RemoveAt(0);
        line2.RemoveAt(0);

        if(!split) // for hyperbola that is not split
        {
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
        else
        {   
            if(shape2 != null)
            {
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
            
        }
        if(ceiling)
        {
            int p1Count = shape1.spline.GetPointCount();
            int p2Count = shape2.spline.GetPointCount();

            Vector3 s1fp = shape1.spline.GetPosition(0);
            Vector3 s1lp = shape1.spline.GetPosition(p1Count - 1);

            Vector3 s2fp = shape2.spline.GetPosition(0);
            Vector3 s2lp = shape2.spline.GetPosition(p2Count - 1);

            if(isVertical)
            {
                s1fp = s1fp + new Vector3(0,15f,0);
                s1lp = s1lp + new Vector3(0,15f,0);

                s2fp = s2fp - new Vector3(0,15f,0);
                s2lp = s2lp - new Vector3(0,15f,0);
            }
            else
            {
               
                s1fp = s1fp + new Vector3(15f,0,0);
                s1lp = s1lp + new Vector3(15f,0,0);
                

        
                s2fp = s2fp - new Vector3(15f,0,0);
                s2lp = s2lp - new Vector3(15f,0,0);
                
            }

            shape1.spline.SetPosition(0, s1fp);
            shape1.spline.SetPosition(p1Count - 1, s1lp);
            shape2.spline.SetPosition(0, s2fp);
            shape2.spline.SetPosition(p2Count - 1, s2lp);
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
        for (float i = -20.0f; i <= 20.0f; i += 0.1f)
        {
            
            if (position < 400)
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

#if UNITY_EDITOR
[CustomEditor(typeof(HyperbolaCreator))]
public class HyperbolaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HyperbolaCreator objectCreator = (HyperbolaCreator)target;

        if (GUILayout.Button("Create Object"))
        {
            objectCreator.CreateObject();
        }
    }


}
#endif
