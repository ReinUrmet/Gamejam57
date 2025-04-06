using UnityEngine;

public class Kasvajabullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction = Vector3.right; // Default is right
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after X seconds
        SetInitialRotation();
    }

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        SetInitialRotation();
    }

    void SetInitialRotation()
    {
        // Set Y rotation based on direction
        if (direction.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (direction.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
