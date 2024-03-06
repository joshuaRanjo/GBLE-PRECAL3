using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpectedAnswer
{
    [Header("Expected Answer")]
    [SerializeField] public float maxA;
    [SerializeField] public float minA, maxB, minB, maxH, minH, maxK, minK;
    [SerializeField] public bool orientation; //true = horizontal , false = vertical
    [SerializeField] public int conicType; // 1 = circle , 2 = ellipse , 3 = parabola , 4 = hyperbola
    [SerializeField] public bool correct;
}
