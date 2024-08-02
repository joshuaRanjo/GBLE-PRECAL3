using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInGround : MonoBehaviour
{
  Coroutine waiting;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
              Debug.Log("Player Entered ground");
          } 
    }
    private void OnTriggerStay2D(Collider2D collision) {
          if(collision.gameObject.CompareTag("Ground"))
          {
            Debug.Log("Player still in ground");
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
            Debug.Log("Player left ground");
            if(waiting != null)
              StopCoroutine(waiting);
            waiting = null;
          } 
    }

    IEnumerator WaitForDuration()
    {
      yield return new WaitForSeconds(0.3f);
      Debug.Log("Ded");
      EventManager.TriggerEvent("PlayerDeath");
    }
}
