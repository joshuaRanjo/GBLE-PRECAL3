using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;

public class CircleCreator : MonoBehaviour
{
    public float a = 1;
    public float height = 0.2f;
    public int segments = 360;

    public void CreateObject()
    {
        // Instantiate the object
        GameObject newObject = new GameObject("NewObject");
        List<Vector3> points = DrawCircle(a);

        UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer = newObject.AddComponent<UnityEngine.U2D.SpriteShapeRenderer>();
        SpriteShapeController shape = newObject.AddComponent<SpriteShapeController>();
        shape.fillPixelsPerUnit = 512;
        shape.spline.Clear();
        Debug.Log(points.Count);
        for(int i = 0; i < points.Count; i++)
        {
            int count = shape.spline.GetPointCount();
            if(count > 0 )
            {
                    
                if(Vector3.Distance(shape.spline.GetPosition(count-1), points[i]) > 0.05f
                    && Vector3.Distance(shape.spline.GetPosition(0), points[i]) > 0.05f  )
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

    public List<Vector3> DrawCircle(float radius)
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

}

#if UNITY_EDITOR
[CustomEditor(typeof(CircleCreator))]
public class CircleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CircleCreator objectCreator = (CircleCreator)target;

        if (GUILayout.Button("Create Object"))
        {
            objectCreator.CreateObject();
        }
    }


}
#endif
