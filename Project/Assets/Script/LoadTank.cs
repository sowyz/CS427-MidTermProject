using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cinemachine;

public class LoadTank : MonoBehaviour
{
    public GameObject[] tankPrefabs;
    public GameObject Player;
    public UnityEvent<GameObject> onTankChanged;
    public GameObject healthUI;
    public Slider healthBar, reloadIndactor;
    public Cinemachine.CinemachineVirtualCamera vcam;

    private void Awake()
    {
        if (onTankChanged == null)
        {
            onTankChanged = new UnityEvent<GameObject>();
        }

        if (Player == null)
        {
            Debug.LogError("Player is not assigned.");
            return;
        }

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
                if (damagable != null)
                {
                    damagable.OnHealthChanged.AddListener(health =>
                    {
                        healthBar.value = (float)health;
                    });

                    GameOverMenu gameOverMenu = FindObjectOfType<GameOverMenu>();
                    damagable.OnDeath.AddListener(() =>
                    {
                        Destroy(healthUI);
                        gameOverMenu.GameOver();
                    });

                    Turret turret = tank.GetComponentInChildren<Turret>();
                    if (turret != null)
                    {
                        turret.OnReloading.AddListener(currentDelay => reloadIndactor.value = currentDelay);
                    }

                    UIFollowTank uiFollowTank = Player.GetComponentInChildren<UIFollowTank>();
                    if (uiFollowTank != null)
                    {
                        uiFollowTank.toFollow = tank.transform;
                    }

                    if (vcam != null)
                    {
                        vcam.Follow = tank.transform;
                        vcam.LookAt = tank.transform;
                    }

                    onTankChanged?.Invoke(tank);
                }
                else
                {
                    Debug.LogError("Damagable not found.");
                }
            }
        }
        else
        {
            Debug.LogError("Tank index is out of range.");
        }
    }
}
