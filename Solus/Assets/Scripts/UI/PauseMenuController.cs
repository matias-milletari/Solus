using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject helpMenu;
    public GameObject audioMenu;
    public delegate void PauseHandler(bool paused);
    public static event PauseHandler OnGamePaused;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        audioMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (audioMenu.activeInHierarchy)
            {
                HideAudioMenu();
                ShowPauseMenu();
            }
            else if (helpMenu.activeInHierarchy)
            {
                HideHelpMenu();
                ShowPauseMenu();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        if (pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
            if (OnGamePaused != null) OnGamePaused(false);
            HidePauseMenu();
        }
        else
        {
            Time.timeScale = 0f;
            if (OnGamePaused != null) OnGamePaused(true);
            ShowPauseMenu();
        }
    }

    public void RestartGame()
    {
        GameManager.instance.StartNewGame();
    }

    public void ShowAudioMenu()
    {
        audioMenu.SetActive(true);
    }

    public void HideAudioMenu()
    {
        audioMenu.SetActive(false);
    }

    public void ShowHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void HideHelpMenu()
    {
        helpMenu.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
