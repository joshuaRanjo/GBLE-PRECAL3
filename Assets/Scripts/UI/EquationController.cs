using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class EquationController : MonoBehaviour
{
    [Header("Text Boxes")]
    [SerializeField] private TEXDraw equationText;

    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;

    private int equationType = 0;
    public bool isVertical = true;
    private string aColor = "#BC3524";
    private string bColor = "#6D37A4";
    private string hColor = "#48A655";
    private string kColor = "#2582B7";

#region EVENT_LISTENERS


    private void OnEnable()
    {
        lineDataScriptableObject.dataChangeEvent.AddListener(UpdateEquation);
        lineDataScriptableObject.attachedDataEvent.AddListener(UpdateEquation);
        EventManager.StartListening("ExitPuzzle", ResetVariables);

    }

        private void OnDisable()
    {
        lineDataScriptableObject.dataChangeEvent.RemoveListener(UpdateEquation);
        lineDataScriptableObject.attachedDataEvent.RemoveListener(UpdateEquation);
        EventManager.StopListening("ExitPuzzle", ResetVariables);
    } 

#endregion

    private void Start() {
        equationType = 0;
    }

    public void SetEquationType(int type)
    {
        equationType = type;
        
        //UpdateEquation();
    }


    // Reset values / set up new equation
    public void ResetVariables()
    {
        isVertical = true;
        equationText.text = "";
    }

    private void UpdateEquation()
    {
        switch (lineDataScriptableObject.conicType)
        {
            
            case 1:
                CircleUpdate();
                break;
            case 3:
                ParabolaUpdate();
                break;   
            case 2:
                EllipseUpdate();
                break;
            case 4:
                HyperbolaUpdate();
                break;    
            default:
                Debug.Log("Equation controller: Equation Type not properly set equation controller");    
                break;
        }
    }

    private void ParabolaUpdate()
    {

        equationText.text = "\\rain y = {\\color{"+aColor+"}"  + lineDataScriptableObject.a + " \\color{#000000}( x - \\color{"+hColor+"}" + lineDataScriptableObject.h + "\\color{#000000} )^2+\\color{"+kColor+"}"+ lineDataScriptableObject.k +"\\color{#000000}";
    }

    private void CircleUpdate()
    {
        //Debug.Log("Equation Updated C");
        equationText.text = "\\rain( x - \\color{"+hColor+"}" + lineDataScriptableObject.h + "\\color{#000000} )^2+( y - \\color{"+kColor+"}" + lineDataScriptableObject.k + "\\color{#000000} )^2 = \\color{"+aColor+"}" + lineDataScriptableObject.a +"^2" + "\\color{#000000}";
    }

    private void EllipseUpdate()
    {
        //Debug.Log("Equation Updated E");
        equationText.text = "\\rain\\frac{( x - \\color{"+hColor+"}" + lineDataScriptableObject.h + "\\color{#000000} )^2}{\\color{"+aColor+"}"  + lineDataScriptableObject.a +  "\\color{#000000}^2} + \\frac{( y - \\color{"+kColor+"}" + lineDataScriptableObject.k + "\\color{#000000} )^2}{\\color{"+bColor+"}" + lineDataScriptableObject.b + "\\color{#000000}^2} = 1";
    }

    private void HyperbolaUpdate()
    {
        //Debug.Log("Equation Updated H");
        equationText.text = "\\rain\\frac{( x - \\color{"+hColor+"}" + lineDataScriptableObject.h + "\\color{#000000} )^2}{\\color{"+aColor+"}" +  lineDataScriptableObject.a + "\\color{#000000}^2} - \\frac{( y - \\color{"+kColor+"}" + lineDataScriptableObject.k + "\\color{#000000} )^2}{\\color{"+bColor+"}" + lineDataScriptableObject.b + "\\color{#000000}^2} = 1";
    }
    
}
