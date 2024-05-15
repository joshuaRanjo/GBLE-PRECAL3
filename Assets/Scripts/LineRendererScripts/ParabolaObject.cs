using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaObject : PuzzleObject
{
    public float maxLineLength = 10f;
    [SerializeField] public float xLimit = 5;
    [SerializeField] public float yLimit = 5;
    [Header("For Hyperbolas")]
    [Tooltip("True = spriteshape has 2 objects")]
    [SerializeField] public bool split;
    


}
