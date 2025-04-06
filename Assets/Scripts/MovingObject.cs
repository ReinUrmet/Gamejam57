using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveDistance = 5f;     // How far to move from center
    public float speed = 2f;            // How fast it moves
    public bool moveRightFirst = true;  // Toggle: move right first or left first

    private Vector3 startPos;
    private float directionMultiplier = 1f;

    void Start()
    {
        startPos = transform.position;

        // If we want to move left first, reverse the direction
        directionMultiplier = moveRightFirst ? 1f : -1f;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * moveDistance * directionMultiplier;
        Vector3 newPos = startPos + new Vector3(offset, 0f, 0f);
        transform.position = newPos;
    }
}
