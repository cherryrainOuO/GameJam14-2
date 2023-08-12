using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSystem : MonoBehaviour
{
    [SerializeField] private Transform transition;
    [SerializeField] private Transform gameOver;
    [SerializeField] private TextMeshProUGUI gameoverText;

    [SerializeField] private bool isEnding = false;

    [SerializeField] private int musicIndex = 1;

    [SerializeField] private TextMeshProUGUI timer;
    private float time = 30f;

    // Start is called before the first frame update
    void Start()
    {
        transition.gameObject.SetActive(true);

        Eun_SoundManager.Instance.AudioChange(musicIndex);
        Eun_SoundManager.Instance.AudioPlay();

        StartCoroutine(CoroutineForStartTransition());
        StartCoroutine(CoroutineForTimer());
    }

    private IEnumerator CoroutineForTimer()
    {
        while (true)
        {
            time -= Time.deltaTime;

            timer.text = (int)(time / 60) + " : " + (int)(time % 60);

            if (time <= 0) break;

            yield return null;
        }

        StartCoroutine(CoroutineForExitTransition());
    }

    private IEnumerator CoroutineForStartTransition()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .7f;

            transition.localPosition = Vector2.Lerp(Vector2.zero, Vector2.right * -1000f, time);

            yield return null;
        }

        //StartCoroutine(CoroutineForExitTransition());
    }

    public IEnumerator CoroutineForExitTransition()
    {
        Eun_SFXManager.Instance.SoundPlay((int)SFXSoundNumber.Success);
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .7f;

            transition.localPosition = Vector2.Lerp(Vector2.right * 1000f, Vector2.zero, time);

            yield return null;
        }

        if (!isEnding)
            SceneManager.LoadScene("LevelScene");
        else
            SceneManager.LoadScene("EndingScene");
    }

    public IEnumerator CoroutineForGameOver()
    {
        Eun_SFXManager.Instance.SoundPlay((int)SFXSoundNumber.Death);

        Eun_SoundManager.Instance.AudioChange(4);
        Eun_SoundManager.Instance.AudioPlay();

        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .7f;

            gameOver.localPosition = Vector2.Lerp(Vector2.up * 200f, Vector2.zero, time);

            yield return null;
        }

        yield return YieldFunctions.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        while (time >= 0f)
        {
            time -= Time.deltaTime / 2f;

            gameoverText.color = new Color(gameoverText.color.r, gameoverText.color.g, gameoverText.color.b, time);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
