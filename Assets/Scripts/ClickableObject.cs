using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ClickableObject : MonoBehaviour
{
    public UnityEvent interaction;
    
    [SerializeField] private GameObject outlineObject;

    private Coroutine lerpCoroutine;
    private Coroutine lerpCoroutine2;

    public LayerMask blockingLayer = 10;

    private bool inPuzzle = false;
    private bool entered = false;

    public void DoAction(){
        //Debug.Log("DoingAction");
        if(!inPuzzle)
            interaction.Invoke();

    }

    private void OnMouseOver() {
        if(!inPuzzle)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Debug.Log("Mouse is over UI");
                return;
            }
            if(!entered)
            {
                //Debug.Log("MouseOver Object");
                entered = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                // Check if the ray hits something on the blocking layer
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("InterferenceLayer"));
                if(hit.collider == null)
                {
                    if(outlineObject != null)
                    {
                        //outlineObject.SetActive(true);
                    }
                    if(spriteRenderers.Count > 0 && lerpCoroutine == null)
                    {
                        lerpCoroutine = StartCoroutine(LerpColorCoroutine());
                        //Debug.Log("Startlerping");
                    }
                    if(spriteShapeRenderer != null)
                    {
                        StartCoroutine(LerpColorSpriteShapeCoroutine());
                    }
                    if(spriteShapeRenderers.Count > 0 && lerpCoroutine == null)
                    {
                        lerpCoroutine = StartCoroutine(LerpColorSpriteShapeCoroutine());
                    }
                    EventManager.TriggerEvent("PuzzleHover");
                }
                
            }
        }
        
    }

    private void OnMouseExit() {
        
        StopLerpCoroutine();
        if(!inPuzzle)
            EventManager.TriggerEvent("PuzzleHoverExit");
        entered = false;
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
        if(spriteShapeRenderers.Count > 0)
        {
            foreach(UnityEngine.U2D.SpriteShapeRenderer sr in spriteShapeRenderers)
            {
                sr.color = startColor;
            }
        }

        //Debug.Log("Stoppedlerping");
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
    public List<UnityEngine.U2D.SpriteShapeRenderer> spriteShapeRenderers = new List<UnityEngine.U2D.SpriteShapeRenderer>();
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
        //Debug.Log("Spriteshape is lerping2");
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            foreach(UnityEngine.U2D.SpriteShapeRenderer renderer in spriteShapeRenderers)
            {
                renderer.color = Color.Lerp(startColor, targetColor, t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach(UnityEngine.U2D.SpriteShapeRenderer renderer in spriteShapeRenderers)
        {
            renderer.color = endColor;
        }
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
