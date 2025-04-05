using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 2f;
    public Rigidbody rig;

    public GameObject animationObject; // 👈 Drag your PlayerAnimation child here
    private Animator anim;

    private bool isBouncing = false;
    private float bounceTimer = 0f;
    private PlayerHealth healthScript;

    private bool wasFalling = false;

    void Start()
    {
        anim = animationObject.GetComponent<Animator>(); // 👈 Animator is on the child

        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            healthScript = gm.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (!isBouncing)
        {
            Move();
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            Jetpack();
        }

        if (isBouncing)
        {
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0f)
            {
                isBouncing = false;
            }
        }

        UpdateAnimations();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = rig.linearVelocity;
        velocity.x = x * moveSpeed;
        rig.linearVelocity = velocity;
    }

    void Jetpack()
    {
        Vector3 velocity = rig.linearVelocity;
        velocity.y = jetpackForce;
        rig.linearVelocity = velocity;
    }

    void UpdateAnimations()
    {
        float x = rig.linearVelocity.x;
        float y = rig.linearVelocity.y;

        bool movingLeft = x < 0;
        bool movingRight = x > 0;
        bool jetpacking = y > 0.2f;
        bool falling = y < -0.2f;

        // 🔽 Priority: Left/Right first
        if (movingLeft)
        {
            anim.SetBool("IsMovingLeft", true);
            anim.SetBool("IsMovingRight", false);
            anim.SetBool("IsJetpacking", false);
            anim.SetBool("IsFalling", false);
            anim.SetBool("StartedFall", false);
        }
        else if (movingRight)
        {
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingRight", true);
            anim.SetBool("IsJetpacking", false);
            anim.SetBool("IsFalling", false);
            anim.SetBool("StartedFall", false);
        }
        else
        {
            anim.SetBool("IsMovingLeft", false);
            anim.SetBool("IsMovingRight", false);

            anim.SetBool("IsJetpacking", jetpacking);
            anim.SetBool("IsFalling", falling);

            if (falling && !wasFalling)
            {
                anim.SetBool("StartedFall", true);
            }
            else if (!falling)
            {
                anim.SetBool("StartedFall", false);
            }
        }

        wasFalling = falling;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = 77.7f;
        transform.position = pos;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (healthScript != null)
            {
                healthScript.TakeDamage(1);
            }

            TriggerBounceCooldown(0.5f);
        }
    }

    public void TriggerBounceCooldown(float duration)
    {
        isBouncing = true;
        bounceTimer = duration;
    }
}
