using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonClick : MonoBehaviour
{
    [SerializeField] private int ButtonIndex; // Index of the menu button from inside Unity script
    private static bool isShowOptionScreen = true; // toggle option screen state

    void Start()
    {
        // set up the listener for the click event
        Button frontMenuButton = GetComponent<Button>();
        frontMenuButton.onClick.AddListener(OnMenuButtonClick);
        isShowOptionScreen = true;
    }

    // ** Front End Menu to handle button Click Event
    public void OnMenuButtonClick()
    {
        switch (ButtonIndex)
        {
            case 0: // Play
                PlayNewGame();
                break;
            
            case 1: // Options
                if (isShowOptionScreen) GameOptions();
                break;

            case 2: // Quit
                QuitGame();
                break;

            case 3: // Exit Option Screen
                isShowOptionScreen = false;
                GameOptions();
                break;

            default:
                break;
        }
    }

    // ** Start new game, load new scene
    void PlayNewGame()
    {
        SceneManager.LoadScene(2);
    }

    // ** Show Options screen method
    void GameOptions()
    {
        ShowOptionsScreen(isShowOptionScreen);

        isShowOptionScreen = !isShowOptionScreen;

        AudioManager.audioManager.PlayMenuSwish();
    }

    // ** Quit game (not inside Unity)
    void QuitGame()
    {
        AudioManager.audioManager.PlayButtonDown();
        Application.Quit();
    }

    // ** Show or Hide options screen fade in/out animation
    private void ShowOptionsScreen(bool Show)
    {
        string animationToShow = Show ? "Show Options" : "Hide Options";
        {
            Animator anim = Animator.FindObjectOfType<Animator>();
            //anim.GetComponent<Animator>().Play(animationToShow);

            anim.Play(animationToShow, 0, 0.0f);

            anim = null;
        }
    }

    // Clean up button click listener
    private void OnDestroy()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.RemoveListener(OnMenuButtonClick);
    }
    
}
