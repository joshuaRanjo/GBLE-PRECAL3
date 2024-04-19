using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaObject : PuzzleObject
{
    public float maxLineLength = 10f;
    [SerializeField] public float xLimit;
    [SerializeField] public float yLimit;
    [Header("For Hyperbolas")]
    [Tooltip("True = spriteshape has 2 objects")]
    [SerializeField] public bool split;
    


}
