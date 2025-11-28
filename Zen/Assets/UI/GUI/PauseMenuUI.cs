using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject player;
    public GameObject container;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            container.SetActive(true);
            player.GetComponent<playerMovment>().enabled = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
        player.GetComponent<playerMovment>().enabled = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
