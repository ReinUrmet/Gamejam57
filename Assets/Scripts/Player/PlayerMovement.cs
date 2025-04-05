using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 2f;
    public Rigidbody rig;

    private bool isBouncing = false;
    private float bounceTimer = 0f;

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

    public void TriggerBounceCooldown(float duration)
    {
        isBouncing = true;
        bounceTimer = duration;
    }
}

