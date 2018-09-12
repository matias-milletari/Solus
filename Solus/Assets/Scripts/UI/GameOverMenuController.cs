using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public delegate void PauseHandler(bool paused);
    public static event PauseHandler OnGamePaused;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        if (OnGamePaused != null) OnGamePaused(true);
    }

    public void RestartGame()
    {
        GameManager.instance.StartNewGame();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
