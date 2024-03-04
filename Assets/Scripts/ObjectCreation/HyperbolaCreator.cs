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

    public bool line = false;
    public bool terrain = false;
    public bool ceiling = false;
    public void CreateObject()
    {
        // Instantiate the object
        GameObject newObject = new GameObject("NewObject");
        List<Vector3> line1;
        List<Vector3> line2;

        if(isVertical)
        {
            DrawVerticalHyperbola(out line1, out line2);
        }
        else{
            DrawHorizontalHyperbola(out line1, out line2);
        }
        

        UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer = newObject.AddComponent<UnityEngine.U2D.SpriteShapeRenderer>();
        SpriteShapeController shape = newObject.AddComponent<SpriteShapeController>();
            
        shape.spline.Clear();



        for(int i = 0; i < line2.Count; i++)
        {
            shape.spline.InsertPointAt(i, line2[i]);
            shape.spline.SetHeight(i, 0.1f);
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
        }
                for(int i = 0; i < line1.Count; i++)
        {
            shape.spline.InsertPointAt(i, line1[i]);
            shape.spline.SetHeight(i, 0.1f);
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
        }
    }

    public void DrawHorizontalHyperbola(out List<Vector3> list1, out List<Vector3> list2)
    {
        int position= 0;
        list1 = new List<Vector3>();
        list2 = new List<Vector3>();
        for (float i = -4.0f; i <= 4.0f; i += 0.1f)
        {
            
            if (position < 81)
            {
                float y =  Mathf.Sqrt((b*b) * ( (i *i ) / (a*a)));
                float x = Mathf.Sqrt((a*a) * (1 + ( (i * i) / (b*b) )));
                if(i < 0){
                    x = x*-1;

                    list2.Add(new Vector3(x,y,0));
                    list1.Add(new Vector3((x*-1),(y*-1),0));
                    
                }
                else{
                    list1.Add(new Vector3(x,y,0));
                    list2.Add(new Vector3((x*-1),(y*-1),0));
                } 
                
                position = position + 1;
            }
        }
        list1.Reverse();
        list2.Reverse();
    }

    private void DrawVerticalHyperbola(out List<Vector3> list1, out List<Vector3> list2)
    {
        
        list1 = new List<Vector3>();
        list2 = new List<Vector3>();
        int position= 0;
        for (float i = -4.0f; i <= 4.0f; i += 0.1f)
        {
            
            if (position < 81)
            {
                float x =  Mathf.Sqrt((b*b) * ( (i *i ) / (a*a)));
                float y = Mathf.Sqrt((a*a) * (1 + ( (i * i) / (b*b) )));

                if(i < 0){
                    x = x*-1;
                }
                float line2X = x;
                float line2Y = y;
 
                list1.Add(new Vector3(x,y,0));

                x = (line2X * -1);
                y = (line2Y * -1);

                list2.Add(new Vector3(x,y,0));
                
                position = position + 1;
            }
        }
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
