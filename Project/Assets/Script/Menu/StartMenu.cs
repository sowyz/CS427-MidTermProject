using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Select Tank");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
