using UnityEngine;


public class watercontroller : MonoBehaviour
{
    public static watercontroller Instance;

    [Header("Hook Reference")]
    public Transform hook;

    [Header("Depth Settings")]
    public float surfaceY = 0f;
    public float maxDepth = -30f;

    [Header("Depth Values (Read Only)")]
    public float currentDepth;     // Raw value
    public float depth01;          // Normalized

    [Header("Water Color")]
    public SpriteRenderer waterSprite;   // Assign if using sprite
    public Renderer waterRenderer;       // Assign if using material
    public Gradient depthGradient;

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        UpdateDepth();
        UpdateWaterColor();
        currentDepth = PlayerController.Instance.maxDistance;
    }

    void UpdateDepth()
    {
        if (hook == null) return;

        currentDepth = Mathf.Clamp(surfaceY - hook.position.y, 0f, Mathf.Abs(maxDepth));
        depth01 = currentDepth / Mathf.Abs(maxDepth);
    }

    void UpdateWaterColor()
    {
        Color depthColor = depthGradient.Evaluate(depth01);

        if (waterSprite != null)
            waterSprite.color = depthColor;

        if (waterRenderer != null && waterRenderer.material != null)
            waterRenderer.material.color = depthColor;
    }
}

