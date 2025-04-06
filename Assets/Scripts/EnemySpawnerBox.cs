using UnityEngine;

public class EnemySpawnerBox : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public bool spawnOnce = true;

    private bool hasSpawned = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (spawnOnce && hasSpawned) return;

        SpawnEnemies();
        hasSpawned = true;
    }

    void SpawnEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            Vector3 spawnPos = point.position;
            spawnPos.z = 62.24f; // 🔥 Force Z position

            // 👇 Rotate 180° on Y-axis from the original rotation
            Quaternion rotatedY = Quaternion.Euler(point.rotation.eulerAngles + new Vector3(0f, 180f, 0f));

            Instantiate(enemyPrefab, spawnPos, rotatedY);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
                Gizmos.DrawWireSphere(point.position, 0.5f);
        }
    }
}
