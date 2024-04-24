using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;

public class HyperbolaCreator : MonoBehaviour
{

    public int pointCount = 100;
    public bool isVertical = true;
    public float a = 1;
    public float b = 1;
    public bool spriteShape = false;
    public bool split = true;

    public bool line = false;
    public bool terrain = false;
    public bool ceiling = false;
    public void CreateObject()
    {
        // Instantiate the object
        GameObject newObject = new GameObject("NewObject");
        UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer1;
        UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer2;

        SpriteShapeController shape1;
        SpriteShapeController shape2 = null;



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

        line1 = AdjustLineLength(line1, 20f);
        line2 = AdjustLineLength(line2, 20f);

        line1.RemoveAt(0);
        line2.RemoveAt(0);

        if(!split) // for hyperbola that is not split
        {
            for(int i = 0; i < line2.Count; i++)
            {
                shape1.spline.InsertPointAt(i, line2[i]);
                shape1.spline.SetHeight(i, 0.1f);
                shape1.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }
            for(int i = 0; i < line1.Count; i++)
            {
                shape1.spline.InsertPointAt(i, line1[i]);
                shape1.spline.SetHeight(i, 0.1f);
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
                    shape2.spline.SetHeight(i, 0.1f);
                    shape2.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                }
                for(int i = 0; i < line1.Count; i++)
                {
                    shape1.spline.InsertPointAt(i, line1[i]);
                    shape1.spline.SetHeight(i, 0.1f);
                    shape1.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                }
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
