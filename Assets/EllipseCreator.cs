using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEditor;

public class EllipseCreator : MonoBehaviour
{
    public float a = 1;
    public float b = 1;
    public float height = 0.1f;
    public int resolution = 720;
    public bool simplifiedEllipse = false;

    public void CreateObject()
    {
        // Instantiate the object
        GameObject newObject = new GameObject("NewObject");
        List<Vector3> points = DrawEllipse();

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

    private List<Vector3> DrawEllipse()
    {
        float a = this.a;
        float b = this.b;

        if(simplifiedEllipse)
        {
            a = Mathf.Sqrt(a);
            b = Mathf.Sqrt(b);
        }
    
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

}

#if UNITY_EDITOR
[CustomEditor(typeof(EllipseCreator))]
public class EllipseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EllipseCreator objectCreator = (EllipseCreator)target;

        if (GUILayout.Button("Create Object"))
        {
            objectCreator.CreateObject();
        }
    }


}
#endif
