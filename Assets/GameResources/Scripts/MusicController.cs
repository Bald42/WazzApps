using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер музыки
/// </summary>
public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic ()
    {
        audioSource.clip = clips[Random.Range(0,clips.Count)];
        audioSource.Play();
    }
}