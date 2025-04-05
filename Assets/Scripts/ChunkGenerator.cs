using UnityEngine;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour
{
    public GameObject chunkPrefabA;         // Esimene chunk prefab
    public GameObject chunkPrefabB;         // Teine chunk prefab
    public Transform cameraTransform;       // Kaamera, mis liigub alla
    public int chunkHeight = 100;           // Chunk'i kõrgus ühikutes (sobita prefabi suurusega)
    public int renderDistance = 5;          // Mitu chunk'i ette ja taha genereeritakse

    private Dictionary<int, GameObject> spawnedChunks = new Dictionary<int, GameObject>();

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

        Vector3 spawnPos = new Vector3(0, -index * chunkHeight, 0);

        // 🎲 Choose a random prefab
        GameObject prefabToUse = (Random.value < 0.5f) ? chunkPrefabA : chunkPrefabB;

        GameObject chunk = Instantiate(prefabToUse, spawnPos, Quaternion.identity);
        chunk.name = "Chunk_" + index;

        var chunkScript = chunk.GetComponent<Chunk>();
        if (chunkScript != null)
        {
            chunkScript.Generate(index);
        }

        spawnedChunks.Add(index, chunk);
    }
}
