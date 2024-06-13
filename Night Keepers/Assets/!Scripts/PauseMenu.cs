using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Continue()
    {
        PausePanel?.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
