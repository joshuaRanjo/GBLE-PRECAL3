using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleConditionSwitch : LevelProp
{
    [SerializeField] private int conditionCount = 0;
    [SerializeField] private int fullConditionCount = 1;

    [SerializeField] private UnityEvent conditionFulfilledEvent;
    [SerializeField] private UnityEvent conditionUnfulfilledEvent;
    public float speed = 1f;


    public void IncreaseCondition()
    {
        conditionCount++;
        
        StopAllCoroutines();
        progress = (float)conditionCount / fullConditionCount;
        StartCoroutine(MoveToObject());

        if (conditionCount >= fullConditionCount)
        {
            conditionFulfilledEvent.Invoke();
        }
    }

    public void DecreaseCondition()
    {
        conditionCount--;

        StopAllCoroutines();
        progress = (float)conditionCount / fullConditionCount;
        StartCoroutine(MoveToObject());

        if(conditionCount < fullConditionCount)
        {
            conditionUnfulfilledEvent.Invoke();
        }
    }
    public GameObject progressBarFill; // Reference to the sprite renderer of the filled part
    public float progress = 1f; // Current progress (0 to 1)

    // Update is called once per frame
    private void Start() {
    }


    IEnumerator MoveToObject()
    {
        //Mathf.Clamp01(conditionCount/fullConditionCount)
        Vector3 startPosition = progressBarFill.transform.localPosition;
        Vector3 target = new Vector3(progress,0f,0f);
        float distance = Vector3.Distance(startPosition, target);

        float startTime = Time.time;
        float journeyLength = distance / speed;

        while (Time.time - startTime < journeyLength)
        {
            float fractionOfJourney = (Time.time - startTime) / journeyLength;
            progressBarFill.transform.localPosition = Vector3.Lerp(startPosition, target, fractionOfJourney);
            yield return null;
        }

        // Ensure the object reaches the exact target position
        progressBarFill.transform.localPosition = target;
    }

}
