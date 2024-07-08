using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankSelection : MonoBehaviour
{
    public GameObject[] tanks;
    public int selectedTank = 0;

    public void NextTank()
    {
        tanks[selectedTank].SetActive(false);
        selectedTank = (selectedTank + 1) % tanks.Length;
        tanks[selectedTank].SetActive(true);
    }

    public void PreviousTank()
    {
        tanks[selectedTank].SetActive(false);
        selectedTank = (selectedTank - 1 + tanks.Length) % tanks.Length;
        tanks[selectedTank].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedTank", selectedTank);
        SceneManager.LoadScene("Game");
    }
}
