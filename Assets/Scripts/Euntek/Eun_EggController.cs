using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eun_EggController : MonoBehaviour
{
    // [SerializeField] private 
    [SerializeField] private Eun_RandomUnitByGrid randomUnitByGrid;
    [SerializeField] private Transform[] eggs; // 3개 돌리기

    private void Start()
    {
        Vector2 pos = randomUnitByGrid.EggInit();

        eggs[0].gameObject.SetActive(true);
        eggs[0].position = pos;


    }

    private IEnumerator CoroutineForEggTimeCount()
    {
        float time = 0f;

        while (time <= 5f)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }


}
