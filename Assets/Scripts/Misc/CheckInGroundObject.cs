using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInGroundObject : MonoBehaviour
{
  Coroutine waiting;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
              
          } 
    }
    private void OnTriggerStay2D(Collider2D collision) {
          if(collision.gameObject.CompareTag("Ground"))
          {
            
            if(waiting == null)
            {
              Debug.Log("StartingCoroutine");
              waiting = StartCoroutine(WaitForDuration());
            }
              
          } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
            
            if(waiting != null)
              StopCoroutine(waiting);
            waiting = null;
          } 
    }

    IEnumerator WaitForDuration()
    {
      yield return new WaitForSeconds(0.3f);
      ResetObject();
      
    }

    private void ResetObject()
    {
        Debug.Log("Object Resetting");
    }
}
