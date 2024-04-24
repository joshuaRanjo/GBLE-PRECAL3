using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawn : MonoBehaviour
{
    public Color gizmoColor = Color.red;
    private float gizmoRadius = 0.3f;

    private void OnEnable() {
       EventManager.StartListening("EnterMainMenu", Teleport);
    }
    private void OnDisable() {
        EventManager.StopListening("EnterMainMenu", Teleport);
    }
    private void Teleport() {
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
