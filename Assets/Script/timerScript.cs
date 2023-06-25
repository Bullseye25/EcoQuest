using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private float totalTime = 300f; // 5 minutes in seconds
    private float currentTime;

    private void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
        InvokeRepeating("DecreaseTimer", 1f, 1f);
    }

    private void UpdateTimerDisplay()
    {
        timerSlider.value = currentTime / totalTime;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void DecreaseTimer()
    {
        currentTime--;
        UpdateTimerDisplay();
        if (currentTime <= 0f)
        {
            CancelInvoke("DecreaseTimer");
            // Timer has reached 0, perform desired actions here
        }
    }
}
