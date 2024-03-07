using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStager : MonoBehaviour
{
    [SerializeField] private FollowPlayer fpScript;
    [SerializeField] private Transform transform;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            fpScript.FocusOnTransform(transform);
        }
    }

        private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            fpScript.FocusOnTransform(null);
        }
    }
}
