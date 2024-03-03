using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class NPCMovement : MonoBehaviour
{
    public List<GameObject> points;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private Flowchart fc;
    private Transform currentPoint;
    private int currentPointIndex = 0;

    [SerializeField] private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = points[0].transform;
    }

/*
    public void MoveToPoint(int newPoint)
    {
        StartCoroutine(Move());
    }
*/
    public void MoveToPoint()
    {
        currentPointIndex = fc.GetIntegerVariable("CurrentPosition");
        
        StartCoroutine(Move( () => {
                if(fc.GetStringVariable("CallBackBlock") != "None")
                    fc.ExecuteBlock(fc.GetStringVariable("CallBackBlock"));
        }));
    }


    IEnumerator Move(System.Action onComplete)
    {
        // Check where is the point
        currentPoint = points[currentPointIndex].transform;
        float xDifference = currentPoint.position.x - transform.position.x ;

        while(Vector2.Distance(transform.position, currentPoint.position) > 0.5f)
        {
            
            // to the right of the NPC
            if(xDifference > 0) 
            {
                if(!facingRight)
                {
                    Flip();
                    facingRight = true;
                }
                rb.velocity = new Vector2(speed, 0);
            }
            else // to the left of the NPC
            {
                if(facingRight)
                {
                    Flip();
                    facingRight = false;
                }
                rb.velocity = new Vector2(-speed, 0);
            }
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
            yield return null;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(0f));
        onComplete?.Invoke();
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos() {
        int iteration = 0;
        foreach(GameObject point in points)
        {
           Gizmos.DrawWireSphere(point.transform.position, 0.5f);
           if(iteration > 0)
           {
            Gizmos.DrawLine(points[iteration-1].transform.position, point.transform.position);
           }
           iteration++;
        }
    }
}
