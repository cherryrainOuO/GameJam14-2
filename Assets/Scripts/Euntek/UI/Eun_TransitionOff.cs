using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eun_TransitionOff : MonoBehaviour
{
    private Image transition;
    // Start is called before the first frame update
    void Start()
    {
        transition = GetComponent<Image>();
        StartCoroutine(CoroutineForTransitionOff());
    }

    private IEnumerator CoroutineForTransitionOff()
    {
        float time = 0f;

        while (time <= 1f)
        {
            time += Time.deltaTime;

            transition.color = new Color(transition.color.r, transition.color.g, transition.color.b, 1 - time);
            yield return null;
        }
    }

}
