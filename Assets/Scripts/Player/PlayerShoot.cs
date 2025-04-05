using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Collider playerCollider; // Assign your player's Collider in Inspector

    [SerializeField] private float shootCooldown = 0.05f;
    private float lastShotTime = -Mathf.Infinity;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastShotTime + shootCooldown)
        {
            ShootDown();
            lastShotTime = Time.time;
        }
    }

    void ShootDown()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Spawn slightly below the firePoint to avoid overlapping with the player
        Vector3 spawnPos = firePoint.position + Vector3.down * 0.1f;

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        bullet.tag = "PlayerBullet"; 

        // Align bullet to face downward
        bullet.transform.up = Vector3.down;

        // Ignore collision between bullet and player
        Collider bulletCol = bullet.GetComponent<Collider>();
        if (bulletCol != null && playerCollider != null)
        {
            Physics.IgnoreCollision(bulletCol, playerCollider);
        }
    }
}
