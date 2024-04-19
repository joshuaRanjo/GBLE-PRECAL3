using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCollider : MonoBehaviour
{

    public LevelManagerSO levelManagerSO;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");  
    }
}
