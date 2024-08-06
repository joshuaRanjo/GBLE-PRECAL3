using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// Player character is moved to this objects location on game startup or when
/// entering the main menu
/// 
/// </summary>
public class MainSpawn : MonoBehaviour
{
    public Color gizmoColor = Color.red;
    private float gizmoRadius = 0.3f;
    private GameObject player;

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player");

       EventManager.StartListening("EnterMainMenu", Teleport);
       EventManager.StartListening("PlayerDeath", Teleport);
    }
    private void OnDisable() {
        EventManager.StopListening("EnterMainMenu", Teleport);
        EventManager.StopListening("PlayerDeath", Teleport);
    }
    
    private void Teleport() {
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
