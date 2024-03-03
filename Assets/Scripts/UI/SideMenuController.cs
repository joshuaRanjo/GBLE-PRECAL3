using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class SideMenuController : MonoBehaviour
{
    [Header("Question Data Scriptable Object")]
    [SerializeField] private QuestionData qdScriptableObject;
    [Header("Line Data Scriptable Object")]
    [SerializeField] private LineData ldScriptableObject;

    [Header("Question Box")]
    [SerializeField] private TEXDraw questionText;


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

    [Header("Submit Button")]
    [SerializeField] private Button submitButton;
    [Header("Back Button")]
    [SerializeField] private Button backButton;

    private void Start() {
        InputStartListening();
    }

    private void OnEnable() {
        qdScriptableObject.questionUpdateEvent.AddListener(UpdateUI);

        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
    }

    private void OnDisable() {
        qdScriptableObject.questionUpdateEvent.RemoveListener(UpdateUI);

        EventManager.StopListening("EnterPuzzle", EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);
    }

    private void UpdateUI()
    {
        //questionText.text = qdScriptableObject.prompt;

        inputA.interactable = qdScriptableObject.allowA;
        inputH.interactable = qdScriptableObject.allowH;
        inputK.interactable = qdScriptableObject.allowK;
        inputB.interactable = qdScriptableObject.allowB;

        circleButton.interactable    = qdScriptableObject.allowCircle;
        parabolaButton.interactable  = qdScriptableObject.allowParabola;
        ellipseButton.interactable   = qdScriptableObject.allowEllipse;
        hyperbolaButton.interactable = qdScriptableObject.allowHyperbola;

        InputStopListening();
        inputA.text = ldScriptableObject.a.ToString();
        inputH.text = ldScriptableObject.h.ToString();
        inputK.text = ldScriptableObject.k.ToString();
        if(ldScriptableObject.b == -99f)
        {
            inputB.text = "";
        }
        else
        {
            inputB.text = ldScriptableObject.b.ToString();
        }
        InputStartListening();
    }

    private void DisableAll()
    {
        inputA.interactable = false;
        inputH.interactable = false;
        inputK.interactable = false;
        inputB.interactable = false;

        circleButton.interactable    = false;
        parabolaButton.interactable  = false;
        ellipseButton.interactable   = false;
        hyperbolaButton.interactable = false;
    }

    private void EnterPuzzle()
    {
        backButton.interactable = true;
        backButton.onClick.AddListener( () => {EventManager.TriggerEvent("ExitPuzzle");});
    }

    private void ExitPuzzle()
    {
        DisableAll();
        ClearInput();

        backButton.interactable = false;
        backButton.onClick.RemoveListener( () => {EventManager.TriggerEvent("ExitPuzzle");});
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
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputA.text = "1";
            ldScriptableObject.SetA(1f, "input");
        }
        InputStartListening();
    }

    private void UpdateBOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputB.text = "1";
            ldScriptableObject.SetB(1f, "input");
        }
        InputStartListening();
    }

    private void UpdateHOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputH.text = "0";
            ldScriptableObject.SetH(0f, "input");
            
        }
        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue >= 10f || floatValue <= -10f )
            {
                inputH.text = "0";
                ldScriptableObject.SetH(0f, "input");
            }
        }
        InputStartListening();
    }

    private void UpdateKOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputK.text = "0";
            ldScriptableObject.SetK(0f, "input");
        }
        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue >= 10f || floatValue <= -10f )
            {
                inputK.text = "0";
                ldScriptableObject.SetK(0f, "input");
            }
        }
        InputStartListening();
    }
    private void UpdateA(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {         
            ldScriptableObject.SetA(floatValue, "input");
        }
        
    }
    private void UpdateB(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            ldScriptableObject.SetB(floatValue, "input");
        }
    }
    private void UpdateH(string newValue)
    {

        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue <= 10f && floatValue >= -10f )
                ldScriptableObject.SetH(floatValue, "input");
        }
        
    }
    private void UpdateK(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            if(floatValue <= 10f && floatValue >= -10f )
                ldScriptableObject.SetK(floatValue, "input");
        }
    }

    private void ClearInput()
    {
        InputStopListening();
        inputA.text = "";
        inputB.text = "";
        inputH.text = "";
        inputK.text = "";
        InputStartListening();
    }
}
