using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupButtonClick : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();
            }
        }

        if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                LoadMainMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Q)  || Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }

    void PauseGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        //SceneManager.UnloadSceneAsync("PauseMenu");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
