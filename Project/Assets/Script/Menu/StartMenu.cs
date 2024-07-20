using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Select Tank");
    }

    public void ContinueGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadLevel();
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
