using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TestScript : MonoBehaviour
{
    public SpriteRenderer progressBarFill; // Reference to the sprite renderer of the filled part
    public float progress = 0.5f; // Current progress (0 to 1)

    // Update is called once per frame
    void Update()
    {
        // Ensure progress is clamped between 0 and 1
        progress = Mathf.Clamp01(progress);

        // Set the scale of the filled part according to the progress
        progressBarFill.transform.localPosition = new Vector3(progress, 1f, 1f);
    }
}

