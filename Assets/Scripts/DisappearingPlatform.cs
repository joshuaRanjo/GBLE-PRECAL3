using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : LevelProp
{
    // Start is called before the first frame update

     [SerializeField]private UnityEngine.U2D.SpriteShapeRenderer spriteShapeRenderer;
     [SerializeField]private EdgeCollider2D edgeCollider;

    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    [SerializeField] private bool defaultStatus;
    private bool openStatus = true;
    private Color targetColor;
    void Start()
    {
        //spriteShapeRenderer = GetComponent<UnityEngine.U2D.SpriteShapeRenderer>();
        //edgeCollider = GetComponent<EdgeCollider2D>();

        if(!defaultStatus)
        {
            SetDisabled();
            
        }
        else{
            SetEnabled();
            
        }
    }

    public void Switch()
    {

    }

    public void SetEnabled()
    {
        if(!openStatus)
        {
            openStatus = true;
            
            edgeCollider.enabled = true;
            targetColor = enabledColor;
            StopAllCoroutines();
            StartCoroutine(ChangeColorCoroutine());
        }
        
    }
    public void SetDisabled()
    {
        if(openStatus)
        {
            openStatus = false;

            edgeCollider.enabled = false;
            targetColor = disabledColor;
            StopAllCoroutines();
            StartCoroutine(ChangeColorCoroutine());
        }
        
    }
    

    private IEnumerator ChangeColorCoroutine()
    {
        Color startColor = spriteShapeRenderer.color;
        float startTime = Time.time;

        while (Time.time - startTime < 1f)
        {
            float t = (Time.time - startTime) / 1f;
            spriteShapeRenderer.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        // Ensure the color reaches exactly the target color
        spriteShapeRenderer.color = targetColor;
    }


}
