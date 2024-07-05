using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassScreen : MonoBehaviour
{
    public GameObject WinGameLevelUI;
    void GameOver()
    {
        WinGameLevelUI.SetActive(true);
        Debug.Log("Game Over");
    }
    public void ShowWinGameLevelUI()
    {
        //check if the current scene is the last scene
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // If it is the last scene, move to the first scene
            //SceneManager.LoadScene(0);
            Debug.Log("You have completed the game!");
            return;
        }

        // Move to the next level
        WinGameLevelUI.SetActive(true);
        Debug.Log("You have passed the level!");        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Level");
    }
    
    public void TurnBackMainMenu(){
        SceneManager.LoadScene(0);
        Debug.Log("Turn back to main menu");
    }
}
