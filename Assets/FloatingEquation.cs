using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FloatingEquation : MonoBehaviour
{
    [Header("Question Data Scriptable Object")]
    [SerializeField] private QuestionData2 qdScriptableObject;
    [Header("Line Data Scriptable Object")]
    [SerializeField] private LineData2 ldScriptableObject;
    [Header("Player Data Scriptable Object")]
    [SerializeField] private PlayerData pDScriptableObject;

    [SerializeField] private Draggable2D draggable2D;

    [SerializeField] private GameObject floatingEquation;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TEXDraw floatingTitle;
    [SerializeField] private TEXDraw conieTitle;

    private bool toggle = true;

    private float offsetX = 0;
    private float offsetY = 0;
    private int quadrant = 0;

    private void OnEnable() {
        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);
        EventManager.StartListening("LevelComplete", ToggleOn);

        ldScriptableObject.dataChangeEvent.AddListener(UpdateLocation);
    }

    private void OnDisable() {
        EventManager.StopListening("EnterPuzzle", EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);

        EventManager.StartListening("LevelComplete", ToggleOn);

        ldScriptableObject.dataChangeEvent.RemoveListener(UpdateLocation);
    }

    private void UpdateOffsets()
    {
        float h = ldScriptableObject.h;
        float k = ldScriptableObject.k;
        float currentPositionX = floatingEquation.transform.position.x;
        float currentPositionY = floatingEquation.transform.position.y;

        offsetX = currentPositionX - h;
        offsetY = currentPositionY - k;
        //UpdateLocation();
        float newX = Mathf.Clamp(offsetX + h, -6,6);
        float newY = Mathf.Clamp(offsetY + k, -6,6);
        LeanTween.cancel(floatingEquation);
        LeanTween.move(floatingEquation, new Vector3(newX, newY , 0), 0.3f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void ToggleOn()
    {
        toggle = true;
    }

    public void ToggleOff()
    {
        toggle = false;
    }

    private void UpdateLocation()
    {
        if(toggle)
        {

        
            float h = ldScriptableObject.h;
            float k = ldScriptableObject.k;
            float currentPositionX = floatingEquation.transform.position.x;
            float currentPositionY = floatingEquation.transform.position.y;

            float newX = 0;
            float newY = 0;
            /*
            if(newX > 7.5f && currentPositionX > 5.5f)
                offsetX = -5f;
            if(newY > 7.5f && currentPositionY > 5.5f)
                offsetY = -3f;
            if(newX < -7.5f && currentPositionX < -5.5f)
                offsetX = 5f;
            if(newY < -7.5f && currentPositionY < -5.5f)
                offsetY = 3f;
            */
            int newQuadrant=0;
            if(h < 0 )
            {
                if(k < 0)
                {
                    newQuadrant = 3;
                }
                else 
                {
                    newQuadrant = 2;
                }
            }
            else
            {
                if(k < 0)
                {
                    newQuadrant = 1;
                }
                else
                {
                    newQuadrant = 4;
                }
            }

            if(newQuadrant != quadrant)
            {
                Debug.Log("New Quadrant");
                quadrant = newQuadrant;
                switch (quadrant)
                {
                    
                    case 1:
                        newX = -4f; newY = 4f;
                        break;
                    case 2:
                        newX = 4f; newY = -4f;
                        break;   
                    case 3:
                        newX = 4f; newY = 4f;
                        break;
                    case 4:
                        newX = -4f; newY = -4f;
                        break;    
                    default:
                        break;
                }

            // newX = Mathf.Clamp(newX, -6,6);
            // newY = Mathf.Clamp(newY, -6,6);


                LeanTween.cancel(floatingEquation);
                LeanTween.move(floatingEquation, new Vector3(newX, newY , 0), 0.3f).setEase(LeanTweenType.easeInOutQuad);
            }
        }
        //newX += offsetX; newY += offsetY;

        
    }

    private void EnterPuzzle()
    {
        if(toggle)
        {
            
        
            float h = ldScriptableObject.h;
            float k = ldScriptableObject.k;

            float posX = -4f;
            float posY = -3f;

            offsetX = 5f;
            offsetY = 3f;

            if(h < 0 )
            {
                posX = 4;
                if(k < 0)
                {
                    posY = 4f;
                    quadrant = 3;
                }
                else
                {
                    posY = -4f;
                    quadrant = 2;
                }
            }
            if(h > 0 )
            {
                posX = -4;
                if(k < 0)
                {
                    posY = 4f;
                    quadrant = 1;
                }
                else
                {
                    posY = -4f;
                    quadrant = 4;
                }
            }


            floatingEquation.transform.position = new Vector3(posX,posY ,0);
            LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f);
        }

        switch(ldScriptableObject.conicType)
        {
            case 1:
                conieTitle.text = "\\cmbold Equation of the Circle\\\\ \\tiny \\cmunso Standard Form";
                floatingTitle.text = "\\cmbold Equation of the Circle\\\\ \\tiny \\cmunso Standard Form";
                break;
            case 2:
                conieTitle.text = "\\cmbold Equation of the Ellipse\\\\ \\tiny \\cmunso Standard Form";
                floatingTitle.text = "\\cmbold Equation of the Ellipse\\\\ \\tiny \\cmunso Standard Form";
                break;
            case 3:
                conieTitle.text = "\\cmbold Equation of the Parabola\\\\ \\tiny \\cmunso Standard Form";
                floatingTitle.text = "\\cmbold Equation of the Parabola\\\\ \\tiny \\cmunso Standard Form";
                break;
            case 4:
                conieTitle.text = "\\cmbold Equation of the Hyperbola\\\\ \\tiny \\cmunso Standard Form";
                floatingTitle.text = "\\cmbold Equation of the Hyperbola\\\\ \\tiny \\cmunso Standard Form";
                break;
            default:
                break;
        }
    }

    private void ExitPuzzle()
    {
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.3f);
        conieTitle.text = "";
    }
}
