using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadTank : MonoBehaviour
{
    public GameObject[] tankPrefabs;
    public GameObject Player;
    public UnityEvent<GameObject> onTankChanged;

    private void Start()
    {
        onTankChanged = new UnityEvent<GameObject>();
        
    }

    public void Awake()
    {
        int selectedTank = PlayerPrefs.GetInt("selectedTank", 0); // Added default value 0
        if (selectedTank >= 0 && selectedTank < tankPrefabs.Length)
        {
            GameObject prefab = tankPrefabs[selectedTank];
            GameObject tank = Instantiate(prefab, Player.transform.position, Player.transform.rotation);
            tank.name = prefab.name;
            tank.transform.SetParent(Player.transform);
            if (tank != null)
            {
                onTankChanged?.Invoke(tank);
            }
        }
        else
        {
            Debug.LogError("Selected tank index is out of range.");
        }
    }
}
