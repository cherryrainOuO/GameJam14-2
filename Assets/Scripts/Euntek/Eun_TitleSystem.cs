using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Eun_TitleSystem : MonoBehaviour
{
    [SerializeField] private Transform transition;
    [SerializeField] private Transform transition2;
    [SerializeField] private Transform startText;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.volume = Eun_SoundManager.Instance.volume;
        audioSource.Play();

        StartCoroutine(CoroutineForStartTransition());
        StartCoroutine(CoroutineForTextMove());
    }

    private IEnumerator CoroutineForTextMove()
    {
        float time = 0f;

        while (true)
        {
            while (time <= 1f)
            {
                time += Time.deltaTime;
                startText.localPosition = Vector2.Lerp(Vector2.up * -70f, Vector2.up * -65f, EasingFunctions.easeInCubic(time, 5));
                yield return null;
            }


            time = 0f;

            while (time <= 1f)
            {
                time += Time.deltaTime;
                startText.localPosition = Vector2.Lerp(Vector2.up * -65f, Vector2.up * -70f, EasingFunctions.easeInCubic(time, 5));
                yield return null;
            }

            time = 0f;

        }


    }

    private IEnumerator CoroutineForStartTransition()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .5f;
            transition.localPosition = Vector2.Lerp(Vector2.zero, Vector2.up * 200f, EasingFunctions.easeInCubic(time, 5));
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.volume = Eun_SoundManager.Instance.volume;
            audioSource.clip = audioClips[1];
            audioSource.Play();

            StartCoroutine(GameStart());

        }


    }

    private IEnumerator GameStart()
    {
        yield return StartCoroutine(CoroutineForTransition());

        SceneManager.LoadScene("TutorialScene"); //Todo 레벨 씬
    }

    private IEnumerator CoroutineForTransition()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .7f;

            transition2.localPosition = Vector2.Lerp(Vector2.right * 1000f, Vector2.zero, time);

            yield return null;
        }
    }

}
