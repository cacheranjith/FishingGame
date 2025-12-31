using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onedirectionscale : MonoBehaviour
{
    public float scaleFactor = 2.0f; // The scale factor you want to apply

    void Start()
    {
        ScaleObject();
    }

    public void ScaleObject()
    {
        // Get the current local scale of the object
        Vector3 currentScale = transform.localScale;

        // Modify the scale in the X direction
        currentScale.x *= scaleFactor;

        // Apply the new scale to the object
        transform.localScale = currentScale;
    }
}
