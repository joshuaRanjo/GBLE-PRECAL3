using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
///  Used in the PlayerSpawnPoint prefab
///  
///  When prefab is enabled, player character is moved to the objects location
/// 
/// </summary>
public class PlayerSpawnPoint : MonoBehaviour
{
    public Color gizmoColor = Color.red;
    private float gizmoRadius = 0.3f;
    private void OnEnable() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = this.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
