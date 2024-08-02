using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject gridObject;
    private void OnEnable() {
        EventManager.StartListening("ExitPuzzle",HideGrid);
        EventManager.StartListening("EnterPuzzle",ShowGrid);
    }
    private void OnDisable() {
        EventManager.StopListening("ExitPuzzle",HideGrid);
        EventManager.StopListening("EnterPuzzle",ShowGrid);
    }

    public void ShowGrid()
    {
        gridObject.SetActive(true);
    }

    public void HideGrid()
    {
        gridObject.SetActive(false);
    }
}
