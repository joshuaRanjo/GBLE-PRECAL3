using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInteractor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject sprite;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Interactable")){
            sprite.SetActive(true);
        }
    }

        private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Interactable")){
            sprite.SetActive(false);
        }
    }
}
