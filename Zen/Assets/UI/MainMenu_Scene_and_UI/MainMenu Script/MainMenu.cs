using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Map Graybox");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
