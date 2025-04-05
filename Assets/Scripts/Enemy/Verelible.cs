using UnityEngine;

public class WavyEnemy : MonoBehaviour
{
    public Transform target;           // Usually the player
    public float speed = 5f;           // Forward movement speed
    public float waveAmplitude = 2f;   // Size of the wave
    public float waveFrequency = 2f;   // Wiggle speed

    private Vector3 direction;
    private Vector3 perpendicular;

    private float waveTimer = 0f;
    private PlayerHealth healthScript;

    void Start()
    {
        if (target == null)
        {
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

        // Get perpendicular wave direction
        perpendicular = Vector3.Cross(direction, Vector3.up);

        waveTimer += Time.deltaTime * waveFrequency;

        Vector3 waveOffset = perpendicular * Mathf.Sin(waveTimer) * waveAmplitude;

        Vector3 finalDirection = direction + waveOffset;

        transform.position += finalDirection.normalized * speed * Time.deltaTime;

        // Optional: face player
        transform.forward = direction;
    }

    // 💥 Destroy enemy on bullet hit

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            if (healthScript != null)
            {
                healthScript.TakeDamage(1);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
