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
    [SerializeField] private TEXDraw equationTextFloating;

    [Header("Line Data")]
    [SerializeField] private LineData2 lineDataScriptableObject;

    private int equationType = 0;
    public bool isVertical = true;
    private string aColor = "#BC3524";
    private string bColor = "#6D37A4";
    private string hColor = "#48A655";
    private string kColor = "#2582B7";

    private string text;

    private bool inPuzzle = false;

    private string aString,bString,hString,kString;

#region EVENT_LISTENERS


    private void OnEnable()
    {
        lineDataScriptableObject.dataChangeEvent.AddListener(UpdateEquation);
        lineDataScriptableObject.attachedDataEvent.AddListener(UpdateEquation);
        lineDataScriptableObject.simplifiedEquationChange.AddListener(UpdateEquation);
        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ResetVariables);

    }

        private void OnDisable()
    {
        lineDataScriptableObject.dataChangeEvent.RemoveListener(UpdateEquation);
        lineDataScriptableObject.attachedDataEvent.RemoveListener(UpdateEquation);
        lineDataScriptableObject.simplifiedEquationChange.RemoveListener(UpdateEquation);
        EventManager.StopListening("EnterPuzzle", EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ResetVariables);
        
    } 

#endregion


    private void Start() {
        equationType = 0;
    }

    private void LateUpdate()
    {}

    private void EnterPuzzle()
    {
        inPuzzle = true;
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
        inPuzzle = false;
    }

    private void UpdateEquation()
    {
        aString = ConvertFloatToString(lineDataScriptableObject.a);
        bString = ConvertFloatToString(lineDataScriptableObject.b);
        hString = ConvertFloatToString(lineDataScriptableObject.h);
        kString = ConvertFloatToString(lineDataScriptableObject.k);
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

        equationText.text = text.Replace("\\rain","\\cmbold");
        equationTextFloating.text = text.Replace("\\rain","\\cmbold");
        if(lineDataScriptableObject.conicType == 1)
        {
             equationText.text = "\\fontsize{15pt} " + text.Replace("\\rain","\\cmbold");
             equationTextFloating.text = "\\fontsize{15pt} " + text.Replace("\\rain","\\cmbold");
        }
    }

    private void ParabolaUpdate()
    {
        if(lineDataScriptableObject.a < 0)
        { aString = lineDataScriptableObject.a.ToString(); }
        if(lineDataScriptableObject.orientation)
        {// \\rain( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2 = 4(\\color{"+aColor+"}"  + aString + "\\color{#000000})( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )
       
            text = " \\rain\\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{4(\\color{"+aColor+"}"  + aString + "\\color{#000000})} = ( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )";
        }
        else
        { // \\rain( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2} = 4(\\color{"+aColor+"}"  + aString + "\\color{#000000})( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )
         
            //equationText.text = "\\rain y = {\\color{"+aColor+"}"  + lineDataScriptableObject.a + " \\color{#000000}( x - \\color{"+hColor+"}" + lineDataScriptableObject.h + "\\color{#000000} )^2+\\color{"+kColor+"}"+ lineDataScriptableObject.k +"\\color{#000000}";
            text = "\\rain\\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{4(\\color{"+aColor+"}"  + aString + "\\color{#000000})} = ( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )";
        }
        
    }

    private void CircleUpdate()
    {
        //Debug.Log("Equation Updated C");
        text = "\\rain( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2+( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2 = \\color{"+aColor+"}" + aString +"^2" + "\\color{#000000}";
    }

    private void EllipseUpdate()
    {
        
        if(lineDataScriptableObject.simplifiedEllipse)
        {
            text = "\\rain\\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{\\color{"+aColor+"}"  + aString +  "\\color{#000000}} + \\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{\\color{"+bColor+"}" + bString + "\\color{#000000}} = 1";
        }
        else
        {
            text = "\\rain\\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{\\color{"+aColor+"}"  + aString +  "\\color{#000000}^2} + \\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{\\color{"+bColor+"}" + bString + "\\color{#000000}^2} = 1";
        }
    }

    private void HyperbolaUpdate()
    {
        //Debug.Log("Equation Updated H");
        if(lineDataScriptableObject.orientation)
        {   
            if(lineDataScriptableObject.simplifiedEllipse)
            {
                text = "\\rain\\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{\\color{"+aColor+"}" +  aString + "\\color{#000000}} - \\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{\\color{"+bColor+"}" + bString + "\\color{#000000}} = 1";
            }
            else{
                text = "\\rain\\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{\\color{"+aColor+"}" +  aString + "\\color{#000000}^2} - \\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{\\color{"+bColor+"}" + bString + "\\color{#000000}^2} = 1";
            }
            
        }
        else
        {
            if(lineDataScriptableObject.simplifiedEllipse)
            {
                text = "\\rain\\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{\\color{"+bColor+"}" +  bString + "\\color{#000000}} - \\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{\\color{"+aColor+"}" + aString + "\\color{#000000}} = 1";
            }
            else{
                text = "\\rain\\frac{( y - \\color{"+kColor+"}" + kString + "\\color{#000000} )^2}{\\color{"+bColor+"}" +  bString + "\\color{#000000}^2} - \\frac{( x - \\color{"+hColor+"}" + hString + "\\color{#000000} )^2}{\\color{"+aColor+"}" + aString + "\\color{#000000}^2} = 1";
            }
        }
    }

    private string ConvertFloatToString(float value)
    {
        if (value < 0 )
        {
            return "(" + value.ToString() + ")";
        }
        else
        {
            return value.ToString();
        }
    }
    
}
