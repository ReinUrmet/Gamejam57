using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class DrillBullet : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 720f;
    public float lifetime = 5f;

    private float currentYRotation = 0f;
    private Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, lifetime);

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false; // MUST be false for trigger detection
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        transform.rotation = Quaternion.Euler(270f, 0f, 0f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector3.down * speed * Time.fixedDeltaTime);

        currentYRotation += rotationSpeed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(270f, currentYRotation, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall") || other.CompareTag("Ground") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
