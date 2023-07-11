using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarrotEffect : ItemEffect
{
    float baseAtkValue = 3;
    float totalAtkValue;

    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.Instance;
        totalAtkValue = baseAtkValue;

        ValueChangedEvent += OnValueChanged;

        totalAtkValue = baseAtkValue * Value;
        playerData.Attack += totalAtkValue;
    }
    private void OnValueChanged()
    {
        float atkToAdd = totalAtkValue;
        totalAtkValue = baseAtkValue * Value;
        atkToAdd = totalAtkValue - atkToAdd;

        playerData.Attack += atkToAdd;
    }

    private void OnDestroy()
    {
        playerData.Attack -= totalAtkValue;
    }
}
