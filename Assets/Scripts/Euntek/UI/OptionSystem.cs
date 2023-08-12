using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSystem : MonoBehaviour
{
    [SerializeField] private Slider bgm;
    [SerializeField] private Dropdown resolution;
    // Start is called before the first frame update
    void Start()
    {
        bgm.value = Eun_SoundManager.Instance.volume;
        //resolution.AddOptions()
    }

    // Update is called once per frame
    void Update()
    {

    }
}
