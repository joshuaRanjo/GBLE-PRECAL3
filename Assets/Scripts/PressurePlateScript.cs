using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateScript : MonoBehaviour
{

private Vector3 originalPos = new Vector3(0,0.4f,0);
bool moveBack = false;
bool moveDown = false;
[SerializeField] private UnityEvent pressInteraction;
[SerializeField] private UnityEvent leaveInteraction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weight") || collision.transform.name == "Weight")
       {
            Debug.Log("pressing");
            pressInteraction.Invoke();
            
            moveDown = true;
            moveBack = false;

            if(collision.gameObject.CompareTag("Player"))
            {
                collision.transform.parent = transform;
            }

       }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weight") || collision.transform.name == "Weight")
       {
            
            moveDown = true;
            moveBack = false;
       }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weight") || collision.transform.name == "Weight")
       {
            leaveInteraction.Invoke();
           
           moveDown = false;
           moveBack = true;
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.transform.parent = null;
            }
       }
    }

    private void Update() {
        
        if(moveDown)
        {
            if(transform.localPosition.y > 0.285f)
            {
                transform.Translate(0,-0.01f,0);
            }
            else{
                moveDown = false;
            }
        }
        if(moveBack)
        {
            if(transform.localPosition.y < originalPos.y)
            {
                transform.Translate(0,0.01f,0);
            }
            else{
                moveBack = false;
            }
        }
    }

    private void Start() {
        transform.parent.parent = GameObject.Find("LevelProps").transform;
    }



}
