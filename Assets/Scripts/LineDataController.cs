using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDataController : MonoBehaviour
{
    [Header("Line Data Scriptable Object")]
    [SerializeField] private LineData2 ldScriptableObject;

    private Vector3 previousPosition;

    private bool inPuzzle = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inPuzzle)
        {
            if(previousPosition != ldScriptableObject.puzzleObject.transform.localPosition)
            {
                ldScriptableObject.SetH(Mathf.Round(ldScriptableObject.puzzleObject.transform.localPosition.x *100) / 100);
                ldScriptableObject.SetK(Mathf.Round(ldScriptableObject.puzzleObject.transform.localPosition.y *100) / 100);
            }
        }
    }

    private void OnEnable() 
    {

        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
    }

    private void OnDisable() 
    {
        EventManager.StopListening("EnterPuzzle", EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);
    }

    private void EnterPuzzle()
    {
        inPuzzle = true;
        previousPosition = ldScriptableObject.puzzleObject.transform.localPosition;
    }

    private void ExitPuzzle()
    {
        inPuzzle = false;
    }
}
