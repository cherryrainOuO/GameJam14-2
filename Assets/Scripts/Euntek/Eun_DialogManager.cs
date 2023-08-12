using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eun_DialogManager : MonoBehaviour
{
    [SerializeField] private Image dialogImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private CutSceneDialog[] dialogSystems;

    private void Start()
    {
        dialogImage.sprite = sprites[0];

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
    }
}
