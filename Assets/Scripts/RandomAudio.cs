using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] bool cycle = false;
    private void Start()
    {
        if (!cycle)
        {
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }
        else
        {
            StartCoroutine(RandomSounds());
        }
    }

    private IEnumerator RandomSounds()
    {
        yield return new WaitForSeconds(Random.Range(1f, 6f));
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }
    }
}
