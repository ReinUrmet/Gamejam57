using UnityEngine;

public class Kasvaja : MonoBehaviour
{
    public GameObject bulletPrefab;     // The projectile to shoot
    public Transform firePoint;         // Where the bullet spawns
    public float shootInterval = 2f;    // Time between shots
    public float bulletSpeed = 10f;     // Speed of the bullet

    private float shootTimer = 0f;

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}

