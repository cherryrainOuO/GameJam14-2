using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSystem : MonoBehaviour
{
    [SerializeField] private Transform transition;
    [SerializeField] private Image gameOverBackground;
    [SerializeField] private TextMeshProUGUI gameoverTextLeft;
    [SerializeField] private TextMeshProUGUI gameoverTextRight;

    [SerializeField] private int musicIndex = 1;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private float time = 120f;

    [HideInInspector] public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        transition.gameObject.SetActive(true);

        Eun_SoundManager.Instance.AudioChange(musicIndex);
        Eun_SoundManager.Instance.AudioPlay();

        StartCoroutine(CoroutineForStartTransition());
        StartCoroutine(CoroutineForTimer());
    }

    public void ScoreUpdate(int _score)
    {
        score += _score;
        scoreText.text = score + "";
    }

    private IEnumerator CoroutineForTimer()
    {
        while (!isGameOver)
        {
            time -= Time.deltaTime;

            timer.text = (int)(time / 60) + " : " + (int)(time % 60);

            if (time <= 0) break;

            yield return null;
        }

        if (!isGameOver)
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

        SceneManager.LoadScene("EndingScene");
    }

    public IEnumerator CoroutineForGameOver()
    {
        isGameOver = true;

        Eun_SFXManager.Instance.SoundPlay((int)SFXSoundNumber.Death);

        Eun_SoundManager.Instance.AudioChange(4);
        Eun_SoundManager.Instance.AudioPlay();

        float time = 0f;

        while (time <= .4f)
        {
            time += Time.deltaTime / .7f;

            gameOverBackground.color = new Color(gameOverBackground.color.r, gameOverBackground.color.g, gameOverBackground.color.b, time);

            yield return null;
        }

        time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .5f;

            gameoverTextLeft.transform.localPosition = Vector3.Lerp(Vector2.right * -200f, Vector2.right * -40f, EasingFunctions.easeOutCubic(time, 5));
            gameoverTextRight.transform.localPosition = Vector3.Lerp(Vector2.right * 200f, Vector2.right * 40f, EasingFunctions.easeOutCubic(time, 5));

            yield return null;
        }

        yield return YieldFunctions.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .7f;

            gameoverTextLeft.color = new Color(gameoverTextLeft.color.r, gameoverTextLeft.color.g, gameoverTextLeft.color.b, 1 - time);
            gameoverTextRight.color = new Color(gameoverTextRight.color.r, gameoverTextRight.color.g, gameoverTextRight.color.b, 1 - time);

            yield return null;
        }

        time = .4f;

        while (time <= 1f)
        {
            time += Time.deltaTime / .7f;

            gameOverBackground.color = new Color(gameOverBackground.color.r, gameOverBackground.color.g, gameOverBackground.color.b, time);
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
