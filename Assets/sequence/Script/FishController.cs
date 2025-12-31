using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public static FishController Instance;

    public GameObject fishSpritePrefab;

    public float speed = 2f;
    public float minX = -5f;
    public float maxX = 5f;
public float spawnMinX;
public float spawnMaxX;
public float spawnMinY;
public float spawnMaxY;

    private List<Transform> fishes = new List<Transform>();
    private List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    private List<int> directions = new List<int>(); // 1 = right, -1 = left
    private List<bool> isHooked = new List<bool>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnFish(25);
    }

    void SpawnFish(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(spawnMinX, spawnMaxX);
            float y = Random.Range(spawnMinY, spawnMaxY);

            GameObject fishObj = Instantiate(
                fishSpritePrefab,
                new Vector3(x, y, 0),
                Quaternion.identity,
                transform
            );

            Transform fishTf = fishObj.transform;
            SpriteRenderer sr = fishObj.GetComponent<SpriteRenderer>();

            int dir = Random.value > 0.5f ? 1 : -1;

            fishes.Add(fishTf);
            renderers.Add(sr);
            directions.Add(dir);
            isHooked.Add(false);

            // 🔑 FORCE FLIP VISUALLY
            sr.flipX = (dir == -1);
        }
    }

    public void HookFish(Transform fish)
    {
        Debug.Log($"[FishController] HookFish called for: {fish.name}");

        int index = fishes.IndexOf(fish);
        if (index == -1)
        {
            Debug.LogError($"[FishController] Fish {fish.name} not found in fishes list!");
            return;
        }

        Debug.Log($"[FishController] Fish index: {index}, marking as hooked");
        isHooked[index] = true;

        // Find the hook GameObject
        GameObject hookObj = GameObject.FindGameObjectWithTag("Hook");
        if (hookObj == null)
        {
            Debug.LogError("[FishController] Hook GameObject not found! Make sure hook is tagged as 'Hook'");
            return;
        }

        Debug.Log($"[FishController] Hook found: {hookObj.name}, parenting fish to hook");

        // Handle Rigidbody2D to prevent physics interference
        Rigidbody2D fishRb = fish.GetComponent<Rigidbody2D>();
        if (fishRb != null)
        {
            // Make it kinematic so physics doesn't interfere with parent transform
            fishRb.bodyType = RigidbodyType2D.Kinematic;
            fishRb.velocity = Vector2.zero;
            fishRb.angularVelocity = 0f;
            Debug.Log($"[FishController] Set fish Rigidbody2D to Kinematic and cleared velocity");
        }

        // Set parent to hook
        fish.SetParent(hookObj.transform);

        // Reset local position to attach at hook's position
        // You can adjust this offset if you want the fish to appear at a specific position relative to the hook
        fish.localPosition = Vector3.zero;

        Debug.Log($"[FishController] Fish {fish.name} successfully parented to hook. Local pos: {fish.localPosition}");
    }

    void Update()
    {
        for (int i = 0; i < fishes.Count; i++)
        {
            if (isHooked[i]) continue;

            fishes[i].Translate(Vector3.right * directions[i] * speed * Time.deltaTime);

            if (fishes[i].position.x > maxX)
            {
                directions[i] = -1;
                renderers[i].flipX = true;
            }
            else if (fishes[i].position.x < minX)
            {
                directions[i] = 1;
                renderers[i].flipX = false;
            }
        }
    }
}
