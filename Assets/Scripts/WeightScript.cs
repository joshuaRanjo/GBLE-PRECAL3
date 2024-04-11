using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeightScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform parent;
    private bool inGround = false;

    private Coroutine resetCoroutine;

    private void Start() {
        parent = transform.parent;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("PressurePlate"))
       {
           
            transform.parent = collision.transform.parent;
       }



    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
          if(collision.gameObject.CompareTag("PressurePlate"))
          {
               transform.parent = collision.transform.parent;
          }



    }

    private void OnCollisionExit2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("PressurePlate"))
       {
            transform.parent = parent;
       }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
              //Object Entered ground
          } 
    }

    private void OnTriggerStay2D(Collider2D collision) {
               if(collision.gameObject.CompareTag("Ground"))
          {
               //Object is still in ground
               resetCoroutine = StartCoroutine(ResetObject());
               inGround = true;
          } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
               //Object left ground
               inGround = false;
               if(resetCoroutine != null)
                    StopCoroutine(resetCoroutine);
          } 
    }

    IEnumerator ResetObject()
    {
          yield return new WaitForSeconds(0.2f);

        // Check if the condition is still true
        if (inGround)
        {
           //Reseting object
            
        }

    }

}
