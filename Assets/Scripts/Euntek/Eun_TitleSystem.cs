using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Eun_TitleSystem : MonoBehaviour
{
    private int currentSelectBtn = 0;

    [SerializeField] private Image transition;
    [SerializeField] private TextMeshProUGUI[] btns;
    [SerializeField] private Image titleImage;


    private Color originColor;
    [SerializeField] private Color selectColor;

    private bool isAbleBtn = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originColor = btns[0].color;

        btns[0].color = selectColor;

        StartCoroutine(CoroutineForBtnFadeOn());
    }
    // Update is called once per frame
    void Update()
    {
        if (isAbleBtn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                audioSource.volume = Eun_SoundManager.Instance.volume;
                audioSource.clip = audioClips[0];
                audioSource.Play();

                currentSelectBtn--;

                if (currentSelectBtn < 0) currentSelectBtn = 1;

                foreach (var i in btns) i.color = originColor;
                btns[currentSelectBtn].color = selectColor;

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                audioSource.volume = Eun_SoundManager.Instance.volume;
                audioSource.clip = audioClips[0];
                audioSource.Play();

                currentSelectBtn++;

                if (currentSelectBtn > 1) currentSelectBtn = 0;

                foreach (var i in btns) i.color = originColor;
                btns[currentSelectBtn].color = selectColor;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioSource.volume = Eun_SoundManager.Instance.volume;
                audioSource.clip = audioClips[1];
                audioSource.Play();

                switch (currentSelectBtn)
                {
                    case 0:
                        StartCoroutine(GameStart());
                        break;
                    case 1:
                        StartCoroutine(Quit());
                        break;
                }
            }
        }

    }

    private IEnumerator GameStart()
    {
        isAbleBtn = false;

        StartCoroutine(CoroutineForBtnFadeOff());

        yield return StartCoroutine(CoroutineForTransition());

        SceneManager.LoadScene(""); //Todo 레벨 씬
    }

    private IEnumerator Quit()
    {
        isAbleBtn = false;

        StartCoroutine(CoroutineForBtnFadeOff());
        yield return StartCoroutine(CoroutineForTransition());

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    private IEnumerator CoroutineForBtnFadeOff()
    {
        float time = 0f;

        Vector2 originPos = new Vector2(0f, 0f);
        Vector2 targetPos = new Vector2(0f, 200f);

        while (time <= 1f)
        {
            time += Time.deltaTime;

            titleImage.transform.localPosition = Vector2.Lerp(targetPos, originPos, EasingFunctions.easeInCubic(time, 5));
            titleImage.color = new Color(titleImage.color.r, titleImage.color.g, titleImage.color.b, 1 - time);
            foreach (var i in btns)
                i.color = new Color(i.color.r, i.color.g, i.color.b, 1 - time);

            yield return null;
        }
    }

    private IEnumerator CoroutineForBtnFadeOn()
    {
        float time = 0f;

        Vector2 originPos = new Vector2(0f, 0f);
        Vector2 targetPos = new Vector2(0f, 200f);

        while (time <= 1f)
        {
            time += Time.deltaTime;

            titleImage.transform.localPosition = Vector2.Lerp(originPos, targetPos, EasingFunctions.easeInCubic(time, 5));
            titleImage.color = new Color(titleImage.color.r, titleImage.color.g, titleImage.color.b, time);
            foreach (var i in btns)
                i.color = new Color(i.color.r, i.color.g, i.color.b, time);

            yield return null;
        }

        isAbleBtn = true;
    }

    private IEnumerator CoroutineForTransition()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime;

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, time);

            yield return null;
        }
    }

}
