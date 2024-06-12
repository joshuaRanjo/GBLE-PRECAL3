using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class SideMenuController3 : MonoBehaviour
{
    //This script manages the content and behaviours of the input fields and sliders

    [Header("Question Data Scriptable Object")]
    [SerializeField] private QuestionData2 qdScriptableObject;
    [Header("Line Data Scriptable Object")]
    [SerializeField] private LineData2 ldScriptableObject;
    [Header("Player Data Scriptable Object")]
    [SerializeField] private PlayerData pDScriptableObject;


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
    

    [Header("Confirm Button")]
    [SerializeField] private Button confirmButton;
    [Header("Back Button")]
    [SerializeField] private Button backButton;

    private bool inPuzzle = false;
    private bool precisionMode = false;

    private float simplifiedEllipseModifier = 1f;

    private void Start() {
        InputStartListening();
        EventManager.TriggerEvent("EnterMainMenu");
    }

    private void LateUpdate() {
        
    }

    private void OnEnable() {
        qdScriptableObject.questionUpdateEvent.AddListener(UpdateUI);
        ldScriptableObject.simplifiedEquationChange.AddListener(UpdateUI);
        ldScriptableObject.dataChangeEventA.AddListener(SetA);
        ldScriptableObject.dataChangeEventB.AddListener(SetB);
        ldScriptableObject.dataChangeEventH.AddListener(SetH);
        ldScriptableObject.dataChangeEventK.AddListener(SetK);

        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
        
    }

    private void OnDisable() {
        qdScriptableObject.questionUpdateEvent.RemoveListener(UpdateUI);
        ldScriptableObject.simplifiedEquationChange.RemoveListener(UpdateUI);
        ldScriptableObject.dataChangeEventA.RemoveListener(SetA);
        ldScriptableObject.dataChangeEventB.RemoveListener(SetB);
        ldScriptableObject.dataChangeEventH.RemoveListener(SetH);
        ldScriptableObject.dataChangeEventK.RemoveListener(SetK);

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
        AddSliderLimits();
        inputA.text = ldScriptableObject.a.ToString();
        inputH.text = ldScriptableObject.h.ToString();
        inputK.text = ldScriptableObject.k.ToString();
        aSlider.value = ldScriptableObject.a;
        hSlider.value = ldScriptableObject.h;
        kSlider.value = ldScriptableObject.k;
        if(ldScriptableObject.b == -99f)
        {
            inputB.text = "";
        }
        else
        {
            bSlider.value = ldScriptableObject.b;
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
        UpdateUI();


        
        inPuzzle = true;
        
    }

    private void ExitPuzzle()
    {
        if(inPuzzle)
        {
            DisableAll();
            ClearInput();

            backButton.interactable = false;
            backButton.onClick.RemoveListener( () => {EventManager.TriggerEvent("ExitPuzzle");});

            inPuzzle = false;
        }
    }

    private void InputStartListening()
    {

        inputA.onEndEdit.AddListener(UpdateAOnEndEdit);
        inputB.onEndEdit.AddListener(UpdateBOnEndEdit);
        inputH.onEndEdit.AddListener(UpdateHOnEndEdit);
        inputK.onEndEdit.AddListener(UpdateKOnEndEdit);


            inputA.onValueChanged.AddListener(UpdateA);
            inputB.onValueChanged.AddListener(UpdateB);
            inputH.onValueChanged.AddListener(UpdateH);
            inputK.onValueChanged.AddListener(UpdateK);

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
            inputA.text = qdScriptableObject.defaultA.ToString();
            //if(!pDScriptableObject.precisionMode)
                ldScriptableObject.SetA(qdScriptableObject.defaultA, "input");
            aSlider.value = qdScriptableObject.defaultA;
        }
        else if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxA != -99) 
                && (
                    (qdScriptableObject.maxA < floatValue || qdScriptableObject.minA > floatValue)
                //|| ((ldScriptableObject.conicType != 3) == (floatValue == 0f))
                    )
              )
            {
                {
                    inputA.text = qdScriptableObject.defaultA.ToString();
                    //if(!pDScriptableObject.precisionMode)
                        ldScriptableObject.SetA(qdScriptableObject.defaultA, "input");
                    aSlider.value = qdScriptableObject.defaultA;
                }
                
            }
                       
        }

        InputStartListening();
    }

    private void UpdateBOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputB.text = qdScriptableObject.defaultB.ToString();
            //if(!pDScriptableObject.precisionMode)
                ldScriptableObject.SetB(qdScriptableObject.defaultB, "input");
            bSlider.value = qdScriptableObject.defaultB;
        }
        else if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxB != -99) 
                && (qdScriptableObject.maxB < floatValue || qdScriptableObject.minB > floatValue)
              )
            {
                inputB.text = qdScriptableObject.defaultB.ToString();
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetB(qdScriptableObject.defaultB, "input");
                bSlider.value = qdScriptableObject.defaultB;
            }
            if(floatValue == 0f)
            {
                inputB.text = qdScriptableObject.defaultB.ToString();
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetB(qdScriptableObject.defaultB, "input");
                bSlider.value = qdScriptableObject.defaultB;
            }
        }

        InputStartListening();
    }

    private void UpdateHOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputH.text = qdScriptableObject.defaultH.ToString();
            //if(!pDScriptableObject.precisionMode)
                ldScriptableObject.SetH(qdScriptableObject.defaultH, "input");
            hSlider.value = qdScriptableObject.defaultH;
        }
        else if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxH != -99) && (qdScriptableObject.maxH < floatValue || qdScriptableObject.minH > floatValue))
            {
                inputH.text = qdScriptableObject.defaultH.ToString();
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetH(qdScriptableObject.defaultH, "input");
                hSlider.value = qdScriptableObject.defaultH;
            }
        }

        InputStartListening();
    }

    private void UpdateKOnEndEdit(string newValue)
    {
        InputStopListening();
        if(!float.TryParse(newValue, out _))
        {
            inputK.text = qdScriptableObject.defaultK.ToString();
            //if(!pDScriptableObject.precisionMode)
                ldScriptableObject.SetK(qdScriptableObject.defaultK, "input");
            kSlider.value = qdScriptableObject.defaultK;
        }
        else if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxK != -99) && (qdScriptableObject.maxK < floatValue || qdScriptableObject.minK > floatValue))
            {
                inputK.text = qdScriptableObject.defaultK.ToString();
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetK(qdScriptableObject.defaultK, "input");
                kSlider.value = qdScriptableObject.defaultK;
                //Debug.Log("Setting to Default: Invalid");
            }
        }

        InputStartListening();
    }
    private void UpdateA(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {         

            if((qdScriptableObject.maxA != -99)  
                && (qdScriptableObject.maxA >= floatValue && qdScriptableObject.minA <= floatValue)
                && (floatValue != 0 || ((floatValue == 0f) && (ldScriptableObject.conicType == 3)))
              )
            {
                InputStopListening();
                ldScriptableObject.SetA(floatValue, "input");

                aSlider.value = floatValue;
                InputStartListening();
            }
        }
    }
    private void UpdateB(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxB != -99) 
                && (qdScriptableObject.maxB >= floatValue && qdScriptableObject.minB <= floatValue)
                && (floatValue != 0f)
              )
            {
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetB(floatValue, "input");
                /*
                else
                {
                    ldScriptableObject.SetB(floatValue, "PrecisionMode");
                }
                */
                InputStopListening();
                bSlider.value = floatValue;
                InputStartListening();
            }
        }
    }
    private void UpdateH(string newValue)
    {
        
        if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxH != -99) && (qdScriptableObject.maxH >= floatValue && qdScriptableObject.minH <= floatValue))
            {
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetH(floatValue, "input");
                InputStopListening();
                hSlider.value = floatValue;
                InputStartListening();
            }
        }
        else
        {

        }
        
        
    }
    private void UpdateK(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            if((qdScriptableObject.maxK != -99) && (qdScriptableObject.maxK >= floatValue && qdScriptableObject.minK <= floatValue))
            {    
                //if(!pDScriptableObject.precisionMode)
                    ldScriptableObject.SetK(floatValue, "input");
                InputStopListening();
                kSlider.value = floatValue;
                InputStartListening();
            }
        }
        else
        {

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
        
        
        aSlider.minValue = 0;
        aSlider.maxValue = 1f;
        bSlider.minValue = 0;
        bSlider.maxValue = 1f;
        hSlider.minValue = 0;
        hSlider.maxValue = 1f;
        kSlider.minValue = 0;
        kSlider.maxValue = 1f;
        InputStartListening();
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
        inputA.text = newValue.ToString("F2");
        if(newValue != 0 || ((newValue == 0f) && (ldScriptableObject.conicType == 3)))
        {
            ldScriptableObject.SetA(Mathf.Round(newValue*100f)/100f, "slider");
        }
            
        InputStartListening();
    }
    private void SliderBChange(float newValue)
    {
        InputStopListening();
        inputB.text = newValue.ToString("F2");
        if(newValue != 0)
            ldScriptableObject.SetB(Mathf.Round(newValue*100f)/100f, "slider");
        InputStartListening();
    }
    private void SliderHChange(float newValue)
    {
        InputStopListening();
        inputH.text = newValue.ToString("F2");
        ldScriptableObject.SetH(Mathf.Round(newValue*100f)/100f, "slider");
        InputStartListening();
    }
    private void SliderKChange(float newValue)
    {
        InputStopListening();
        inputK.text = newValue.ToString("F2");
        ldScriptableObject.SetK(Mathf.Round(newValue*100f)/100f, "slider");
        InputStartListening();
    }

    private void SetA()
    {
        inputA.onValueChanged.RemoveListener(UpdateA);
        aSlider.onValueChanged.RemoveListener(SliderAChange);

        inputA.text = ldScriptableObject.a.ToString();
        aSlider.value = ldScriptableObject.a;

        inputA.onValueChanged.AddListener(UpdateA);
        aSlider.onValueChanged.AddListener(SliderAChange);

    }

    private void SetB()
    {
        inputB.onValueChanged.RemoveListener(UpdateB);
        bSlider.onValueChanged.RemoveListener(SliderBChange);

        inputB.text = ldScriptableObject.b.ToString();
        bSlider.value = ldScriptableObject.b;

        inputB.onValueChanged.AddListener(UpdateB);
        bSlider.onValueChanged.AddListener(SliderBChange);
    }
    private void SetH()
    {
        inputH.onValueChanged.RemoveListener(UpdateH);
        hSlider.onValueChanged.RemoveListener(SliderHChange);

        inputH.text = ldScriptableObject.h.ToString();
        hSlider.value = ldScriptableObject.h;

        inputH.onValueChanged.AddListener(UpdateH);
        hSlider.onValueChanged.AddListener(SliderHChange);
    }
    private void SetK()
    {
        inputK.onValueChanged.RemoveListener(UpdateK);
        kSlider.onValueChanged.RemoveListener(SliderKChange);

        inputK.text = ldScriptableObject.k.ToString();
        kSlider.value = ldScriptableObject.k;

        inputA.onValueChanged.AddListener(UpdateK);
        kSlider.onValueChanged.AddListener(SliderKChange);
    }

    public void ConfirmChangesButton()
    {
        float a,b,h,k;
        if(!float.TryParse(inputA.text, out a))
        {
            Debug.LogError("Invalid A");
        }
        if(!float.TryParse(inputB.text, out b))
        {
            b = -99;
        }
        if(!float.TryParse(inputH.text, out h))
        {
            Debug.LogError("Invalid H");
        }
        if(!float.TryParse(inputK.text, out k))
        {
            Debug.LogError("Invalid K");
        }
        ldScriptableObject.SetAll(a,b,h,k);

    }

}
