using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            DrawVerticalHyperbola();
        }
        else
        {
            DrawHorizontalHyperbola();
        }
    }

    void DrawHorizontalHyperbola()
    {


        lineRenderer1.positionCount = 61;
        lineRenderer2.positionCount = 61;

        int position= 0;
        Vector3[] points_line1 = new Vector3[61];
        Vector3[] points_line2 = new Vector3[61];
        for (float i = -3.0f; i <= 3.0f; i += 0.1f)
        {
            
            if (position < 61)
            {
                float y =  Mathf.Sqrt((b*b) * ( (i *i ) / (a*a)));
                float x = Mathf.Sqrt((a*a) * (1 + ( (i * i) / (b*b) )));
                if(i < 0){
                    x = x*-1;

                    points_line2[position] = new Vector3(x,y,0);
                    points_line1[position] = new Vector3((x*-1),(y*-1),0);
                }
                else{
                    points_line1[position] = new Vector3(x,y,0);
                    points_line2[position] = new Vector3((x*-1),(y*-1),0);
                } 
                 
                
                //colliderPoints[position] = new Vector2( (i + h + centerX), (a * (i * i)) + k + centerY);
                
                position = position + 1;
            }
        }

        
        lineRenderer1.SetPositions(points_line1);
        lineRenderer2.SetPositions(points_line2);


    }

    private void DrawVerticalHyperbola()
    {
  

        lineRenderer1.positionCount = 61;
        lineRenderer2.positionCount = 61;

        int position= 0;
        Vector3[] points_line1 = new Vector3[61];
        Vector3[] points_line2 = new Vector3[61];
        for (float i = -3.0f; i <= 3.0f; i += 0.1f)
        {
            
            if (position < 61)
            {
                float x =  Mathf.Sqrt((b*b) * ( (i *i ) / (a*a)));
                float y = Mathf.Sqrt((a*a) * (1 + ( (i * i) / (b*b) )));

                if(i < 0){
                    x = x*-1;
                }
                float line2X = x;
                float line2Y = y;
 
                points_line1[position] = new Vector3(x,y,0);

                x = (line2X * -1);
                y = (line2Y * -1);

                points_line2[position] = new Vector3(x,y,0);
                
                position = position + 1;
            }
        }

        lineRenderer1.SetPositions(points_line1);
        lineRenderer2.SetPositions(points_line2);


    }
}
