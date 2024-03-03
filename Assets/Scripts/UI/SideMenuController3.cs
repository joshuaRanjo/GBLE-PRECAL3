using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class SideMenuController3 : MonoBehaviour
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

    [Header("Sliders")]
    [SerializeField] private Slider aSlider;
    [SerializeField] private Slider bSlider;
    [SerializeField] private Slider hSlider;
    [SerializeField] private Slider kSlider;



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

        // Set Interactability
        inputA.interactable = qdScriptableObject.allowA;
        inputH.interactable = qdScriptableObject.allowH;
        inputK.interactable = qdScriptableObject.allowK;
        inputB.interactable = qdScriptableObject.allowB;

        aSlider.interactable = qdScriptableObject.allowA;
        bSlider.interactable = qdScriptableObject.allowB;
        hSlider.interactable = qdScriptableObject.allowH;
        kSlider.interactable = qdScriptableObject.allowK;

        circleButton.interactable    = qdScriptableObject.allowCircle;
        parabolaButton.interactable  = qdScriptableObject.allowParabola;
        ellipseButton.interactable   = qdScriptableObject.allowEllipse;
        hyperbolaButton.interactable = qdScriptableObject.allowHyperbola;

        //Set values of inputs and sliders
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

        aSlider.interactable = false;
        bSlider.interactable = false;
        hSlider.interactable = false;
        kSlider.interactable = false;
    }

    private void EnterPuzzle()
    {
        backButton.interactable = true;
        backButton.onClick.AddListener( () => {EventManager.TriggerEvent("ExitPuzzle");});

        AddSliderLimits();
        UpdateUI();
        
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

        aSlider.onValueChanged.AddListener(SliderAChange);
        bSlider.onValueChanged.AddListener(SliderBChange);
        hSlider.onValueChanged.AddListener(SliderHChange);
        kSlider.onValueChanged.AddListener(SliderKChange);
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

        aSlider.onValueChanged.RemoveListener(SliderAChange);
        bSlider.onValueChanged.RemoveListener(SliderBChange);
        hSlider.onValueChanged.RemoveListener(SliderHChange);
        kSlider.onValueChanged.RemoveListener(SliderKChange);
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

        aSlider.value = 0.5f;
        bSlider.value = 0.5f;
        hSlider.value = 0.5f;
        kSlider.value = 0.5f;
        InputStartListening();

        aSlider.minValue = 0;
        aSlider.maxValue = 1f;
        bSlider.minValue = 0;
        bSlider.maxValue = 1f;
        hSlider.minValue = 0;
        hSlider.maxValue = 1f;
        kSlider.minValue = 0;
        kSlider.maxValue = 1f;
    }

    private void AddSliderLimits()
    {
        if(qdScriptableObject.minA != -99)
        {
            aSlider.minValue = qdScriptableObject.minA;
            aSlider.maxValue = qdScriptableObject.maxA;
        }
        if(qdScriptableObject.minB != -99)
        {
            bSlider.minValue = qdScriptableObject.minB;
            bSlider.maxValue = qdScriptableObject.maxB;
        }
        if(qdScriptableObject.minH != -99)
        {
            hSlider.minValue = qdScriptableObject.minH;
            hSlider.maxValue = qdScriptableObject.maxH;
        }
        if(qdScriptableObject.minK != -99)
        {
            kSlider.minValue = qdScriptableObject.minK;
            kSlider.maxValue = qdScriptableObject.maxK;
        }
    }

    private void SliderAChange(float newValue)
    {
        InputStopListening();
        inputA.text = newValue.ToString("F1");
        ldScriptableObject.SetA(Mathf.Round(newValue*10f)/10f);
        InputStartListening();
    }
    private void SliderBChange(float newValue)
    {
        InputStopListening();
        inputB.text = newValue.ToString("F1");
        ldScriptableObject.SetB(Mathf.Round(newValue*10f)/10f);
        InputStartListening();
    }
    private void SliderHChange(float newValue)
    {
        InputStopListening();
        inputH.text = newValue.ToString("F1");
        ldScriptableObject.SetH(Mathf.Round(newValue*10f)/10f);
        InputStartListening();
    }
    private void SliderKChange(float newValue)
    {
        InputStopListening();
        inputK.text = newValue.ToString("F1");
        ldScriptableObject.SetK(Mathf.Round(newValue*10f)/10f);
        InputStartListening();
    }

}
