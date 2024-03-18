using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableObject : MonoBehaviour
{

    public bool isInRange;
    public UnityEvent interaction;
    private InputAction.CallbackContext context;
    [SerializeField] private GameObject outlineObject;

    public void DoAction(){
        Debug.Log("DoingAction");
        interaction.Invoke();
        if(outlineObject != null)
        {
            //outlineObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Interactor")){
            isInRange = true;
            if(outlineObject != null)
            {
                //outlineObject.SetActive(true);
            }
            if(spriteRenderers.Count > 0)
            {
                StartCoroutine(LerpColorCoroutine());
            }
            if(spriteShapeRenderer != null)
            {
                StartCoroutine(LerpColorSpriteShapeCoroutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Interactor")){
            isInRange = false;
            StopLerp();
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
        }
    }
    private void OnEnable() {
        EventManager.StartListening("EnterConversation", StopLerp);
        EventManager.StartListening("ExitConversation", StopLerp);

        EventManager.StartListening("EnterPuzzle",StopLerp);
        EventManager.StartListening("ExitPuzzle", StopLerp);
        
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", StopLerp);
        EventManager.StopListening("ExitConversation", StopLerp);

        EventManager.StopListening("EnterPuzzle", StopLerp);
        EventManager.StopListening("ExitPuzzle", StopLerp );
    }


    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer;
    public Color startColor = Color.white;
    public Color endColor = new Color(191f / 255f, 191f / 255f, 191f / 255f); // #BFBFBF in RGB
    public float lerpDuration = 0.5f;

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

    public void StopLerp()
    {
        StopAllCoroutines();
    }


}