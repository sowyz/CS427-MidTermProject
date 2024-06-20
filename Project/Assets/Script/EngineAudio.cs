using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public float minVolume = 0.1f, maxVolume = 0.5f;
    public float volumeIncreaseSpeed = 0.05f;
    [SerializeField]
    private float currentVolume = 0.1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentVolume = minVolume;
    }

    private void Start()
    {
        audioSource.volume = currentVolume;
    }

    public void ControlEngineVolume(float speed)
    {
        if (speed > 0)
        {
            if (currentVolume < maxVolume)
            {
                currentVolume += volumeIncreaseSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (currentVolume > minVolume)
            {
                currentVolume -= volumeIncreaseSpeed * Time.deltaTime;
            }
        }
        currentVolume = Mathf.Clamp(currentVolume, minVolume, maxVolume);
        audioSource.volume = currentVolume;
    }
}
