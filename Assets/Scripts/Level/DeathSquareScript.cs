using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// Triggers all objects listening to "PlayerDeath"
/// also shows death message using the Fungus library
/// </summary>
public class DeathSquareScript : MonoBehaviour
{

    public Fungus.Flowchart fc;
    private void OnEnable() {
       EventManager.StartListening("PlayerDeath", Death);
    }
    private void OnDisable() {
       EventManager.StopListening("PlayerDeath", Death);
    }

    private void Death()
    {
        fc.ExecuteBlock("In Wall");
    }
}
