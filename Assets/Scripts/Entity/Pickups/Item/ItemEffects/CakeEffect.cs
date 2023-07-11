using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeEffect : ItemEffect
{
    float procChance = 0.6f;
    float buffDuration = 3f;

    float baseMsValue = 1.5f;
    float totalMsValue;

    float baseAtkSpdMultiplier = 0.9f;
    float totalAtkSpdValue;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
    }
    public void CakeProc()
    {
        StartCoroutine(StatIncreaseCoroutine());
    }

    IEnumerator StatIncreaseCoroutine()
    {
        totalMsValue = baseMsValue * Value;
        playerData.MovementSpeed += totalMsValue;

        totalAtkSpdValue = Mathf.Pow(baseAtkSpdMultiplier, Value);
        playerData.AttackSpeed *= totalAtkSpdValue;

        yield return new WaitForSeconds(buffDuration);

        playerData.MovementSpeed -= totalMsValue;
        playerData.AttackSpeed /= totalAtkSpdValue;
    }
    public float ProcChance()
    {
        return procChance;
    }
}

