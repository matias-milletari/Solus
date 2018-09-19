using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject helpMenu;
    public GameObject audioMenu;
    public AudioSource mainMenuAudioSource;

    void Start()
    {
        mainMenu.SetActive(true);
        audioMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMainMenu();
        }
    }

    public void ShowAudioMenu()
    {
        mainMenu.SetActive(false);
        audioMenu.SetActive(true);
    }

    public void ShowHelpMenu()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        audioMenu.SetActive(false);
        helpMenu.SetActive(false);
    }
}
