using UnityEngine;

public class DrillBUllet : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 720f; // Degrees per second
    public float lifetime = 5f;

    private float currentYRotation = 0f;

    void Start()
    {
        // Destroy the bullet after some time
        Destroy(gameObject, lifetime);

        // Ensure it starts facing downward
        transform.rotation = Quaternion.Euler(270f, 0f, 0f);
    }

    void Update()
    {
        // Move the bullet straight down
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Update the Y rotation value manually
        currentYRotation += rotationSpeed * Time.deltaTime;

        // Apply locked X rotation (-90°, i.e. 270°) and spinning Y rotation
        transform.rotation = Quaternion.Euler(270f, currentYRotation, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}


