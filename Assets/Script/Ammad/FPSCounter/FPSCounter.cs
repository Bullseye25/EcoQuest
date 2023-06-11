using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    private float deltaTime;

    private void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("FPSCounter: TextMeshProUGUI reference is missing!");
            enabled = false;
        }
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.RoundToInt(fps);
    }
}
