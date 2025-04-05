using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject platformPrefab;
    public int platformCount = 5;

    public void Generate(int seed)
    {
        Random.InitState(seed);

        for (int i = 0; i < platformCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(0f, 10f), Random.Range(-5f, 5f));
            GameObject platform = Instantiate(platformPrefab, transform);
            platform.transform.localPosition = pos;
        }
    }
}
