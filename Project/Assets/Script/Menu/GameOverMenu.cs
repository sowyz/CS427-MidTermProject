using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    void Start()
    {
        gameOverMenuUI.SetActive(false); // Ensure the menu is hidden at the start
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void TurnBackMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Debug.Log("Turn back to main menu");
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Over");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
