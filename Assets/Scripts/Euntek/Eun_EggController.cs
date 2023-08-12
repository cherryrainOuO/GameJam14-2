using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eun_EggController : MonoBehaviour
{
    // [SerializeField] private 
    [SerializeField] private Eun_RandomUnitByGrid randomUnitByGrid;
    [SerializeField] private Kyun_NatureEggUnit[] eggs; // 3개 돌리기
    private bool isCounting = false;

    private void Start()
    {
        Vector2 pos = randomUnitByGrid.EggInit();

        eggs[0].transform.position = pos;
        eggs[0].gameObject.SetActive(true);

        CoroutineForEggTimeCounting(eggs[0]);
    }
    public void GetEgg(Kyun_NatureEggUnit _egg)
    {
        isCounting = false;
        Vector2 pos = randomUnitByGrid.EggInit();

        _egg.transform.position = pos;

        CoroutineForEggTimeCounting(_egg);
    }


    private IEnumerator CoroutineForEggTimeCounting(Kyun_NatureEggUnit _egg)
    {
        isCounting = true;

        float time = 0f;

        while (time <= 5f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (isCounting)
        {
            isCounting = false;
            Vector2 pos = randomUnitByGrid.EggInit();

            _egg.transform.position = pos;

            CoroutineForEggTimeCounting(_egg);
        }
    }


}
