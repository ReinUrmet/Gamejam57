using UnityEngine;

public class VahiBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
        UpdateRotation();
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
        UpdateRotation();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void UpdateRotation()
    {
        if (direction != Vector3.zero)
        {
            Quaternion lookRot = Quaternion.LookRotation(direction);

            // 👇 Final fix: rotate 90° on Z to point the rocket "up" in local space
            transform.rotation = lookRot * Quaternion.Euler(90f, 0f, 0f);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
