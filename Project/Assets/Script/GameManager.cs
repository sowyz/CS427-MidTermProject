using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public SaveSystem saveSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Initialize;
    }

    private void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded GM");
        var playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            player = playerInput.gameObject;
        saveSystem = FindObjectOfType<SaveSystem>();
        if (player != null && saveSystem.LoadedData != null)
        {
            var damagable = player.GetComponentInChildren<Damagable>();
            damagable.CurrentHealth = saveSystem.LoadedData.playerHealth;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveData();
            LoadMainMenu();
        }
    }

    public void LoadLevel()
    {
        if (saveSystem.LoadedData != null)
        {
            int sceneIndex = saveSystem.LoadedData.sceneIndex;
            if (IsValidSceneIndex(sceneIndex))
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
            return;
        }
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (IsValidSceneIndex(nextSceneIndex))
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SaveData()
    {
        if (player != null)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            saveSystem.SaveData(sceneIndex, player.GetComponentInChildren<Damagable>().CurrentHealth);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetData()
    {
        saveSystem.ResetData();
    }

    private bool IsValidSceneIndex(int sceneIndex)
    {
        return sceneIndex >= 0 && sceneIndex <= 3;
    }
}
