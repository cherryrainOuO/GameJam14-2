using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum SFXSoundNumber { Walk, EggBreak, Attack1, Attack2, Success, Death }
public class Eun_SFXManager : Singleton<Eun_SFXManager>
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Eun_SoundManager.Instance.volume;
    }

    public void SoundPlay(int _index)
    {
        audioSource.Stop();
        audioSource.clip = audioClips[_index];
        audioSource.volume = Eun_SoundManager.Instance.volume;
        audioSource.Play();
    }


}
