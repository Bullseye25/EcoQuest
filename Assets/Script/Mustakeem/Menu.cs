using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pausePanel;

   

    // Start is called before the first frame update
    void Start()
    {
       
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
        player.SetActive(false);
        
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        player.SetActive(true);
        
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void homeGame()
    {
        SceneManager.LoadScene("MainMenue");
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