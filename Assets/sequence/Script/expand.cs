using UnityEngine;
using UnityEngine.UI;

public class ResizeImage : MonoBehaviour
{
    public Image water;
    public Vector2 newSize;

    void Start()
    {
        // Call the function to resize the image
        Resize();
    }

    void Resize()
    {
        // Get the RectTransform component attached to the image GameObject
        RectTransform rectTransform = water.GetComponent<RectTransform>();

        // Set the size of the RectTransform to the new size
        rectTransform.sizeDelta = newSize;
    }
}
