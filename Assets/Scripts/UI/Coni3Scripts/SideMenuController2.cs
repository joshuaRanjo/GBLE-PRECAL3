using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class SideMenuController2 : MonoBehaviour
{
    [Header("Question Data Scriptable Object")]
    [SerializeField] private QuestionData qdScriptableObject;
    [Header("Line Data Scriptable Object")]
    [SerializeField] private LineData ldScriptableObject;

    [Header("Question Box")]
    [SerializeField] private TEXDraw questionText;


    [Header("Inputs")]
    [SerializeField] private TMP_InputField inputValue;

    [Header("Input Selectors")]
    [SerializeField] private Button aButton;
    [SerializeField] private Button bButton;
    [SerializeField] private Button hButton;
    [SerializeField] private Button kButton;
    [Header("Min and Max")]
    [SerializeField] private TMP_Text minValueText;
    [SerializeField] private TMP_Text maxValueText;
    [Header("Slider")]
    [SerializeField] private Slider valueSlider;


    [Header("Conic Selectors")]
    [SerializeField] private Button circleButton;
    [SerializeField] private Button parabolaButton;
    [SerializeField] private Button ellipseButton;
    [SerializeField] private Button hyperbolaButton;

    [Header("Submit Button")]
    [SerializeField] private Button submitButton;
    [Header("Back Button")]
    [SerializeField] private Button backButton;

    private int selectedVariable = 0;
    private Color aColor = new Color(109f / 255f, 55f / 255f, 164f / 255f);
    private Color bColor = new Color(198f / 255f, 74f / 255f, 29f / 255f);
    private Color hColor = new Color(30f / 255f, 204f / 255f, 43f / 255f);
    private Color kColor = new Color(0f, 161f / 255f, 255f / 255f);

    private void Start() {
        InputStartListening();

        //Button Listeners
        aButton.onClick.AddListener( () => SelectVariable(1));
        bButton.onClick.AddListener( () => SelectVariable(2));
        hButton.onClick.AddListener( () => SelectVariable(3));
        kButton.onClick.AddListener( () => SelectVariable(4));
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

        aButton.interactable = qdScriptableObject.allowA;
        hButton.interactable = qdScriptableObject.allowH;
        kButton.interactable = qdScriptableObject.allowK;
        bButton.interactable = qdScriptableObject.allowB;

        circleButton.interactable    = qdScriptableObject.allowCircle;
        parabolaButton.interactable  = qdScriptableObject.allowParabola;
        ellipseButton.interactable   = qdScriptableObject.allowEllipse;
        hyperbolaButton.interactable = qdScriptableObject.allowHyperbola;

        

    }

    private void DisableAll()
    {
        aButton.interactable = false;
        bButton.interactable = false;
        kButton.interactable = false;
        hButton.interactable = false;

        inputValue.interactable = false;

        circleButton.interactable    = false;
        parabolaButton.interactable  = false;
        ellipseButton.interactable   = false;
        hyperbolaButton.interactable = false;

        valueSlider.interactable = false;
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
        DeselectAllVarButtons();

        maxValueText.text = ">";
        minValueText.text = "<";
        valueSlider.minValue = 0f;
        valueSlider.maxValue = 1f;

        InputStopListening();
        valueSlider.value = 0.5f;
        InputStartListening();

        backButton.interactable = false;
        backButton.onClick.RemoveListener( () => {EventManager.TriggerEvent("ExitPuzzle");});
    }

    private void InputStartListening()
    {
        
        inputValue.onValueChanged.AddListener(UpdateValue);
        inputValue.onEndEdit.AddListener(UpdateValueOnEndEdit);
        valueSlider.onValueChanged.AddListener(SliderValueChanged);

    }

    private void InputStopListening()
    {

        inputValue.onValueChanged.RemoveListener(UpdateValue);
        inputValue.onEndEdit.RemoveListener(UpdateValueOnEndEdit);
        valueSlider.onValueChanged.RemoveListener(SliderValueChanged);

    }
    private void SelectVariable(int chosenVar)
    {
        selectedVariable = chosenVar;
        float currentValue = 0f;

        DeselectAllVarButtons();

        if(selectedVariable == 1)
        {
            currentValue = ldScriptableObject.a;
            valueSlider.minValue = qdScriptableObject.minA;
            valueSlider.maxValue = qdScriptableObject.maxA;
            minValueText.text = qdScriptableObject.minA.ToString();
            maxValueText.text = qdScriptableObject.maxA.ToString();

            aButton.GetComponentInChildren<TEXDraw>().color = Color.white;
            aButton.GetComponent<Image>().color = aColor;
            
        }
        else if(selectedVariable == 2)
        {
            currentValue = ldScriptableObject.b;
            valueSlider.minValue = qdScriptableObject.minB;
            valueSlider.maxValue = qdScriptableObject.maxB;
            minValueText.text = qdScriptableObject.minB.ToString();
            maxValueText.text = qdScriptableObject.maxB.ToString();

            bButton.GetComponentInChildren<TEXDraw>().color = Color.white;
            bButton.GetComponent<Image>().color = bColor;
        }
        else if(selectedVariable == 3)
        {
            currentValue = ldScriptableObject.h;
            valueSlider.minValue = qdScriptableObject.minH;
            valueSlider.maxValue = qdScriptableObject.maxH;
            minValueText.text = qdScriptableObject.minH.ToString();
            maxValueText.text = qdScriptableObject.maxH.ToString();

            hButton.GetComponentInChildren<TEXDraw>().color = Color.white;
            hButton.GetComponent<Image>().color = hColor;
        }
        else if(selectedVariable == 4)
        {
            currentValue = ldScriptableObject.k;
            valueSlider.minValue = qdScriptableObject.minK;
            valueSlider.maxValue = qdScriptableObject.maxK;
            minValueText.text = qdScriptableObject.minK.ToString();
            maxValueText.text = qdScriptableObject.maxK.ToString();

            kButton.GetComponentInChildren<TEXDraw>().color = Color.white;
            kButton.GetComponent<Image>().color = kColor;
        }

        InputStopListening();
        inputValue.text = currentValue.ToString();
        valueSlider.value = currentValue;
        InputStartListening();

        inputValue.interactable = true;
        valueSlider.interactable = true;
        
    }

    private void UpdateValue(string newValue)
    {
        InputStopListening();
        if(float.TryParse(newValue,out float floatValue)) // if newValue is a valid float number
        {
            if(selectedVariable == 1)
            {
                // Check if valid
                if((qdScriptableObject.maxA != -99) && (qdScriptableObject.maxA >= floatValue || qdScriptableObject.minA <= floatValue))
                {
                    ldScriptableObject.SetA(floatValue, "input");
                    valueSlider.value = floatValue;
                }
            }
            else if(selectedVariable == 2)
            {
                if((qdScriptableObject.maxB != -99) && (qdScriptableObject.maxB >= floatValue || qdScriptableObject.minB <= floatValue))
                {
                    ldScriptableObject.SetB(floatValue, "input");
                    valueSlider.value = floatValue;
                }
            }
            else if(selectedVariable == 3)
            {
                if((qdScriptableObject.maxH != -99) && (qdScriptableObject.maxH >= floatValue || qdScriptableObject.minH <= floatValue))
                {
                    ldScriptableObject.SetH(floatValue, "input");
                    valueSlider.value = floatValue;
                }
            }
            else if(selectedVariable == 4)
            {
                if((qdScriptableObject.maxK != -99) && (qdScriptableObject.maxK >= floatValue || qdScriptableObject.minK <= floatValue))
                {    
                    ldScriptableObject.SetK(floatValue, "input");
                    valueSlider.value = floatValue;
                }
            }
        }
        InputStartListening();
    }


    private void UpdateValueOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _)) // if left empty
        {
            if(selectedVariable == 1)
            {
                inputValue.text = qdScriptableObject.defaultA.ToString();
                ldScriptableObject.SetA(qdScriptableObject.defaultA, "input");
                valueSlider.value = qdScriptableObject.defaultA;
            }
            else if(selectedVariable == 2)
            {
                inputValue.text = qdScriptableObject.defaultB.ToString();
                ldScriptableObject.SetB(qdScriptableObject.defaultB, "input");
                valueSlider.value = qdScriptableObject.defaultB;
            }
            else if(selectedVariable == 3)
            {
                inputValue.text = qdScriptableObject.defaultH.ToString();
                ldScriptableObject.SetH(qdScriptableObject.defaultH, "input");
                valueSlider.value = qdScriptableObject.defaultH;
            }
            else if(selectedVariable == 4)
            {
                inputValue.text = qdScriptableObject.defaultK.ToString();
                ldScriptableObject.SetK(qdScriptableObject.defaultK, "input");
                valueSlider.value = qdScriptableObject.defaultK;
            }
        }
        else if(float.TryParse(newValue, out float floatValue)) // Check when done editing, if not valid set to default values
        {
            if(selectedVariable == 1)
            {
                // Check if valid
                if((qdScriptableObject.maxA != -99) && (qdScriptableObject.maxA < floatValue || qdScriptableObject.minA > floatValue))
                {
                    inputValue.text = qdScriptableObject.defaultA.ToString();
                    ldScriptableObject.SetA(qdScriptableObject.defaultA, "input");
                    valueSlider.value = qdScriptableObject.defaultA;
                }
                    
            }
            else if(selectedVariable == 2)
            {
                if((qdScriptableObject.maxB != -99) && (qdScriptableObject.maxB >= floatValue || qdScriptableObject.minB <= floatValue))
                {
                    inputValue.text = qdScriptableObject.defaultB.ToString();
                    ldScriptableObject.SetB(qdScriptableObject.defaultB, "input");
                    valueSlider.value = qdScriptableObject.defaultB;
                }
                    
            }
            else if(selectedVariable == 3)
            {
                if((qdScriptableObject.maxH != -99) && (qdScriptableObject.maxH < floatValue || qdScriptableObject.minH > floatValue))
                {
                    inputValue.text = qdScriptableObject.defaultH.ToString();
                    ldScriptableObject.SetH(qdScriptableObject.defaultH, "input");
                    valueSlider.value = qdScriptableObject.defaultH;
                }
            }
            else if(selectedVariable == 4)
            {
                if((qdScriptableObject.maxK != -99) && (qdScriptableObject.maxK < floatValue || qdScriptableObject.minK > floatValue))
                {
                    inputValue.text = qdScriptableObject.defaultK.ToString();
                    ldScriptableObject.SetK(qdScriptableObject.defaultK, "input");
                    valueSlider.value = qdScriptableObject.defaultK;
                }
            }
        }
        InputStartListening();
    }

    private void SliderValueChanged(float sliderValue)
    {
        UpdateValue(sliderValue.ToString("F1"));
        InputStopListening();
        inputValue.text = sliderValue.ToString("F1");
        InputStartListening();
    }  
    private void ClearInput()
    {
        InputStopListening();
        inputValue.text = "";
        selectedVariable = 0;
        InputStartListening();
    }

    private void DeselectAllVarButtons()
    {
        aButton.GetComponentInChildren<TEXDraw>().color = aColor;
        aButton.GetComponent<Image>().color = Color.white;

        bButton.GetComponentInChildren<TEXDraw>().color = bColor;
        bButton.GetComponent<Image>().color = Color.white;

        hButton.GetComponentInChildren<TEXDraw>().color = hColor;
        hButton.GetComponent<Image>().color = Color.white;

        kButton.GetComponentInChildren<TEXDraw>().color = kColor;
        kButton.GetComponent<Image>().color = Color.white;
    }

    private Color ConvertHexToColor(string hex)
    {
        Color textColor;
        if(ColorUtility.TryParseHtmlString(hex, out textColor))
        {
            return textColor;
        }
        else{
            return Color.white;
        }
    }
    
}
