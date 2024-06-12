using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeys : LevelProp
{
    [SerializeField] private bool showFoci = true;
    [SerializeField] private bool showVertex = true;
    [SerializeField] private GameObject vertexPoint;
    [SerializeField] private GameObject vertexPoint2;
    [SerializeField] private GameObject fociPoint1;
    [SerializeField] private GameObject fociPoint2;

    private int conicType;
    private bool firstRun = true;

    private float a,b,h,k,c,d, x1,x2,y1,y2;

    private PuzzleObject poScript;
    // Start is called before the first frame update
    private void Start()
    {
        PuzzleObject puzzleObject = GetComponent<PuzzleObject>();
        ParabolaObject parabolaObject= GetComponent<ParabolaObject>();
        
        GameObject fociPoint = Resources.Load<GameObject>("ObjectPrefabs/" + "fociPoint");
        GameObject vertexPoint1 = Resources.Load<GameObject>("ObjectPrefabs/" + "vertexPoint");

        if(showVertex)
        {
            vertexPoint = Instantiate(vertexPoint1, Vector3.zero, Quaternion.identity);
            vertexPoint.transform.SetParent(transform.parent);
            vertexPoint.GetComponent<PointKeyScript>().puzzleObject = puzzleObject;
            vertexPoint.GetComponent<PointKeyScript>().parabolaObject = parabolaObject;
            vertexPoint.GetComponent<PointKeyScript>().SetUpClickableScript();

        }
        
        if(showFoci)
        {
            fociPoint1 = Instantiate(fociPoint, Vector3.zero, Quaternion.identity);
            fociPoint1.transform.SetParent(transform.parent);
            fociPoint1.GetComponent<PointKeyScript>().puzzleObject = puzzleObject;
            fociPoint1.GetComponent<PointKeyScript>().parabolaObject = parabolaObject;    
            fociPoint1.GetComponent<PointKeyScript>().SetUpClickableScript();
        }
            
        if(conicType != 3)
        {
            if(showFoci)
            {
                fociPoint2 = Instantiate(fociPoint, Vector3.zero, Quaternion.identity);
                fociPoint2.transform.SetParent(transform.parent);
                fociPoint2.GetComponent<PointKeyScript>().puzzleObject = puzzleObject;
                fociPoint2.GetComponent<PointKeyScript>().parabolaObject = parabolaObject;
                fociPoint2.GetComponent<PointKeyScript>().SetUpClickableScript();
            }
            
        }
        if(conicType == 4 || conicType == 2)
        {   
            if(showVertex)
            {
                vertexPoint2 = Instantiate(vertexPoint1, Vector3.zero, Quaternion.identity);
                vertexPoint2.transform.SetParent(transform.parent);
                vertexPoint2.GetComponent<PointKeyScript>().puzzleObject = puzzleObject;
                vertexPoint2.GetComponent<PointKeyScript>().parabolaObject = parabolaObject;
                vertexPoint2.GetComponent<PointKeyScript>().SetUpClickableScript();
            }
        }



        UpdatePoints();
    }

    private void OnEnable() {
        poScript = GetComponent<PuzzleObject>();
        conicType = poScript.conicType;
        poScript.updateObjectCalled += UpdatePoints;

    }

    private void OnDisable()
    {
        poScript.updateObjectCalled -= UpdatePoints;
    }

    private void UpdatePoints()
    {
        h = poScript.h;
        k = poScript.k;
        a = poScript.a;
        b = poScript.b;

        if(showVertex)
        {
            vertexPoint.transform.position = new Vector3(h,k, 0f);
        }
            
        if(conicType == 2)
        {
            if(showFoci)
            {
                c = 0;
                d = 0;
                if(!poScript.simplifiedEllipse)
                {

                    a = a*a;
                    b = b*b;
                }
                if(a > b)
                {
                    c = Mathf.Sqrt(a-b);
                }
                if(a < b)
                {
                    d = Mathf.Sqrt(b-a);
                }
                if(a == b)
                {
                    c = 0;
                    d = 0;
                }
                x1 = h+c;
                y1 = k+d;
                x2 = h-c;
                y2 = k-d;
            
                fociPoint1.transform.position = new Vector3(x1,y1,0f);
                fociPoint2.transform.position = new Vector3(x2,y2,0f);  
            }
            if(showVertex)
            {
                float sqrtA = Mathf.Sqrt(a);
                float sqrtB = Mathf.Sqrt(b);
                
                if(a>b)
                {
                    vertexPoint.transform.position = new Vector3(h + sqrtA,k, 0f);
                    vertexPoint2.transform.position = new Vector3(h - sqrtA,k, 0f);
                }
                else if(b > a)
                {
                    vertexPoint.transform.position = new Vector3(h,k + sqrtB, 0f);
                    vertexPoint2.transform.position = new Vector3(h,k - sqrtB, 0f);
                }
                else if( a == b)
                {
                    vertexPoint.transform.position = new Vector3(h,k, 0f);
                    vertexPoint2.transform.position = new Vector3(h,k, 0f);
                }
                
            }
        }
        
        if(conicType == 3)
        {   if(showFoci)
            {
                if(a != 0f)
                {
                    if(poScript.orientation)
                    {
                        //x1 = (h + (1/(4*a)));
                        x1 = h+a;
                        y1 = k;
                        fociPoint1.transform.position = new Vector3(x1, y1,0);
                    }
                    else
                    {
                        x1 = h;
                        //y1 = (k + (1/(4*a)));
                        y1 = k + a;
                        fociPoint1.transform.position = new Vector3(x1, y1,0);
                    }
                }
                else
                {
                    fociPoint1.transform.position = new Vector3(h, k,0);
                }
            }
        }
        if(conicType == 4)
        {
            c = 0;
            d = 0;
            if(poScript.simplifiedEllipse)
            {
                a = Mathf.Sqrt(a);
                b = Mathf.Sqrt(b);
            }
            if(showVertex)
            {
                if(poScript.orientation)
                {
                    
                    vertexPoint.transform.position = new Vector3(h+a,k, 0f);
                    vertexPoint2.transform.position = new Vector3(h-a,k, 0f);
                }
                else
                {
                    
                    vertexPoint.transform.position = new Vector3(h,k+b, 0f);
                    vertexPoint2.transform.position = new Vector3(h,k-b, 0f);
                }
            }
            if(showFoci)
            {
                if(poScript.orientation)
                {
                    c = Mathf.Sqrt((a*a) + (b*b));
                }
                else
                {
                    d = Mathf.Sqrt((a*a) + (b*b));
                }

                x1 = h+c;
                y1 = k+d;
                x2 = h-c;
                y2 = k-d;
                fociPoint1.transform.position = new Vector3(x1,y1,0f);
                fociPoint2.transform.position = new Vector3(x2,y2,0f);
            }
        }

    }


}
