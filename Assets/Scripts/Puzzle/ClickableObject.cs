using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ClickableObject : MonoBehaviour
{
    public UnityEvent interaction;
    public int layerToCheck = 2;
    
    [SerializeField] private GameObject outlineObject;

    private Coroutine lerpCoroutine;
    private Coroutine lerpCoroutine2;

    public LayerMask blockingLayer = 10;

    private bool inPuzzle = false;
    private bool entered = false;
    private bool blinking = false;

    public LineData2 ld;

    public void DoAction(){
        //Debug.Log("DoingAction");
        //if(!inPuzzle)
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
        
        if(!inPuzzle){
            StopLerpCoroutine();
            EventManager.TriggerEvent("PuzzleHoverExit");
        }

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
        EventManager.StartListening("EnterPuzzle", Blink);
        
        EventManager.StartListening("ExitPuzzle", AllowLerp);
        EventManager.StartListening("ExitPuzzle", StopBlink);
        

        allColliders = FindObjectsOfType<Collider>();
    }

    private void OnDisable() {
        EventManager.StopListening("EnterConversation", DisallowLerp);
        EventManager.StopListening("ExitConversation", AllowLerp);

        EventManager.StopListening("EnterPuzzle", DisallowLerp);
        EventManager.StopListening("EnterPuzzle", Blink);

        EventManager.StopListening("ExitPuzzle", AllowLerp );
        EventManager.StopListening("ExitPuzzle",StopBlink );
    }


    private void Start()
    {
        ld = Resources.Load<LineData2>("ScriptableObjects/LineData2");
    }

    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    public List<UnityEngine.U2D.SpriteShapeRenderer> spriteShapeRenderers = new List<UnityEngine.U2D.SpriteShapeRenderer>();
    public Color startColor = Color.white;
    public Color endColor = new Color(191f / 255f, 191f / 255f, 191f / 255f); // #BFBFBF in RGB
//new Color(255f / 255f, 132f / 255f, 132f / 255f);
    public Color endColor3 = new Color(255f / 255f, 132f / 255f, 132f / 255f);
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

    private void StopBlink()
    {
        StopLerpCoroutine();
    }

    private void Blink()
    {
        if(ld.puzzleObject == gameObject || transform.parent.gameObject == ld.puzzleObject)
        {

        
                    if(spriteRenderers.Count > 0 && lerpCoroutine == null)
                    {
                        lerpCoroutine = StartCoroutine(LerpColorCoroutine());
                        //Debug.Log("Startlerping");
                    }
                    if(spriteShapeRenderers.Count > 0 && lerpCoroutine == null)
                    {
                        lerpCoroutine = StartCoroutine(LerpColorSpriteShapeCoroutine());
                    }
        }
        else
        {
            StopBlink();
        }

    }
    
    private Collider[] allColliders;
    public bool isColliding()
    {
        Collider thisCollider = GetComponent<Collider>();

        if (thisCollider == null)
        {
            Debug.LogError("No collider attached to this object.");
            return false;
        }

        foreach (var otherCollider in allColliders)
        {
            // Skip checking against itself
            if (otherCollider == thisCollider)
                continue;

            // Check if the other collider is in the specified layer mask
            if (otherCollider.gameObject.layer != layerToCheck)
                continue;

            // Check if the colliders are intersecting
            if (thisCollider.bounds.Intersects(otherCollider.bounds))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator LerpColorRed(float duration)
    {
        float timeElapsedRed = 0f;
        float lerpDuration = 1f;
        
        while (timeElapsedRed < duration)
        {        
            
            if(spriteRenderers.Count > 0)
            {
                yield return LerpColor(startColor, endColor3, lerpDuration);
                yield return LerpColor(endColor3, startColor, lerpDuration);
                timeElapsedRed += (lerpDuration *2);
            }
            else if(spriteShapeRenderers.Count > 0)
            {
                yield return LerpColorSpriteShape(startColor, endColor3, lerpDuration); // Lerp color from startColor to targetColor
                yield return LerpColorSpriteShape(endColor3, startColor, lerpDuration); // Lerp color from targetColor to startColor
                timeElapsedRed += (lerpDuration *2);
            }
            
            lerpDuration -= 0.07f;
            Debug.Log(timeElapsedRed);
        }
        Debug.Log("Done");
        GetComponent<PuzzleObject>().ResetObject();
    }

    private void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("started");
            StartCoroutine(LerpColorRed(15f));
        }
        */
    }    
}
