using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] private Rigidbody rb;
    private RigidbodyConstraints originalConstraints;
    [SerializeField] private MonoBehaviour PlayerMovement;
    [SerializeField] private GameObject pausePanel;

   

    // Start is called before the first frame update
    void Start()
    {
        originalConstraints = rb.constraints;
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

   
   
    public void Pause()
    {
        pausePanel.SetActive(true);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        PlayerMovement.enabled = false;
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        rb.constraints = originalConstraints;
        PlayerMovement.enabled = true;
    }
    public void Exit()
    {
        Application.Quit();
    }


    // Sound Slider Settings
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}