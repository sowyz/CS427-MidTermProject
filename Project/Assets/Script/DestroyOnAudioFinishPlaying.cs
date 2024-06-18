using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioFinishPlaying : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitCorouTine());
    }

    IEnumerator WaitCorouTine()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}