using UnityEngine;

public class FishTrigger : MonoBehaviour
{
    [Header("Debug Settings")]
    [Tooltip("Enable to see collision debug messages in Console")]
    public bool enableDebugLogs = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enableDebugLogs)
        {
            Debug.Log($"[FishTrigger] Collision detected with: {other.gameObject.name} (Tag: {other.tag})");
        }

        if (other.CompareTag("Hook"))
        {
            if (enableDebugLogs)
            {
                Debug.Log($"[FishTrigger] Hook detected! Attempting to hook fish: {gameObject.name}");
            }

            // Check if FishController instance exists
            if (FishController.Instance == null)
            {
                Debug.LogError("[FishTrigger] FishController.Instance is NULL! Cannot hook fish.");
                return;
            }

            // Attempt to hook the fish
            FishController.Instance.HookFish(transform);

            if (enableDebugLogs)
            {
                Debug.Log($"[FishTrigger] Fish {gameObject.name} successfully hooked!");
            }
        }
        else
        {
            if (enableDebugLogs)
            {
                Debug.LogWarning($"[FishTrigger] Collision with non-hook object. Expected tag 'Hook', got '{other.tag}'");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (enableDebugLogs && other.CompareTag("Hook"))
        {
            Debug.Log($"[FishTrigger] Hook exited collision with fish: {gameObject.name}");
        }
    }

    // Validate setup on start
    private void Start()
    {
        ValidateSetup();
    }

    private void ValidateSetup()
    {
        // Check for Collider2D
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError($"[FishTrigger] {gameObject.name} is missing a Collider2D component! Add CircleCollider2D or BoxCollider2D.");
        }
        else if (!col.isTrigger)
        {
            Debug.LogWarning($"[FishTrigger] {gameObject.name}'s Collider2D 'Is Trigger' is NOT enabled! Enable it in Inspector.");
        }

        // Check for Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning($"[FishTrigger] {gameObject.name} is missing a Rigidbody2D component. Add one and set Body Type to Kinematic.");
        }
        else if (rb.bodyType != RigidbodyType2D.Kinematic)
        {
            Debug.LogWarning($"[FishTrigger] {gameObject.name}'s Rigidbody2D should be set to Kinematic for best results.");
        }

        if (enableDebugLogs)
        {
            Debug.Log($"[FishTrigger] {gameObject.name} setup validation complete.");
        }
    }
}
