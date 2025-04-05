using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 2f;
    public Rigidbody rig;

    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.Space))
        {
            Jetpack();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 velocity = rig.linearVelocity;
        velocity.x = x * moveSpeed;
        velocity.z = 0f; // Lock to 2D plane
        rig.linearVelocity = velocity;
    }

    void Jetpack()
    {
        rig.AddForce(Vector3.up * jetpackForce, ForceMode.Force);
    }

    void LateUpdate()
    {
        // Lock Z axis
        Vector3 pos = transform.position;
        pos.z = 77.7f;
        transform.position = pos;
    }
}
