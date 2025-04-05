using UnityEngine;

public class WanderingShooter3D_XY : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float directionChangeInterval = 2f;
    public float raycastDistance = 1f;

    public GameObject bulletPrefab;
    public float shootInterval = 3f;
    public Transform firePoint;
    public Transform player;

    private Vector3 currentDirection;
    private float moveTimer = 0f;
    private float shootTimer = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ChooseNewDirection();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        moveTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        if (moveTimer >= directionChangeInterval)
        {
            ChooseNewDirection();
            moveTimer = 0f;
        }

        if (shootTimer >= shootInterval && player != null)
        {
            ShootAtPlayer();
            shootTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, currentDirection, raycastDistance))
        {
            ChooseNewDirection();
            return;
        }

        Vector3 nextPosition = rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime;
        nextPosition.z = transform.position.z;
        rb.MovePosition(nextPosition);
    }

    void ChooseNewDirection()
    {
        float angle;
        Vector3 dir;

        do
        {
            angle = Random.Range(0f, 360f);
            dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f).normalized;
        }
        while (dir.y > 0);

        currentDirection = dir;
    }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null || player == null) return;

        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set bullet direction
        VahiBullet bulletScript = bullet.GetComponent<VahiBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(directionToPlayer);
        }

        // Prevent bullet from hitting the enemy that fired it
        Collider enemyCollider = GetComponent<Collider>();
        Collider bulletCollider = bullet.GetComponent<Collider>();
        if (enemyCollider != null && bulletCollider != null)
        {
            Physics.IgnoreCollision(enemyCollider, bulletCollider);
        }
    }



    // 💥 Kill enemy if hit by player's bullet
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet
            Destroy(gameObject);          // Destroy the enemy
        }
    }
}
