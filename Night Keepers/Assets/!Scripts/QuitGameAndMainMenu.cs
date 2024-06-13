using UnityEngine;
using UnityEngine.SceneManagement;

namespace NightKeepers
{
    public class QuitGameAndMainMenu : MonoBehaviour
    {
        public void QuitGameFromUI()
        {
            Application.Quit();
        }

        public void LoadMainMenu()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Main Menu");
        }
    }
}