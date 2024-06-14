using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> spriteRenderers;

    [SerializeField]
    private Color startColor = Color.white;

    [SerializeField]
    private Color endColor = Color.black;

    [SerializeField]
    private float duration = 1.0f;

    private Coroutine blinkCoroutine;

    private void Start()
    {
        StartBlinking();
    }

    public void StartBlinking()
    {
        if (blinkCoroutine == null)
        {
            blinkCoroutine = StartCoroutine(StartBlink());
        }
    }

    public void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
    }

    private IEnumerator StartBlink()
    {
        while(true)
        {
            yield return Blink(startColor, endColor, duration);
            yield return Blink(endColor, startColor, duration);
        }
    }

    private IEnumerator Blink(Color color1, Color color2, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            
            
            float t = Mathf.PingPong(elapsedTime / duration, 1f);

            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = Color.Lerp(color1, color2, t);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.color = color2;
        }
    }
}
