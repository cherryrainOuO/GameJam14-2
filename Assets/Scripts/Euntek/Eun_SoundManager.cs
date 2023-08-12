using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Eun_SoundManager : Singleton<Eun_SoundManager>
{
    public float volume = 50f;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    public void AudioPlay()
    {
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void AudioChange(int _index)
    {
        audioSource.clip = audioClips[_index];
    }

    public IEnumerator FadeOn()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, time);
            yield return null;
        }

    }

    public IEnumerator FadeOff()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volume, 0f, time);
            yield return null;
        }

    }

}
