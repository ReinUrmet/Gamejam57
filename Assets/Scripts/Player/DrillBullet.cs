using UnityEngine;

public class DrillBullet : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 720f; // Y-axis spin
    public float lifetime = 5f;

    private float currentYRotation = 0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
        transform.rotation = Quaternion.Euler(270f, 0f, 0f);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        currentYRotation += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(270f, currentYRotation, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // 💥 Destroy enemy
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        // 💥 Destroy bullet if it hits walls or other obstacles
        if (other.CompareTag("Wall") || other.CompareTag("Ground") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
