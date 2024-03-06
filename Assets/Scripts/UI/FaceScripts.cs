using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceScripts : MonoBehaviour
{
    [SerializeField] private LineData ld;
    [SerializeField] private QuestionData qd;
    [SerializeField] private bool faceMovement = false;
    public Sprite[] imageChoices;
    public Vector2 fullSize;
    public Vector2 miniSize;
    public Vector2 cornerPosition;

    private Vector2 targetSize;
    private Vector2 targetPosition;
    public float sizeChangeSpeed = 1.0f; // Set the speed for size interpolation
    public float positionChangeSpeed = 2.0f; // Set the speed for position interpolation
  
    private RectTransform uiObject;
    private Image imageComponent;

    private float percentage;

    private bool faceCoroutineStarted = false;



    private void Start() {
        uiObject = GetComponent<RectTransform>();
        imageComponent = GetComponent<Image>();
    }

    private void OnEnable() {
        ld.attachedDataEvent.AddListener(CheckPercentage);
        ld.dataChangeEvent.AddListener(CheckPercentage);

        if(faceMovement)
        {
            EventManager.StartListening("ExitPuzzle",FaceExitPuzzle);
            EventManager.StartListening("EnterPuzzle",FaceEnterPuzzle);
        }
        
    }

    private void OnDisable() {
        ld.attachedDataEvent.RemoveListener(CheckPercentage);
        ld.dataChangeEvent.RemoveListener(CheckPercentage);

        if(faceMovement)
        {
            EventManager.StopListening("ExitPuzzle",FaceExitPuzzle);
            EventManager.StopListening("EnterPuzzle",FaceEnterPuzzle);
        }
        
    }

    public void CheckPercentage()
    {
        if(qd.expectedAnswerList.Count > 0)
        {
            float a = 0;
            float b = 0;
            float h = 0;
            float k = 0;
            float count = 0;
            if(qd.maxA != -99)
            {
                a = CalculatePercentage(ld.a, qd.maxA, qd.minA, qd.expectedAnswerList[0].maxA, qd.expectedAnswerList[0].minA);
                count++;
            }
            if(qd.maxB != -99)
            {
                b = CalculatePercentage(ld.b, qd.maxB, qd.minB, qd.expectedAnswerList[0].maxB, qd.expectedAnswerList[0].minB);
                count++;
            }
            if(qd.maxH != -99)
            {
                h = CalculatePercentage(ld.h, qd.maxH, qd.minH, qd.expectedAnswerList[0].maxH, qd.expectedAnswerList[0].minH);
                count++;
            }
            if(qd.maxK != -99)
            {
                k = CalculatePercentage(ld.k, qd.maxK, qd.minK, qd.expectedAnswerList[0].maxK, qd.expectedAnswerList[0].minK);
                count++;
            }
            float totalPercentage = (a+b+h+k)/count;
            int chosenImage = 3;


            if(totalPercentage == 0)
            {chosenImage = 0;}
            else if(totalPercentage > 0f && totalPercentage < 0.20f)
            {chosenImage = 1;}
            else if(totalPercentage > 0.20f && totalPercentage < 0.5f)
            {chosenImage = 2;}
            else
            {chosenImage = 3;}

            imageComponent.sprite = imageChoices[chosenImage];
        }
        
    }

    public float CalculatePercentage(float number, float largeMax, float largeMin, float smallMax, float smallMin)
    {
        
        if (number >= smallMin && number <= smallMax)
        {
            return 0f;
        }
        else if (number < largeMin || number > largeMax)
        {
            return 1f;
        }
        else
        {
            float distanceToSmallerRange = Mathf.Min(Mathf.Abs(number - smallMin), Mathf.Abs(number - smallMax));
            float percentage;
            if((largeMax - smallMax) > (smallMin - largeMin))
                return (distanceToSmallerRange / Mathf.Abs(largeMax - smallMax));
            else
                return (distanceToSmallerRange / Mathf.Abs(smallMin - largeMin));
            //Debug.Log("Percentage distance to smaller range: " + percentage.ToString("F2") + "%");
        }
    }

    public void FaceEnterPuzzle()
    {
        targetSize = miniSize;
        targetPosition = cornerPosition;
        if(!faceCoroutineStarted)
            StartCoroutine(FaceCoroutine());
    }

    public void FaceExitPuzzle()
    {
        targetSize = fullSize;
        targetPosition = new Vector2(0,0);
        if(!faceCoroutineStarted)
            StartCoroutine(FaceCoroutine());
        imageComponent.sprite = imageChoices[0];
    }

    IEnumerator FaceCoroutine()
    {
        faceCoroutineStarted = true;
        float sizeTimer = 0f;
        float positionTimer = 0f;
        Vector2 originalPosition = uiObject.anchoredPosition;

       while (sizeTimer < 1f || uiObject.anchoredPosition != targetPosition)
        {
            // Interpolate size with a separate speed
            uiObject.sizeDelta = Vector2.Lerp(uiObject.sizeDelta, targetSize, sizeTimer / sizeChangeSpeed);

            // Interpolate position with a separate speed
            uiObject.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, positionTimer / positionChangeSpeed);

            // Increment the timers based on the time passed since the last frame
            sizeTimer += Time.deltaTime ;
            positionTimer += Time.deltaTime;

            // Clamp the timers to prevent overshooting
            sizeTimer = Mathf.Clamp01(sizeTimer);
            

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final size and position are set to the target values
        
        uiObject.sizeDelta = targetSize;
        uiObject.anchoredPosition = targetPosition;
        faceCoroutineStarted = false;
    }
}
