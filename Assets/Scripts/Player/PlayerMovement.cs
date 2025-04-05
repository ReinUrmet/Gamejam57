using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 2f;
    public Rigidbody rig;

    private bool isBouncing = false;
    private float bounceTimer = 0f;
    private PlayerHealth healthScript;

    void Start()
    {
        // Get the PlayerHealth script from the same GameObject
        GameObject gm = GameObject.Find("GameManager"); // Make sure name matches!
        if (gm != null)
        {
            healthScript = gm.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (!isBouncing)
        {
            Move(); // ← Only move if not bouncing
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            Jetpack();
        }

        if (isBouncing)
        {
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0f)
            {
                isBouncing = false;
            }
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = rig.linearVelocity;
        velocity.x = x * moveSpeed;
        rig.linearVelocity = velocity;
    }

    void Jetpack()
    {
        Vector3 velocity = rig.linearVelocity;
        velocity.y = jetpackForce; // Set exact upward velocity
        rig.linearVelocity = velocity;
    }

    void LateUpdate()
    {
        // Snap Z to a constant to stay in 2.5D
        Vector3 pos = transform.position;
        pos.z = 77.7f;
        transform.position = pos;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (healthScript != null)
            {
                healthScript.TakeDamage(1);
            }

            // Optional: trigger bounce so player can't take damage instantly again
            TriggerBounceCooldown(0.5f); // Example cooldown time
        }
    }

    public void TriggerBounceCooldown(float duration)
    {
        isBouncing = true;
        bounceTimer = duration;
    }
}

