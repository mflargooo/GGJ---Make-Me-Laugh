using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private void Start()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
