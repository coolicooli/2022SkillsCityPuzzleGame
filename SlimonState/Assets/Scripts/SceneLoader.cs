using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    healthTest resetHealth;
    playerCollectsKeypieces resetCollectedPeices;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void RestartFromWinningScene()
    {
        //resetHealth.health = 3;
        //resetCollectedPeices.numberOfPiecesCollected = 0;
        SceneManager.LoadScene("Level1");
    }
    public void RestartFromLosingScene()
    {   
        //resetHealth.health = 3;
        //resetCollectedPeices.numberOfPiecesCollected = 0;
        SceneManager.LoadScene("Level1");
    }
    public void MenuFromWinningScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void MenuFromLosingScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelFromIntro()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
