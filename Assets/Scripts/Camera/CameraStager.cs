using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraStager : MonoBehaviour
{

    public int id = 5;
    [SerializeField] private FollowPlayer fpScript;
    [SerializeField] private Transform cameratransform;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("Stager id" + id);
            fpScript.FocusOnTransform(cameratransform);
        }
    }

        private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
             Debug.Log("Stager id" + id);
            fpScript.FocusOnTransform(null);
        }
    }
}
