using UnityEngine;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour
{
    public List<GameObject> chunkPrefabs;   // List of chunk prefabs (set in Inspector)
    public Transform cameraTransform;       // Camera that moves downward
    public int chunkHeight = 100;           // Height of each chunk
    public int renderDistance = 5;          // How many chunks to render ahead

    private Dictionary<int, GameObject> spawnedChunks = new Dictionary<int, GameObject>();
    private GameObject lastSpawnedPrefab = null;

    void Start()
    {
        for (int i = 0; i < renderDistance; i++)
        {
            SpawnChunk(i);
        }
    }

    void Update()
    {
        int cameraChunk = Mathf.FloorToInt(cameraTransform.position.y / -chunkHeight);

        for (int i = cameraChunk; i <= cameraChunk + renderDistance; i++)
        {
            SpawnChunk(i);
        }

        int safeZoneAbove = cameraChunk - 2;

        List<int> toRemove = new List<int>();
        foreach (var kvp in spawnedChunks)
        {
            if (kvp.Key < safeZoneAbove)
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }

        foreach (int key in toRemove)
        {
            spawnedChunks.Remove(key);
        }
    }

    void SpawnChunk(int index)
    {
        if (spawnedChunks.ContainsKey(index)) return;

        if (chunkPrefabs.Count == 0)
        {
            Debug.LogWarning("ChunkGenerator: No chunk prefabs assigned!");
            return;
        }

        Vector3 spawnPos = new Vector3(0, -index * chunkHeight, 0);

        // 🎲 Pick a random chunk that's not the same as the last one
        GameObject selectedPrefab = null;
        int attempts = 0;
        do
        {
            selectedPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
            attempts++;
        }
        while (selectedPrefab == lastSpawnedPrefab && chunkPrefabs.Count > 1 && attempts < 10);

        GameObject chunk = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        chunk.name = "Chunk_" + index;

        var chunkScript = chunk.GetComponent<Chunk>();
        if (chunkScript != null)
        {
            chunkScript.Generate(index);
        }

        spawnedChunks.Add(index, chunk);
        lastSpawnedPrefab = selectedPrefab; // ✅ Store the last one used
    }
}
