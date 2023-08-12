using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Eun_ForkBoomSystem : MonoBehaviour
{
    private List<Eun_Fork> forks;

    // Start is called before the first frame update
    void Start()
    {
        forks = GetComponentsInChildren<Eun_Fork>().ToList();
    }

    /// <summary>
    /// 돼지 죽을때마다 실행시키세요~
    /// </summary>
    public void ForkBoom()
    {
        int rand = Random.Range(0, forks.Count);

        forks[rand].Boom();

        forks.RemoveAt(rand);
    }
}
