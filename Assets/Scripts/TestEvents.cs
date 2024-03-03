using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour
{

    private void OnEnable() {
        EventManager.StartListening("Testing", TestEvent);
    }

    private void OnDisable() {
        EventManager.StopListening("Testing", TestEvent);
    }

    public void TestEvent()
    {
        Debug.Log("TestEvent Triggger");
    }

    public void ExitPuzzle()
    {
        EventManager.TriggerEvent("ExitPuzzle");
    }
}
