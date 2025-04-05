using UnityEngine;

public class FloatUpPhysics : MonoBehaviour
{
    public float speed = 2f;
    private float initialZ;

    void Start()
    {
        initialZ = transform.position.z;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Lock Z position
        Vector3 pos = transform.position;
        pos.z = initialZ;
        transform.position = pos;
    }
}
