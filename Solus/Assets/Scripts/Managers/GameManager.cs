using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject menu;
    public int loadingSceneIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        menu.SetActive(true);
    }

    public void StartNewGame()
    {
        LoadingScreenController.Instance.Show(SceneManager.LoadSceneAsync(loadingSceneIndex));

        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}