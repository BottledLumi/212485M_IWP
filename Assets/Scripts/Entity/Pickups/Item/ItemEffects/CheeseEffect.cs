using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheeseEffect : ItemEffect
{
    float baseAtkSpdValue = 0.1f;
    float totalAtkSpdValue;
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        totalAtkSpdValue = baseAtkSpdValue;

        ValueChangedEvent += OnValueChanged;

        totalAtkSpdValue = baseAtkSpdValue * Value;
        playerData.AttackSpeed += totalAtkSpdValue;
    }
    private void OnValueChanged()
    {
        float AtkSpdToAdd = totalAtkSpdValue;
        totalAtkSpdValue = baseAtkSpdValue * Value;
        AtkSpdToAdd = totalAtkSpdValue - AtkSpdToAdd;

        playerData.AttackSpeed += AtkSpdToAdd;
    }

    private void OnDestroy()
    {
        playerData.AttackSpeed -= totalAtkSpdValue;
    }
}
