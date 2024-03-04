using UnityEngine;
using UnityEditor;


public class RangePercentageCalculator : MonoBehaviour
{
    public float largeRangeMin = 0f;
    public float largeRangeMax = 100f;
    public float smallerRangeMin = 25f;
    public float smallerRangeMax = 75f;
    public float number = 50f;

    void Start()
    {
        CalculateDistance();
    }

    public void CalculateDistance()
    {
        if (number >= smallerRangeMin && number <= smallerRangeMax)
        {
            Debug.Log("Number falls within the smaller range. Distance: 0");
        }
        else if (number < largeRangeMin || number > largeRangeMax)
        {
            Debug.Log("Number is outside the large range.");
        }
        else
        {
            float distanceToSmallerRange = Mathf.Min(Mathf.Abs(number - smallerRangeMin), Mathf.Abs(number - smallerRangeMax));
            float percentage;
            if((largeRangeMax - smallerRangeMax) > (smallerRangeMin - largeRangeMin))
                percentage = (distanceToSmallerRange / Mathf.Abs(largeRangeMax - smallerRangeMax)) * 100f;
            else
                percentage = (distanceToSmallerRange / Mathf.Abs(smallerRangeMin - largeRangeMin)) * 100f;
            Debug.Log("Percentage distance to smaller range: " + percentage.ToString("F2") + "%");
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(RangePercentageCalculator))]
public class RPCEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RangePercentageCalculator rpCalculator = (RangePercentageCalculator)target;

        if (GUILayout.Button("Calculate"))
        {
            rpCalculator.CalculateDistance();
        }
    }


}

#endif