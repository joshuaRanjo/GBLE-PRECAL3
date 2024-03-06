using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteBlockOnExitPuzzle : MonoBehaviour
{
    // Checks expected answer and calls a Flowchart block
    [SerializeField] private PuzzleScript puzzleScript;
    [SerializeField] private LineData ld;
    [SerializeField] private FlowchartController fcc;
    [SerializeField] private string blockToRun;
 
    private string  puzzleID;

    private void OnEnable() {
        EventManager.StartListening("ExitPuzzle",ExitPuzzle);
    }

    private void OnDisable() {
         EventManager.StopListening("ExitPuzzle",ExitPuzzle);
    }
    // Start is called before the first frame update
    void Awake()
    {
        puzzleID = puzzleScript.puzzleID;
    }

    private void ExitPuzzle()
    {
        if(ld.puzzleID == puzzleID)
        {
            fcc.RunBlock(blockToRun);
        }
    }


}
