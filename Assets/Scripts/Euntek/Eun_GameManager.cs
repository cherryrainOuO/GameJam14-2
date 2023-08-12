using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eun_GameManager : Singleton<Eun_GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(320, 180, FullScreenMode.ExclusiveFullScreen);
    }


}
