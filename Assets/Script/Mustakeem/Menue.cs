using UnityEngine;
using UnityEngine.UI;

public class Menue : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public Rigidbody rb;
    private RigidbodyConstraints originalConstraints;
    public MonoBehaviour PlayerMovement;
    public GameObject pausePanel;

   

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

    // Update is called once per frame
    void Update()
    {
       
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