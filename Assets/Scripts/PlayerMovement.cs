using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 2f;
    public Rigidbody rig;

    public GameObject bulletPrefab;       // Assign bullet prefab in the Inspector
    public Transform firePoint;           // Empty GameObject below the player

    [SerializeField] private float shootCooldown = 0.05f;  // Time between allowed shots
    private float lastShotTime = -Mathf.Infinity;

    void Update()
    {
        Move();

        // Jetpack on hold
        if (Input.GetKey(KeyCode.Space))
        {
            Jetpack();
        }

        // Shoot only when Space is *pressed* (not held), and cooldown passed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastShotTime + shootCooldown)
        {
            ShootDown();
            lastShotTime = Time.time;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 velocity = rig.linearVelocity;
        velocity.x = x * moveSpeed;
        velocity.z = 0f; // Lock player to 2D plane
        rig.linearVelocity = velocity;
    }

    void Jetpack()
    {
        rig.AddForce(Vector3.up * jetpackForce, ForceMode.Force);
    }

    void ShootDown()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Spawn bullet at fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Give bullet downward velocity
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = Vector3.down * 10f;
        }

        // Visually rotate bullet to face downward
        bullet.transform.up = Vector3.down;
    }

    void LateUpdate()
    {
        // Keep player locked on the Z axis (side-scrolling plane)
        Vector3 pos = transform.position;
        pos.z = 77.7f;
        transform.position = pos;
    }
}
