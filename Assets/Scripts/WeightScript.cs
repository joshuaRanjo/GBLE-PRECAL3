using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeightScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private bool checkForGround = true;
    private Transform parent;
    private Vector3 savedPosition;
    private bool inGround = false;

    private Coroutine resetCoroutine;

    private void Start() {
        parent = transform.parent;
    }

    private void OnEnable() {
        savedPosition = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("PressurePlate"))
       {
          Debug.Log("ChangingParent to pressureplate");
          //transform.SetParent(collision.transform.parent);
          //transform.position = transform.position;
       }



    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
          if(collision.gameObject.CompareTag("PressurePlate"))
          {
               //Debug.Log("ChangingParent to pressureplate");
               //transform.SetParent(collision.transform.parent);
               //transform.parent = collision.transform.parent;
               //transform.position = transform.position;
          }



    }

    private void OnCollisionExit2D(Collision2D collision)   
    {
        if(collision.gameObject.CompareTag("PressurePlate"))
       {
          //Debug.Log("ChangingParent to original parent");
          //transform.SetParent(parent,true);  
       }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
              //Object Entered ground
              //Debug.Log("Object Entered ground");
          } 
    }

    private void OnTriggerStay2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Ground") && checkForGround)
          { inGround = true;
            Debug.Log("ground inside");
               //Object is still in ground
               if(resetCoroutine == null)
                resetCoroutine = StartCoroutine(ResetObject());
              
              // Debug.Log("Object still in ground");
          } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
               //Object left ground
               inGround = false;
               if(resetCoroutine != null)
               {
                    StopCoroutine(resetCoroutine);
                    resetCoroutine = null;
               }
                    

               //Debug.Log("Object left ground");
          } 
    }

    IEnumerator ResetObject()
    {
          yield return new WaitForSeconds(3f);
       
        // Check if the condition is still true
        if (inGround)
        {
             transform.position = savedPosition;
            resetCoroutine = null; 
        }

    }

}
