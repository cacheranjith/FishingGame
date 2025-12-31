using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WavePrefab
    {
        public GameObject prefab;
        public float speed;
    }

    public List<WavePrefab> wavePrefabs; // List of wave prefabs to be instantiated
    public int numberOfWaves = 5; // Number of waves to spawn
    public float spawnRangeMinX = -10f; // Minimum X coordinate for spawning waves
    public float spawnRangeMaxX = 10f; // Maximum X coordinate for spawning waves
    public float spawnRangeMinY = -5f; // Minimum Y coordinate for spawning waves
    public float spawnRangeMaxY = 5f; // Maximum Y coordinate for spawning waves
    public float minimumDistanceBetweenWaves = 2f; // Minimum distance between waves
    public float minimumYDistanceBetweenWaves = 1f; // Minimum Y distance between waves
    public float maxXDistance = 15f; // Maximum X distance for waves to travel

    private List<GameObject> waves = new List<GameObject>();

    void Start()
    {
        SpawnWaves();
    }

    void SpawnWaves()
    {
        int attempts = 0; // To prevent infinite loop in case of impossible placement conditions
        int maxAttempts = 100 * numberOfWaves; // A safety limit to avoid infinite loops

        for (int i = 0; i < numberOfWaves; i++)
        {
            Vector2 newPosition;
            bool positionValid = false;

            while (!positionValid && attempts < maxAttempts)
            {
                attempts++;
                newPosition = new Vector2(
                    Random.Range(spawnRangeMinX, spawnRangeMaxX),
                    Random.Range(spawnRangeMinY, spawnRangeMaxY)
                );

                positionValid = true;

                // Check if the new position is far enough from all existing waves
                foreach (GameObject wave in waves)
                {
                    if (wave != null && Vector2.Distance(newPosition, wave.transform.position) < minimumDistanceBetweenWaves)
                    {
                        positionValid = false;
                        break;
                    }
                }

                if (positionValid)
                {
                    // Randomly select a wave prefab from the list
                    WavePrefab selectedWavePrefab = wavePrefabs[Random.Range(0, wavePrefabs.Count)];

                    // Instantiate the wave
                    GameObject wave = Instantiate(selectedWavePrefab.prefab, newPosition, Quaternion.identity);
                    waves.Add(wave);

                    // Set movement parameters for parent and all children
                    bool moveRight = Random.value > 0.5f; // Randomly decide if wave moves right or left
                    SetWaveMovement(wave, selectedWavePrefab.speed, moveRight);

                    break; // Move on to the next wave
                }
            }

            // Safety check to ensure we do not enter an infinite loop
            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Max attempts reached. Some waves might not have been placed.");
                break;
            }
        }
    }

    void SetWaveMovement(GameObject wave, float speed, bool moveRight)
    {
        // Set movement parameters for the parent wave
        WaveMovement parentWaveMovement = wave.GetComponent<WaveMovement>();
        if (parentWaveMovement == null)
        {
            parentWaveMovement = wave.AddComponent<WaveMovement>();
        }
        parentWaveMovement.speed = speed;
        parentWaveMovement.maxXDistance = maxXDistance;
        parentWaveMovement.originalX = wave.transform.position.x;
        parentWaveMovement.direction = moveRight ? 1 : -1; // Set direction based on moveRight flag

        // Set movement parameters for all child waves (if any)
        foreach (Transform child in wave.transform)
        {
            WaveMovement childWaveMovement = child.gameObject.GetComponent<WaveMovement>();
            if (childWaveMovement == null)
            {
                childWaveMovement = child.gameObject.AddComponent<WaveMovement>();
            }
            childWaveMovement.speed = speed;
            childWaveMovement.maxXDistance = maxXDistance;
            childWaveMovement.originalX = wave.transform.position.x;
            childWaveMovement.direction = moveRight ? 1 : -1; // Set direction based on moveRight flag
        }
    }

    private class WaveMovement : MonoBehaviour
    {
        public float speed; // Speed of wave movement
        public float maxXDistance; // Maximum X distance for waves to travel
        public float originalX; // Starting X position
        public int direction = 1; // 1 for right, -1 for left

        void Update()
        {
            // Move the wave
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

            // Check if the wave has reached the maximum X distance
            if (Mathf.Abs(transform.position.x - originalX) >= maxXDistance)
            {
                // Change direction
                direction *= -1;
            }
        }
    }
}
