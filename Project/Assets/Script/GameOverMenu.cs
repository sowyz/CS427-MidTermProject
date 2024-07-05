using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    // Update is called once per frame
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void TurnBackMainMenu(){
        SceneManager.LoadScene(0);
        Debug.Log("Turn back to main menu");
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Debug.Log("Game Over");
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
