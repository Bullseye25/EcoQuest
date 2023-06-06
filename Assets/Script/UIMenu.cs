
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{

     [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject player;
   
    public void pauseGame()
    {
        pausePanel.SetActive(true);
        player.SetActive(false);
    }
    public void resumeGame(){
        player.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void homeGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
