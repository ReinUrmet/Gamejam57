using UnityEngine;
using TMPro;

public class DepthCounter : MonoBehaviour
{
    public TextMeshProUGUI depthText; // Assign this in Inspector
    private int depth = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.2f)
        {
            depth++;
            depthText.text = depth.ToString();
            timer = 0f;
        }
    }
}
