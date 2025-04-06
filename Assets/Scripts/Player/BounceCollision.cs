using UnityEngine;

public class BounceCollision : MonoBehaviour
{
    public float bounceStrength = 10f;
	public AudioSource source;
	public AudioClip clip;

    void OnCollisionEnter(Collision collision)
    {
        // Bounce only off "Wall" or "Object" tags
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("Hit the wall");
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 normal = contact.normal;

                // Check for side hit (left or right only), not top/bottom
                bool hitSideways = Mathf.Abs(normal.x) > 0.5f;
                bool notFromTopOrBottom = Mathf.Abs(normal.y) < 0.5f;

                if (hitSideways && notFromTopOrBottom)
                {
                    Rigidbody rb = GetComponent<Rigidbody>();

                    // Clear horizontal velocity and apply bounce force
                    Vector3 bounce = new Vector3(normal.x * bounceStrength, 0f, 0f);
                    rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, rb.linearVelocity.z);
                    rb.AddForce(bounce, ForceMode.VelocityChange);
					
					// Play sound
					source.PlayOneShot(clip);

                    // Trigger movement cooldown so bounce isn't canceled
                    PlayerMovement movementScript = GetComponent<PlayerMovement>();
                    if (movementScript != null)
                    {
                        movementScript.TriggerBounceCooldown(0.2f);
                    }
                }
            }
        }
    }
}


