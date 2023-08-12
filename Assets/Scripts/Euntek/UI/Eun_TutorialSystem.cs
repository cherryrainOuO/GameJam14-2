using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Eun_TutorialSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField, TextArea(4, 5)] private string[] strs;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tutorial());
    }

    private IEnumerator Tutorial()
    {
        text.text = strs[0];

        float time = 0f;

        for (int i = 1; i <= strs.Length; i++)
        {
            while (time <= 1f)
            {
                time += Time.deltaTime / .5f;
                text.color = new Color(text.color.r, text.color.g, text.color.b, time);
                yield return null;
            }

            yield return YieldFunctions.WaitForSeconds(4f);

            while (time >= 0f)
            {
                time -= Time.deltaTime / .5f;
                text.color = new Color(text.color.r, text.color.g, text.color.b, time);
                yield return null;
            }

            yield return YieldFunctions.WaitForSeconds(1f);

            if (i == strs.Length) break;

            text.text = strs[i];
        }
    }

}

