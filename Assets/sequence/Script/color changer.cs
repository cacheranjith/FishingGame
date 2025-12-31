using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorchanger : MonoBehaviour
{
    // Public variables to adjust the colors and duration in the Unity Inspector
    public Color startColor = new Color(0.678f, 0.847f, 1f); // Light Blue
    public Color endColor = new Color(0f, 0f, 0.545f); // Dark Blue
    public float duration = 5.0f; // Duration in seconds

    private Renderer objRenderer;
    private float timeElapsed;

    void Start()
    {
        // Get the Renderer component of the GameObject
        objRenderer = GetComponent<Renderer>();

        // Set the initial color
        if (objRenderer != null)
        {
            objRenderer.material.color = startColor;
        }
    }

    void Update()
    {
        if (objRenderer != null)
        {
            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Calculate the proportion of the duration that has passed
            float t = timeElapsed / duration;

            // Lerp the color between startColor and endColor based on the elapsed time
            objRenderer.material.color = Color.Lerp(startColor, endColor, t);

            // Reset timeElapsed if the duration is exceeded to loop the color change
            if (timeElapsed > duration)
            {
                timeElapsed = 0f;
            }
        }
    }
}