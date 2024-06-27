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

    private float offsetX = 0;
    private float offsetY = 0;

    private void OnEnable() {
        EventManager.StartListening("EnterPuzzle", EnterPuzzle);
        EventManager.StartListening("ExitPuzzle", ExitPuzzle);

        //ldScriptableObject.dataChangeEvent.AddListener(UpdateLocation);
    }

    private void OnDisable() {
        EventManager.StopListening("EnterPuzzle", EnterPuzzle);
        EventManager.StopListening("ExitPuzzle", ExitPuzzle);

        //ldScriptableObject.dataChangeEvent.RemoveListener(UpdateLocation);
    }

    private void UpdateOffsets()
    {
        float h = ldScriptableObject.h;
        float k = ldScriptableObject.k;
        float currentPositionX = floatingEquation.transform.position.x;
        float currentPositionY = floatingEquation.transform.position.y;

        offsetX = currentPositionX - h;
        offsetY = currentPositionY - k;
        UpdateLocation();
    }

    private void UpdateLocation()
    {
        float newX = ldScriptableObject.h;
        float newY = ldScriptableObject.k;
        float currentPositionX = floatingEquation.transform.position.x;
        float currentPositionY = floatingEquation.transform.position.y;

        if(newX > 7.5f && currentPositionX > 5.5f)
            offsetX = -5f;
        if(newY > 7.5f && currentPositionY > 5.5f)
            offsetY = -3f;
        if(newX < -7.5f && currentPositionX < -5.5f)
            offsetX = 5f;
        if(newY < -7.5f && currentPositionY < -5.5f)
            offsetY = 3f;

        newX += offsetX; newY += offsetY;

        newX = Mathf.Clamp(newX, -6,6);
        newY = Mathf.Clamp(newY, -6,6);


        LeanTween.cancel(floatingEquation);
        LeanTween.move(floatingEquation, new Vector3(newX, newY , 0), 0.1f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void EnterPuzzle()
    {
        float h = ldScriptableObject.h;
        float k = ldScriptableObject.k;

        offsetX = 5f;
        offsetY = 3f;

        if(h > 0)
            offsetX = -5f;
        if(k > 0)
            offsetY = -3f;

        if(h > 7.5f)
            offsetX = -5f;
        if(k > 7.5f)
            offsetY = -3f;
        if(h < -7.5f)
            offsetX = 5f;
        if(k < -7.5f)
            offsetY = 3f;

        floatingEquation.transform.position = new Vector3(h+offsetX, k+offsetY, 0);
        LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f);
    }

    private void ExitPuzzle()
    {
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.3f);
    }
}
