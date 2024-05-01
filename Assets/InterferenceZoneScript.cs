using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InterferenceZoneScript : LevelProp
{
    public bool defaultStatus = false;
    public bool activatedStatus = false;

    private float time1 = 0.35f;
    private float time2 = 0.35f;
    private float time3 = 0.3f;

    private PolygonCollider2D collider;


    private UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer;

    void OnEnable()
    {
        spriteShapeRenderer = GetComponent<UnityEngine.U2D.SpriteShapeRenderer>();
        collider = GetComponent<PolygonCollider2D>();
        if(defaultStatus)
        {
            activatedStatus = true;
        }
        else{
            activatedStatus = false;
            DeactivateIntereferece();
        }
    }

    public void Switch()
    {
        activatedStatus = !activatedStatus;

        if(activatedStatus)
        {
            ActivateIntereferece();
        }
        else
        {
            DeactivateIntereferece();
        }
    }

    public void ActivateIntereferece()
    {
        StartCoroutine( ChangeTransparencyCoroutine(0.3f,time1, () =>
                        {
                            StartCoroutine(ChangeTransparencyCoroutine(0.1f, time2, () =>
                            {
                                StartCoroutine(ChangeTransparencyCoroutine(0.3f,time3));
                            }));
                        }));    
        collider.enabled = true;
    }

    public void DeactivateIntereferece()
    {
        StartCoroutine( ChangeTransparencyCoroutine(0.1f,time1, () =>
                        {
                            StartCoroutine(ChangeTransparencyCoroutine(0.3f, time2, () =>
                            {
                                StartCoroutine(ChangeTransparencyCoroutine(0.0f,time3));
                            }));
                        }));
        collider.enabled = false;                
    }

    private IEnumerator ChangeTransparencyCoroutine(float targetAlpha, float transitionDuration, Action onComplete = null)
    {
        // Get the current color of the fill material
        Color startColor = spriteShapeRenderer.color;
        float elapsedTime = 0f;
        //Debug.Log("Transition time " + transitionDuration);
        // Loop until the desired transparency is reached
        while (elapsedTime < transitionDuration)
        {
            // Calculate the new alpha value based on the interpolation of start and target alpha
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / transitionDuration);
            
            // Set the new alpha value to the color
            Color newColor = startColor;
            newColor.a = newAlpha;

            // Assign the new color to the fill material
            spriteShapeRenderer.color = newColor;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the target alpha is set correctly at the end of the transition
        startColor.a = targetAlpha;
        spriteShapeRenderer.color = startColor;

        onComplete?.Invoke();
    }
}
