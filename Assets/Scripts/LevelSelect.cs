using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer;
    public Color endColor = Color.blue;
    public float lerpDuration = 2f;

    private Color startColor;

    private void Start()
    {
        if (spriteShapeRenderer == null)
        {
            Debug.LogError("SpriteShapeRenderer not found. Attach the script to a GameObject with a SpriteShapeRenderer component.");
        }

        // Capture the current color as the start color
        startColor = spriteShapeRenderer.color;
    }

    public void StartColorLerp()
    {
        // Stop any existing coroutine before starting a new one
        startColor = spriteShapeRenderer.color;
        StopCoroutine("LerpColorCoroutine");
        StartCoroutine(LerpColorCoroutine());
    }

    private IEnumerator LerpColorCoroutine()
    {
        Debug.Log("Changing color");
        float elapsed_time = 0f;

        while (elapsed_time < lerpDuration)
        {
            // Perform color lerp
            float t = elapsed_time / lerpDuration;
            Color lerpedColor = Color.Lerp(startColor, endColor, t);

            // Apply the lerped color to the Sprite Shape Renderer
            spriteShapeRenderer.color = lerpedColor;

            // Wait for the next frame
            yield return null;

            // Increment elapsed time
            elapsed_time += Time.deltaTime;
        }

        // Ensure the final color is set
        spriteShapeRenderer.color = endColor;

    }
}
