using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShrinkUI : MonoBehaviour
{
    public RectTransform uiObject;
    public float targetWidth = 100f; // Set your desired target width
    public float targetHeight = 50f; // Set your desired target height
    public float shrinkTime = 1.0f; // Set the time it takes to complete the shrink

    void Start()
    {
        if (uiObject == null)
        {
            uiObject = GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // You can change the condition as per your requirements
        {
            StartCoroutine(ShrinkOverTime());
        }
    }

    IEnumerator ShrinkOverTime()
    {
        float timer = 0f;
        Vector2 originalSize = uiObject.sizeDelta;
        Vector2 targetSize = new Vector2(targetWidth, targetHeight);

        while (timer < shrinkTime)
        {
            // Interpolate between the original size and the target size over time
            uiObject.sizeDelta = Vector2.Lerp(originalSize, targetSize, timer / shrinkTime);

            // Increment the timer based on the time passed since the last frame
            timer += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final size is set to the target size
        uiObject.sizeDelta = targetSize;
    }
}
