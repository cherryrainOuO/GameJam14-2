using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Eun_UIOnOff : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CoroutineForUIOnOff());
    }

    private IEnumerator CoroutineForUIOnOff()
    {
        for (int i = 0; i < 5; i++)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

            yield return YieldFunctions.WaitForSeconds(.5f);

            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

            yield return YieldFunctions.WaitForSeconds(.5f);
        }
    }

}
