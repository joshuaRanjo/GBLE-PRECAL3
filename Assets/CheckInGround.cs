using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInGround : MonoBehaviour
{
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
          } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
          if(collision.gameObject.CompareTag("Ground"))
          {
            Debug.Log("Player left ground");
          } 
    }
}
