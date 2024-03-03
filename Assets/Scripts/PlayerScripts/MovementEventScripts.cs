using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEventScripts : MonoBehaviour
{
    public void EnterPuzzleEvent()
    {
        EventManager.TriggerEvent("EnterPuzzle");
    }

    public void ExitPuzzleEvent()
    {
        EventManager.TriggerEvent("ExitPuzzle");
    }
}
