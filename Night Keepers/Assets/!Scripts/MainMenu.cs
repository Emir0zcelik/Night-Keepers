using NightKeepers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField seed;
    [SerializeField] private GameObject seedPanel;
    public void PlayGame()
    {
        seedPanel.SetActive(true);
    }

    public void GameSceneChange()
    {
        Seed.Instance.GameSeed = seed.text;
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}