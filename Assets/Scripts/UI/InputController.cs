using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{

    [Header("Vertex Form Inputs")]

    [SerializeField] private TMP_InputField inputA;
    [SerializeField] private TMP_InputField inputB;
    [SerializeField] private TMP_InputField inputH;
    [SerializeField] private TMP_InputField inputK;

    [Header("Conic Selectors")]
    [SerializeField] private Button circleButton;
    [SerializeField] private Button parabolaButton;
    [SerializeField] private Button ellipseButton;
    [SerializeField] private Button hyperbolaButton;

    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;
    [Header("Question Data")]
    [SerializeField] private QuestionData qdScriptableObject;
    private void Start() {
        InputStartListening();
    }

    private void InputStartListening()
    {
        inputA.onValueChanged.AddListener(UpdateA);
        inputB.onValueChanged.AddListener(UpdateB);
        inputH.onValueChanged.AddListener(UpdateH);
        inputK.onValueChanged.AddListener(UpdateK);

        inputA.onEndEdit.AddListener(UpdateAOnEndEdit);
        inputB.onEndEdit.AddListener(UpdateBOnEndEdit);
        inputH.onEndEdit.AddListener(UpdateHOnEndEdit);
        inputK.onEndEdit.AddListener(UpdateKOnEndEdit);
    }

    private void InputStopListening()
    {
        inputA.onValueChanged.RemoveListener(UpdateA);
        inputB.onValueChanged.RemoveListener(UpdateB);
        inputH.onValueChanged.RemoveListener(UpdateH);
        inputK.onValueChanged.RemoveListener(UpdateK);

        inputA.onEndEdit.RemoveListener(UpdateAOnEndEdit);
        inputB.onEndEdit.RemoveListener(UpdateBOnEndEdit);
        inputH.onEndEdit.RemoveListener(UpdateHOnEndEdit);
        inputK.onEndEdit.RemoveListener(UpdateKOnEndEdit);
    }

    private void UpdateAOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputA.text = "1";
            lineDataScriptableObject.SetA(1f, "input");
        }
        
    }

    private void UpdateBOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputB.text = "1";
            lineDataScriptableObject.SetB(1f, "input");
        }
    }

    private void UpdateHOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputH.text = "0";
            lineDataScriptableObject.SetH(0f, "input");
        }
        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue >= 10f || floatValue <= -10f )
            inputH.text = "0";
            lineDataScriptableObject.SetH(0f, "input");
        }
    }

    private void UpdateKOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputK.text = "0";
            lineDataScriptableObject.SetK(0f, "input");
        }
        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue >= 10f || floatValue <= -10f )
            inputH.text = "0";
            lineDataScriptableObject.SetH(0f, "input");
        }
    }
    private void UpdateA(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {         
            lineDataScriptableObject.SetA(floatValue, "input");
        }
        
    }
    private void UpdateB(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            lineDataScriptableObject.SetB(floatValue, "input");
        }
    }
    private void UpdateH(string newValue)
    {

        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue <= 10f && floatValue >= -10f )
                lineDataScriptableObject.SetH(floatValue, "input");
        }
        
    }
    private void UpdateK(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue <= 10f && floatValue >= -10f )
                lineDataScriptableObject.SetK(floatValue, "input");
        }
    }

    private void SetVariablesToDefault()
    {
        InputStopListening();
        inputA.text = "";
        inputB.text = "";
        inputH.text = "";
        inputK.text = "";
        InputStartListening();
    }
}
