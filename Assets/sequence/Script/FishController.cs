using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FishType
{
    public GameObject prefab;
    public int value;
    public float speedMultiplier = 1f;
}

[System.Serializable]
public class LevelRule
{
    public int maxFishCatch;
}

public class FishController : MonoBehaviour
{
    public static FishController Instance;

    public GameObject fishSpritePrefab;

    [Header("Fish Types")]
    public FishType[] fishTypes;
    public LevelRule[] levels;
    private int currentLevel = 0;
    private int currentCatch = 0;

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
    private List<int> fishTypeIndex = new List<int>();

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
        // 1️⃣ Pick random fish type
        int typeIndex = Random.Range(0, fishTypes.Length);
        FishType type = fishTypes[typeIndex];

        // 2️⃣ Random spawn position (within reachable area)
        float x = Random.Range(spawnMinX, spawnMaxX);
        float y = Random.Range(spawnMinY, spawnMaxY);

        // 3️⃣ Spawn fish
        GameObject fishObj = Instantiate(
            type.prefab,
            new Vector3(x, y, 0),
            Quaternion.identity,
            transform
        );

        // 4️⃣ Cache components
        Transform fishTf = fishObj.transform;
        SpriteRenderer sr = fishObj.GetComponent<SpriteRenderer>();

        // 5️⃣ Direction
        int dir = Random.value > 0.5f ? 1 : -1;

        fishes.Add(fishTf);
        renderers.Add(sr);
        directions.Add(dir);
        isHooked.Add(false);
        fishTypeIndex.Add(typeIndex);

        // 6️⃣ NORMALIZE orientation
        sr.flipX = false;
    }
}

    public void HookFish(Transform fish)
    {
    if (currentCatch >= levels[currentLevel].maxFishCatch)
        return;

    int index = fishes.IndexOf(fish);
    if (index == -1 || isHooked[index])
        return;

    isHooked[index] = true;

    Transform hook = GameObject.FindGameObjectWithTag("Hook").transform;

    fish.SetParent(hook);

    int value = fishTypes[fishTypeIndex[index]].value;
Debug.Log("Caught fish value: " + value);

    // STACKING
    float offsetY = -0.4f * currentCatch;
    fish.localPosition = new Vector3(0, offsetY, 0);

    currentCatch++;
    }

    public void ClearHookedFish()
{
    for (int i = 0; i < fishes.Count; i++)
    {
        if (isHooked[i])
        {
            fishes[i].gameObject.SetActive(false);
            isHooked[i] = false;
        }
    }

    currentCatch = 0;
}

public void NextLevel()
{
    currentLevel++;

    if (currentLevel >= levels.Length)
    {
        currentLevel = levels.Length - 1; // stay at last level
    }
}

    void Update()
    {
        for (int i = 0; i < fishes.Count; i++)
        {
            if (isHooked[i]) continue;

            float typeSpeed = speed * fishTypes[fishTypeIndex[i]].speedMultiplier;
            fishes[i].Translate(Vector3.right * directions[i] * typeSpeed * Time.deltaTime);

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
        Debug.Log("Level: " + currentLevel + " / Caught: " + currentCatch);
    }
}
