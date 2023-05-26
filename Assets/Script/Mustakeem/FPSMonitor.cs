
using TMPro;
using UnityEngine;

public class FPSMonitor : MonoBehaviour
{
    private float timer , reFresh , avgFramerate;
    private string display = "{0} FPS";
    [SerializeField] private TextMeshProUGUI m_Text;

    private void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? reFresh : timer -= timelapse ;

        if(timer <= 0) avgFramerate = (int) (1F/timelapse);
        m_Text.text = string.Format(display,avgFramerate.ToString());
    }
}
