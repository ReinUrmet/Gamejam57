using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    [SerializeField] private float shootCooldown = 0.05f;
    private float lastShotTime = -Mathf.Infinity;

    void Update()
    {
        // Shoot only when Space is pressed down and cooldown has passed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastShotTime + shootCooldown)
        {
            ShootDown();
            lastShotTime = Time.time;
        }
    }

    void ShootDown()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = Vector3.down * 10f;
        }

        bullet.transform.up = Vector3.down;
    }
}
