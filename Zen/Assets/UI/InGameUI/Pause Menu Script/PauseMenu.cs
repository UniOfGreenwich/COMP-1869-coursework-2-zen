using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    public GameObject pauseMenu;
    public GameObject player;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        player.GetComponent<playerMovment>().enabled = false;
        Time.timeScale = 0f;
        
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        player.GetComponent<playerMovment>().enabled = true;
        Time.timeScale = 1f;
       
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
