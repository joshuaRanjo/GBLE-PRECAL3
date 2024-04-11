using UnityEngine;
using System.Collections;

public class OutlineScript : MonoBehaviour
{
    public Material targetMaterial;
    public float lerpDuration = 0.5f; // Duration of each lerping cycle

    void Start()
    {
        //StartCoroutine(LerpAlphaCoroutine());
    }

    private void OnEnable()
    {
        StartCoroutine(LerpAlphaCoroutine());
    }

    // Stop the coroutine when the object is set inactive
    private void OnDisable()
    {
        StopCoroutine(LerpAlphaCoroutine());
        Color startColor = targetMaterial.GetColor("_SolidOutline"); 
        Color targetColor = startColor;
        targetColor.a = 1.0f;
    }

    IEnumerator LerpAlphaCoroutine()
    {
        while (true)
        {
            yield return LerpAlpha(1.0f, 0.5f, lerpDuration); // Lerp alpha from 1.0 (100%) to 0.5 (50%)
            yield return LerpAlpha(0.5f, 1.0f, lerpDuration); // Lerp alpha from 0.5 (50%) to 1.0 (100%)
        }
    }

    IEnumerator LerpAlpha(float startAlpha, float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = targetMaterial.GetColor("_SolidOutline"); // Replace "_YourColorProperty" with the actual property name
        Color targetColor = startColor;
        targetColor.a = targetAlpha;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color lerpedColor = Color.Lerp(startColor, targetColor, t);
            lerpedColor.a = Mathf.Lerp(startAlpha, targetAlpha, t); // Lerp the alpha value
            targetMaterial.SetColor("_SolidOutline", lerpedColor); // Replace "_YourColorProperty" with the actual property name
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha value is set
        targetColor.a = targetAlpha;
        targetMaterial.SetColor("_SolidOutline", targetColor); // Replace "_YourColorProperty" with the actual property name
    

    }
}
