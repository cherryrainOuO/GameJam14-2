using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Eun_DialogManager : MonoBehaviour
{
    [SerializeField] private Image dialogImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private CutSceneDialog[] dialogSystems;

    public bool isIntro = true;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField, TextArea(4, 5)] private string[] endingStr;

    private void Start()
    {
        dialogImage.sprite = sprites[0];

        Eun_SoundManager.Instance.AudioChange(0);
        Eun_SoundManager.Instance.AudioPlay();

        StartCoroutine(DialogPlaying());
    }

    private IEnumerator DialogPlaying()
    {
        for (int i = 0; i < dialogSystems.Length; i++)
        {
            yield return StartCoroutine(dialogSystems[i].UpdateDialog());

            float time = 0f;

            while (time <= 1f)
            {
                time += Time.deltaTime / .5f;
                dialogImage.color = new Color(dialogImage.color.r, dialogImage.color.g, dialogImage.color.b, 1 - time);
                yield return null;
            }

            if (i == dialogSystems.Length - 1) break;

            dialogImage.sprite = sprites[i + 1];


            time = 0f;

            while (time <= 1f)
            {
                time += Time.deltaTime / .5f;
                dialogImage.color = new Color(dialogImage.color.r, dialogImage.color.g, dialogImage.color.b, time);
                yield return null;
            }

        }

        if (isIntro) SceneManager.LoadScene("TitleScene");
        else
        {
            StartCoroutine(EndingCredit());
        }
    }

    private IEnumerator EndingCredit()
    {
        float time = 0f;

        text.text = endingStr[0];

        for (int i = 1; i <= endingStr.Length; i++)
        {
            while (time <= 1f)
            {
                time += Time.deltaTime;

                text.color = new Color(text.color.r, text.color.g, text.color.b, time);
                yield return null;
            }

            yield return YieldFunctions.WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            while (time >= 0f)
            {
                time -= Time.deltaTime;

                text.color = new Color(text.color.r, text.color.g, text.color.b, time);
                yield return null;
            }

            if (i == endingStr.Length) break;

            text.text = endingStr[i];

            yield return null;
        }

        SceneManager.LoadScene("TitleScene");
    }
}
