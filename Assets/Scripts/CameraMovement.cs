using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 3f; // Units per second

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
