using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eun_ScoreSystem : MonoBehaviour
{
    private int totalScore = 0;
    private int multiple = 1;
    private int feverTimeCheckCount = 0;
    private Coroutine runningCoroutine = null;


    private IEnumerator FeverTimeCheck()
    {
        float time = 0f;

        while (time <= 6f)
        {
            time += Time.deltaTime;

            if (feverTimeCheckCount >= 5)
            {
                feverTimeCheckCount = 0;
                FeverTimeOn();
                break;
            }

            yield return null;
        }

        runningCoroutine = null;
    }
    public void EggToScore()
    {
        totalScore += 100 * multiple;

        if (runningCoroutine == null)
            runningCoroutine = StartCoroutine(FeverTimeCheck());

        feverTimeCheckCount++;
    }

    public void ChickToScore() => totalScore += 400 * multiple;
    public void ChickDieToScore() => totalScore += 50 * multiple;
    public void ChickenAttackBossToScore() => totalScore += 1000 * multiple;
    public void ChickenDefeatBossToScore() => totalScore += 5000 * multiple;

    private void FeverTimeOn()
    {
        Debug.Log("피버타임 온~~");
        multiple = 3;

        StartCoroutine(FeverTimeOff());
    }
    private IEnumerator FeverTimeOff()
    {

        yield return YieldFunctions.WaitForSeconds(10f);

        multiple = 1;
    }


}
