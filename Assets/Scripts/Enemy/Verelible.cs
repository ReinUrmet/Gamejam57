using UnityEngine;

public class WavyEnemy : MonoBehaviour
{
    public Transform target;           // Usually the player
    public float speed = 5f;           // Forward movement speed
    public float waveAmplitude = 2f;   // Size of the wave (how far it strays left/right)
    public float waveFrequency = 2f;   // Speed of wave (how quickly it wiggles)

    private Vector3 direction;
    private Vector3 perpendicular;

    private float waveTimer = 0f;

    void Start()
    {
        if (target == null)
        {
            // Try auto-find player by tag
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }

        if (target == null)
        {
            Debug.LogWarning("WavyEnemy: No target assigned!");
        }
    }

    void Update()
    {
        if (target == null) return;

        // Calculate direction from enemy to player
        direction = (target.position - transform.position).normalized;

        // Get a perpendicular vector for waving (cross with up for 3D)
        perpendicular = Vector3.Cross(direction, Vector3.up);

        // Advance the timer
        waveTimer += Time.deltaTime * waveFrequency;

        // Calculate offset using sine wave
        Vector3 waveOffset = perpendicular * Mathf.Sin(waveTimer) * waveAmplitude;

        // Final movement direction: toward the player + wave
        Vector3 finalDirection = direction + waveOffset;

        // Move enemy
        transform.position += finalDirection.normalized * speed * Time.deltaTime;

        // Optional: Rotate to face the player (not necessary if you want static orientation)
        transform.forward = direction;
    }
}
