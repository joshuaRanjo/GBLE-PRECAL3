using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointKeys : LevelProp
{

    [SerializeField] private GameObject vertexPoint;
    [SerializeField] private GameObject vertexPoint2;
    [SerializeField] private GameObject fociPoint1;
    [SerializeField] private GameObject fociPoint2;

    private int conicType;

    private float a,b,h,k,c,d, x1,x2,y1,y2;

    private PuzzleObject poScript;
    // Start is called before the first frame update
    private void Start()
    {
        
        
        GameObject fociPoint = Resources.Load<GameObject>("ObjectPrefabs/" + "fociPoint");
        GameObject vertexPoint1 = Resources.Load<GameObject>("ObjectPrefabs/" + "vertexPoint");

        vertexPoint = Instantiate(vertexPoint1, Vector3.zero, Quaternion.identity);
        fociPoint1 = Instantiate(fociPoint, Vector3.zero, Quaternion.identity);

        vertexPoint.transform.SetParent(transform.parent);
        fociPoint1.transform.SetParent(transform.parent);

        vertexPoint.GetComponent<PointKeyScript>().SetUpClickableScript();
        fociPoint1.GetComponent<PointKeyScript>().SetUpClickableScript();
        if(conicType != 3)
        {
            fociPoint2 = Instantiate(fociPoint, Vector3.zero, Quaternion.identity);
            fociPoint2.transform.SetParent(transform.parent);
            fociPoint2.GetComponent<PointKeyScript>().SetUpClickableScript();
        }
        if(conicType == 4)
        {
            vertexPoint2 = Instantiate(vertexPoint1, Vector3.zero, Quaternion.identity);
            vertexPoint2.transform.SetParent(transform.parent);
            vertexPoint2.GetComponent<PointKeyScript>().SetUpClickableScript();
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
        vertexPoint.transform.position = new Vector3(h,k, 0f);
        if(conicType == 2)
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
            x1 = h+c;
            y1 = k+d;
            x2 = h-c;
            y2 = k-d;
            fociPoint1.transform.position = new Vector3(x1,y1,0f);
            fociPoint2.transform.position = new Vector3(x2,y2,0f);

        }
        
        if(conicType == 3)
        {
            if(poScript.orientation)
            {
                x1 = (h + (1/(4*a)));
                y1 = k;
                fociPoint1.transform.position = new Vector3(x1, y1,0);
            }
            else
            {
                x1 = h;
                y1 = (k + (1/(4*a)));
                fociPoint1.transform.position = new Vector3(x1, y1,0);
            }
        }
        if(conicType == 4)
        {
            c = 0;
            d = 0;
            if(poScript.orientation)
            {
                c = Mathf.Sqrt((a*a) + (b*b));
                vertexPoint.transform.position = new Vector3(h+a,k, 0f);
                vertexPoint2.transform.position = new Vector3(h-a,k, 0f);
            }
            else
            {
                d = Mathf.Sqrt((a*a) + (b*b));
                vertexPoint.transform.position = new Vector3(h,k+a, 0f);
                vertexPoint2.transform.position = new Vector3(h,k-a, 0f);
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
