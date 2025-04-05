using UnityEngine;

public class WanderingShooter3D_XY : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float directionChangeInterval = 2f;
    public float raycastDistance = 1f;

    public GameObject bulletPrefab;
    public float shootInterval = 3f;
    public Transform firePoint;       // A child transform for bullet spawn
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

        // Change direction
        if (moveTimer >= directionChangeInterval)
        {
            ChooseNewDirection();
            moveTimer = 0f;
        }

        // Shoot at player
        if (shootTimer >= shootInterval && player != null)
        {
            ShootAtPlayer();
            shootTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        // Check for wall collision ahead
        if (Physics.Raycast(transform.position, currentDirection, raycastDistance))
        {
            ChooseNewDirection();
            return;
        }

        // Move in direction (X and Y only)
        Vector3 nextPosition = rb.position + currentDirection * moveSpeed * Time.fixedDeltaTime;
        nextPosition.z = transform.position.z; // Lock Z to current value
        rb.MovePosition(nextPosition);
    }
    void ChooseNewDirection()
    {
        // Only allow movement left/right/down — never up (Y must be <= 0)
        float angle;
        Vector3 dir;

        do
        {
            angle = Random.Range(0f, 360f);
            dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f).normalized;
        }
        while (dir.y > 0); // Repeat until Y is 0 or negative (no upward movement)

        currentDirection = dir;
    }


    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null || player == null) return;

        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Tell the bullet which way to go
        bullet.GetComponent<VahiBullet>().SetDirection(directionToPlayer);
    }


}
