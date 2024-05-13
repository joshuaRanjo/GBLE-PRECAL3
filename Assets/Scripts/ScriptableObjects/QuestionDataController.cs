using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDataController : MonoBehaviour
{

    [Header("Line Data Scriptable Object")]
    [SerializeField] private LineData2 ldScriptableObject;

    [Header("Question Data Scriptable Object")]
    [SerializeField] private QuestionData2 qdScriptableObject;

    private void OnEnable() {
        EventManager.StartListening("SwitchSimplifiedEllipse", ChangeQDSimplifiedEllipse);
    }
    private void OnDisable() {
        EventManager.StopListening("SwitchSimplifiedEllipse", ChangeQDSimplifiedEllipse);
    }

    private void ChangeQDSimplifiedEllipse()
    {
        qdScriptableObject.ModifyMaxMinSimplifiedEllipse();
    }
}
