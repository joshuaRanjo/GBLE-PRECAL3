using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TestScript : MonoBehaviour
{
    public GameObject object1;
    private void Start() {
        PuzzleObject objectP = object1.GetComponent<PuzzleObject>();
    }
}
