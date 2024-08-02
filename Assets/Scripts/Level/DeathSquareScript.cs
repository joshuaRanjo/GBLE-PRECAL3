using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
