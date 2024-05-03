using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleConditionSwitch : MonoBehaviour
{
    [SerializeField] private int conditionCount = 0;
    [SerializeField] private int fullConditionCount = 1;

    [SerializeField] private UnityEvent conditionFulfilledEvent;
    [SerializeField] private UnityEvent conditionUnfulfilledEvent;

    public void IncreaseCondition()
    {
        conditionCount++;
        if (conditionCount >= fullConditionCount)
        {
            conditionFulfilledEvent.Invoke();
        }
    }

    public void DecreaseCondition()
    {
        conditionCount--;
        if(conditionCount < fullConditionCount)
        {
            conditionUnfulfilledEvent.Invoke();
        }
    } 
}
