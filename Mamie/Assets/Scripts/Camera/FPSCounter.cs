using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Assign this in the inspector

    private int frameCount;
    private float deltaTime;
    private float fps;
    private float updateInterval = 0.5f; // Update interval in seconds

    void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("FPS Text component not assigned!");
        }
    }

    void Update()
    {
        frameCount++;
        deltaTime += Time.deltaTime;

        if (deltaTime > updateInterval)
        {
            fps = frameCount / deltaTime;
            fpsText.text = $"FPS: {fps:F2}";

            frameCount = 0;
            deltaTime -= updateInterval;
        }
    }
}
