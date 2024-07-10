using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadTank : MonoBehaviour
{
    public GameObject[] tankPrefabs;
    public GameObject Player;
    public UnityEvent<GameObject> onTankChanged;
    public GameObject healthUI;
    public Slider healthBar;
    public Cinemachine.CinemachineVirtualCamera vcam;

    private void Start()
    {
        onTankChanged = new UnityEvent<GameObject>();
    }

    public void Awake()
    {
        int selectedTank = PlayerPrefs.GetInt("selectedTank", 0);
        if (selectedTank >= 0 && selectedTank < tankPrefabs.Length)
        {
            GameObject prefab = tankPrefabs[selectedTank];
            GameObject tank = Instantiate(prefab, Player.transform.position, Player.transform.rotation);
            if (tank != null)
            {
                tank.name = prefab.name;
                tank.transform.SetParent(Player.transform);

                Damagable damagable = tank.GetComponent<Damagable>();
                damagable.OnHealthChanged.AddListener(health => healthBar.value = health);
                GameOverMenu gameOverMenu = FindObjectOfType<GameOverMenu>();
                damagable.OnDeath.AddListener(() =>
                {
                    Destroy(healthUI);
                    gameOverMenu.GameOver();
                });

                vcam.Follow = tank.transform;
                vcam.LookAt = tank.transform;
                onTankChanged?.Invoke(tank);
            }
        }
        else
        {
            Debug.LogError("Selected tank index is out of range.");
        }
    }
}
