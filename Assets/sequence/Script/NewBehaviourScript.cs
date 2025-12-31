using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform hook; // Reference to the hook
    public Camera mainCamera; // Reference to the main camera
    public float maxDepth = -10f; // Maximum depth where color change occurs
    public float minDepth = 0f; // Minimum depth where color is original
    public Color surfaceColor = Color.cyan; // Color at the surface
    public Color deepColor = Color.black; // Color at the maximum depth

    void Update()
    {
        // Get the current depth of the hook
        float currentDepth = hook.position.y;

        // Clamp the depth between minDepth and maxDepth
        currentDepth = Mathf.Clamp(currentDepth, maxDepth, minDepth);

        // Calculate the t value for Lerp
        float t = Mathf.InverseLerp(minDepth, maxDepth, currentDepth);

        // Interpolate between surfaceColor and deepColor based on the depth
        Color currentColor = Color.Lerp(surfaceColor, deepColor, t);

        // Set the background color
        mainCamera.backgroundColor = currentColor;
    }
}

