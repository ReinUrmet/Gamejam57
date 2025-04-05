using UnityEngine;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour
{
    public GameObject chunkPrefab;           // Chunk'i prefab, mille põhjal uusi luuakse
    public Transform cameraTransform;        // Kaamera, mis liigub alla
    public int chunkHeight = 100;            // Chunk'i kõrgus ühikutes (sobita prefabi suurusega)
    public int renderDistance = 5;           // Mitu chunk'i ette ja taha genereeritakse

    private Dictionary<int, GameObject> spawnedChunks = new Dictionary<int, GameObject>();

    void Start()
    {
        // Genereeri esimesed chunk'id
        for (int i = 0; i < renderDistance; i++)
        {
            SpawnChunk(i);
        }
    }

    void Update()
    {
        // Arvuta, mis chunk'i sees kaamera hetkel asub (alla liikudes suureneb indeks)
        int cameraChunk = Mathf.FloorToInt(cameraTransform.position.y / -chunkHeight);

        // Genereeri kõik chunk'id, mida veel pole ja mis peaksid kaamera ees olemas olema
        for (int i = cameraChunk; i <= cameraChunk + renderDistance; i++)
        {
            SpawnChunk(i);
        }

        // Leia chunk'id, mis on liiga kõrgel (st kaamera juba ammu möödunud)
        int safeZoneAbove = cameraChunk - 2; // 👈 jättes 2 chunk'i varuks ülevalpool

        List<int> toRemove = new List<int>();
        foreach (var kvp in spawnedChunks)
        {
            if (kvp.Key < safeZoneAbove)
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }

        // Eemalda kustutatud chunk'id ka loendist
        foreach (int key in toRemove)
        {
            spawnedChunks.Remove(key);
        }
    }

    void SpawnChunk(int index)
    {
        // Ära genereeri uuesti kui juba olemas
        if (spawnedChunks.ContainsKey(index)) return;

        // Arvuta chunk'i positsioon põhinedes tema indeksil
        Vector3 spawnPos = new Vector3(0, -index * chunkHeight, 0);
        GameObject chunk = Instantiate(chunkPrefab, spawnPos, Quaternion.identity);
        chunk.name = "Chunk_" + index;

        // Kui chunk'il on sisu genereerija (nt platvormid vms), kutsu see välja
        var chunkScript = chunk.GetComponent<Chunk>();
        if (chunkScript != null)
        {
            chunkScript.Generate(index);
        }

        // Lisa chunk jälgitavate hulka
        spawnedChunks.Add(index, chunk);
    }
}
