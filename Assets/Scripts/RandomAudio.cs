using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] private float minStartTime = 0f;
    [SerializeField] private float maxStartTime = .5f;
    [SerializeField] private float minCycleTime = 3f;
    [SerializeField] private float maxCycleTime = 7f;

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
        yield return new WaitForSeconds(Random.Range(minStartTime, maxStartTime));
        while (true)
        {
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            yield return new WaitForSeconds(Random.Range(minCycleTime, maxCycleTime));
        }
    }
}
