using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBuff : Buff
{
    PlayerData playerData;

    float baseMsValue = 1.5f;
    float totalMsValue;

    float baseAtkSpdMultiplier = 0.9f;
    float totalAtkSpdValue;

    private void Awake()
    {
        playerData = PlayerData.Instance;
        duration = 3f;
    }

    public override void BuffStart()
    {
        totalMsValue = baseMsValue * value;
        playerData.MovementSpeed += totalMsValue;

        totalAtkSpdValue = Mathf.Pow(baseAtkSpdMultiplier, value);
        playerData.AttackSpeed *= totalAtkSpdValue;
    }

    public override void BuffEnd()
    {
        playerData.MovementSpeed -= totalMsValue;
        playerData.AttackSpeed /= totalAtkSpdValue;
    }
}
