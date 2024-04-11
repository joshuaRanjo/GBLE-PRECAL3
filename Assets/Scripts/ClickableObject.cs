using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ClickableObject : MonoBehaviour
{
    public UnityEvent interaction;
    [SerializeField] private GameObject outlineObject;

    private Coroutine lerpCoroutine;
    private Coroutine lerpCoroutine2;

    private bool inPuzzle = false;

    public void DoAction(){
        //Debug.Log("DoingAction");
        if(!inPuzzle)
            interaction.Invoke();

    }

    private void OnMouseOver() {
        if(!inPuzzle)
        {
            if(outlineObject != null)
            {
                //outlineObject.SetActive(true);
            }
            if(spriteRenderers.Count > 0 && lerpCoroutine == null)
            {
                lerpCoroutine = StartCoroutine(LerpColorCoroutine());
                Debug.Log("Startlerping");
            }
            if(spriteShapeRenderer != null)
            {
                StartCoroutine(LerpColorSpriteShapeCoroutine());
            }
        }
        
    }

    private void OnMouseExit() {
        StopLerpCoroutine();
    }

    private void ResetObjectSprite()
    {
        if(outlineObject != null)
        {
            //outlineObject.SetActive(false);
        }
        if(spriteRenderers.Count > 0)
        {
            foreach(SpriteRenderer sr in spriteRenderers)
            {
                sr.color = startColor;
            }
        }
        if(spriteShapeRenderer != null)
        {
            spriteShapeRenderer.color = startColor;
        }
        Debug.Log("Stoppedlerping");
    }

    private void OnEnable() {
        EventManager.StartListening("EnterConversation", DisallowLerp);
        EventManager.StartListening("ExitConversation", AllowLerp);

        EventManager.StartListening("EnterPuzzle",DisallowLerp);
        EventManager.StartListening("ExitPuzzle", AllowLerp);
        
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", DisallowLerp);
        EventManager.StopListening("ExitConversation", AllowLerp);

        EventManager.StopListening("EnterPuzzle", DisallowLerp);
        EventManager.StopListening("ExitPuzzle", AllowLerp );
    }

    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer;
    public Color startColor = Color.white;
    public Color endColor = new Color(191f / 255f, 191f / 255f, 191f / 255f); // #BFBFBF in RGB
    private float lerpDuration = 0.25f;

    private bool lerpingForward = true;

    IEnumerator LerpColorCoroutine()
    {
        while (true)
        {
            yield return LerpColor(startColor, endColor, lerpDuration);
            yield return LerpColor(endColor, startColor, lerpDuration);
        }
    }

    IEnumerator LerpColor(Color startColor, Color endColor, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.color = Color.Lerp(startColor, endColor, t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.color = endColor;
        }
    }

    IEnumerator LerpColorSpriteShapeCoroutine()
    {
        while (true)
        {
            yield return LerpColorSpriteShape(startColor, endColor, lerpDuration); // Lerp color from startColor to targetColor
            yield return LerpColorSpriteShape(endColor, startColor, lerpDuration); // Lerp color from targetColor to startColor
        }
    }

    IEnumerator LerpColorSpriteShape(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0f;
        Debug.Log("Spriteshape is lerping");
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Color lerpedColor = Color.Lerp(startColor, targetColor, t);
            spriteShapeRenderer.color = lerpedColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color value is set
        spriteShapeRenderer.color = targetColor;
    }

    public void StopLerpCoroutine()
    {
        StopAllCoroutines();
        lerpCoroutine = null;
        lerpCoroutine2 = null;
        ResetObjectSprite();
    }

    public void DisallowLerp()
    {
        inPuzzle = true;
        StopLerpCoroutine();
    }

    public void AllowLerp()
    {
        inPuzzle = false;
    }
    
}
